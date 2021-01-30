using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundController : MonoBehaviour
{
    public Transform backgroundImage;
    private bool transitioning = false;
    private bool swappedTransition = false;
    public BiomeSettings currentBiome;
    public float minWeight = 0.1f;
    public float weightMultiplier = 1.2f;
    public float currentX = 0;
    public Transform brights;

    public Transform[] layers;
    public ParallaxBackgroundTransitionLayer transitionLayer;
    private Camera cam;
    private Vector3 transitionStartPos;

    private PlayerController player;
    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        currentX = player.transform.position.x;

        for (int i = 0; i < layers.Length; i++)
        {
            var weight = minWeight + (i * weightMultiplier);
            UpdateLayerPosition(layers[i], weight);
        }

        if (transitioning)
        {
            UpdateTransitionLayer();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            StartTransition();
        }

        UpdateTransition();
    }

    private void UpdateTransitionLayer()
    {
        var weight = minWeight + (layers.Length * weightMultiplier);
        UpdateLayerPosition(transitionLayer.transform, weight, true);
    }

    private void UpdateTransition()
    {
        if (!transitioning) return;

        var camLeft = cam.transform.position.x - 9;
        if (!swappedTransition && transitionLayer.transitionPoint.transform.position.x < camLeft)
        {
            FinishTransition();
        }
        else if (transitionLayer.endPoint.transform.position.x < camLeft)
        {
            CleanUpTransition();
        }
    }

    public void StartTransition()
    {
        transitioning = true;
        swappedTransition = false;
        transitionStartPos = player.transform.position;
        transitionLayer.gameObject.SetActive(true);
        UpdateTransitionLayer();
    }

    private void FinishTransition()
    {
        swappedTransition = true;
        SwapLayers(currentBiome);
    }

    private void CleanUpTransition()
    {
        transitioning = false;
        swappedTransition = false;

        //destroy old transition layer
        var newTransition = Instantiate(currentBiome.transitionLayer, transitionLayer.transform.parent);
        Destroy(transitionLayer.gameObject);
        transitionLayer = newTransition;
        transitionLayer.gameObject.SetActive(false);
    }

    private void UpdateLayerPosition(Transform layer, float weightX, bool isTransition = false)
    {
        var pos = layer.localPosition;
        pos.x = isTransition ? (-(currentX - transitionStartPos.x) * weightX) : (-currentX * weightX);
        layer.localPosition = pos;

        // for debug purposes: if their local position is out of view, bring them back to the front
        for(int i = 0; i < layer.childCount; i++)
        {
            var child = layer.GetChild(i);
            if (!isTransition && child.position.x - cam.transform.position.x < -20)
            {
                child.position = new Vector3(cam.transform.position.x + 20, child.position.y, child.position.z);
            }
        }
    }

    public void SetBiome(BiomeSettings biome, bool skipTransition = false)
    {
        currentBiome = biome;
        if (!skipTransition)
        {
            StartTransition();
        }
        else
        {
            StartTransition();
            FinishTransition();
            CleanUpTransition();
        }

    }

    public void SwapLayers(BiomeSettings biome)
    {
        // layers
        for(int i = 0; i < layers.Length; i++)
        {
            // remember where old layer was
            float copyX = layers[i].transform.position.x;

            // Destroy and reinstantiate new layer
            Destroy(layers[i].gameObject);
            layers[i] = Instantiate(biome.layers[i], transform);

            // calculate by how much we'll need to move the children to recover their rightful place
            float moveBy = layers[i].transform.position.x - copyX;

            // move the parent where the old layer was
            layers[i].transform.position = new Vector3(
                copyX,
                layers[i].transform.position.y,
                layers[i].transform.position.z);

            // move the children to recover appropiate place
            for(int j = 0; j < layers[i].childCount; j++)
            {
                layers[i].GetChild(j).transform.position += new Vector3(moveBy, 0, 0);
            }
        }

        // sky image
        backgroundImage.transform.position = new Vector3(
            backgroundImage.transform.position.x,
            backgroundImage.transform.position.y,
            biome.backgroundZ);

        // brights
        Destroy(brights.gameObject);
        brights = Instantiate(biome.brights, transform);
    }
}

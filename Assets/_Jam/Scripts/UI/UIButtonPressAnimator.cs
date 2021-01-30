using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonPressAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public float minScale = 0.9f;
    private float bounceSpeed = 5f;
    private bool isPressed = false;

    private void Update()
    {
        UpdateBounce();
    }

    private void UpdateBounce()
    {
        float s = transform.localScale.x;
        s = Mathf.Clamp(s + (bounceSpeed * Time.deltaTime) * (isPressed ? -1 : 1), minScale, 1f);

        transform.localScale = new Vector3(s, s, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPressed = false;
    }
}

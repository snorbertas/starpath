using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ScarfController scarf;
    public Animator outroAnimator;
    public ParticleSystem starParticles;
    public ParticleSystem dustParticles;
    public ParticleSystem powerupParticles;
    public Animator playerAnimator;
    public Animator starRobeAnimator;
    public Transform playerVisual;
    public Transform starVisual;
    public enum Mode { FallingStar, Player }
    public Mode mode = Mode.FallingStar;

    [Header("Fixing Physics")]
    public float maxAngle = 80f;
    public float minVelocity = 1f;
    public float maxVelocity = 10f;
    private Rigidbody2D body;

    [Header("Gameplay")]
    public float jumpForce = 10f;
    public float flapForce = 10f;

    public int collisionCount = 0;
    private bool isFinalAccel = false;
    public bool playerIsDead = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    IEnumerator TransformToPlayer()
    {
        starRobeAnimator.enabled = true;
        yield return new WaitForSeconds(1.5f);

        starVisual.gameObject.SetActive(false);
        playerVisual.gameObject.SetActive(true);
        scarf.activePieces = 7;
    }

    void FixedUpdate()
    {
        if (playerIsDead)
        {
            body.velocity /= 1.1f;
            if (body.velocity.magnitude < 0.5f)
                body.velocity = Vector2.zero;
        }

        else if (mode == Mode.FallingStar)
        {
            // Fix velocity
            body.velocity = new Vector2(10, -5).normalized * maxVelocity;
            if (IsOnGround)
            {
                mode = Mode.Player;
                StartCoroutine(TransformToPlayer());
            }
        }

        else if(mode == Mode.Player)
        {
            // Fix velocity
            var velocity = body.velocity.magnitude;
            if (velocity > maxVelocity) body.velocity = body.velocity.normalized * maxVelocity;
            else if (velocity < minVelocity) body.velocity = new Vector2(minVelocity, minVelocity);

            // Fix angle
            if (body.rotation < -maxAngle) body.SetRotation(-maxAngle);
            else if (body.rotation > maxAngle) body.SetRotation(maxAngle);
        }
    }

    private void Update()
    {
        if (playerIsDead || mode == Mode.FallingStar) return;

        if (Input.GetMouseButtonDown(0))
        {
            TryJump();
        }

        if(playerAnimator.gameObject.activeInHierarchy)
            playerAnimator.SetBool("Sliding", IsOnGround);

        if (IsOnGround && !dustParticles.isEmitting)
            dustParticles.Play();
        if (!IsOnGround && dustParticles.isEmitting)
            dustParticles.Stop();
    }

    private void TryJump()
    {
        if (isFinalAccel || playerIsDead) return;

        if (IsOnGround)
        {
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            body.angularVelocity = 0;
        }
        else
        {
            if (scarf.activePieces <= 0) return;
            scarf.activePieces = Mathf.Clamp(scarf.activePieces - 1, 0, 7);

            body.velocity = new Vector2(body.velocity.magnitude, 0);
            body.AddForce(new Vector2(0, flapForce), ForceMode2D.Impulse);
            
            if(playerAnimator.gameObject.activeInHierarchy)
                playerAnimator.SetTrigger("Flap");

            AudioManager.PlaySound(AudioManager.Sound.Flap);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionCount == 0 && !playerIsDead)
            AudioManager.PlaySlideSound();
        collisionCount++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount--;
        if (collisionCount == 0)
            AudioManager.StopSliding();
    }

    private bool IsOnGround { get { return collisionCount > 0;  } }

    public void GetHit()
    {
        playerIsDead = true;
        dustParticles.Stop();
        playerAnimator.SetTrigger("Dead");
        StartCoroutine(Restart());
        AudioManager.PlaySound(AudioManager.Sound.Impact);
    }

    public void TriggerEvent(PlayerEventTrigger.EventType type)
    {
        if(type == PlayerEventTrigger.EventType.FinalAccelerate)
        {
            isFinalAccel = true;
            maxVelocity = 100f;
            body.gravityScale = 1f;
        }

        else if(type == PlayerEventTrigger.EventType.FinalJump)
        {
            body.gravityScale = -3f;

            starVisual.gameObject.SetActive(true);
            playerVisual.gameObject.SetActive(false);

            StartCoroutine(PlayOutro());
        }

        else if(type == PlayerEventTrigger.EventType.StarPower)
        {
            scarf.activePieces = Mathf.Clamp(scarf.activePieces + 1, 0, 7);
            powerupParticles.Play();
            AudioManager.PlaySound(AudioManager.Sound.PowerUp);
        }

        else if(type == PlayerEventTrigger.EventType.IceChunk)
        {
            GetHit();
        }
    }

    IEnumerator PlayOutro()
    {
        yield return new WaitForSeconds(2f);
        outroAnimator.enabled = true;
        starParticles.Stop();
    }

    IEnumerator Restart()
    {
        // Just reload scene
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("norb3");
    }
}

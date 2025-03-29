using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Cinemachine;

public class CinematicTransitionManager : MonoBehaviour
{
    public GameObject player;
    public GameObject badGuy;
    public float runDuration = 2f;
    public Image fadeImage; // Full-screen UI image for fade
    public Transform cinematicRunTarget; // Point to run toward
    public Transform newPlayerPosition; // Where to teleport the player after fade

    private PlayerMovement2D playerMovement; // <-- Replace with your actual movement script name
    private ChasePlayer chasePlayer;
    public CinemachineVirtualCamera virtualCamera;

    // Backup original camera values
    private float originalYDamping;
    private Vector2 originalDeadZone;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement2D>();
        chasePlayer = badGuy.GetComponent<ChasePlayer>();

        var transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        originalYDamping = transposer.m_YDamping;
        originalDeadZone = new Vector2(transposer.m_DeadZoneWidth, transposer.m_DeadZoneHeight);
    }

    public void TurnDirection()
    {
        chasePlayer.flipDir();
        badGuy.transform.position = new Vector2(player.transform.position.x + -chasePlayer.dir * 15, player.transform.position.y + 1);
    }

    public void StartCinematicTransition()
    {
        StartCoroutine(CinematicSequence());
    }

    private IEnumerator CinematicSequence()
    {
        // Disable player input
        playerMovement.enabled = false;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        var transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        // Add high damping and dead zones so camera doesn't move
        transposer.m_YDamping = 100f;
        transposer.m_DeadZoneHeight = 100f;
        transposer.m_DeadZoneWidth = 100f;
        
        // Vector3 playerScale = player.transform.localScale;

        // Auto-run player toward cinematicRunTarget
        float timer = 0f;
        while (timer < runDuration)
        {
            Vector2 dir = (cinematicRunTarget.position - player.transform.position).normalized;
            rb = player.GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + dir * playerMovement.moveSpeed*2 * Time.deltaTime);
            // player.transform.localScale -= new Vector3(0.002f, 0.002f, 0.002f);
            timer += Time.deltaTime;
            yield return null;
        }
        // 🔒 Freeze Y movement and disable gravity
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        
        // Fade out
        yield return StartCoroutine(Fade(1));

        // Disable damping and dead zones for snappy camera follow
        transposer.m_YDamping = 0f;
        transposer.m_DeadZoneHeight = 0f;
        transposer.m_DeadZoneWidth = 0f;

        // Move player to new position
        player.transform.position = newPlayerPosition.position;
        playerMovement.moveSpeed += 1f;
        // player.transform.localScale = playerScale;

        chasePlayer.recheckDir();
        badGuy.transform.position = new Vector2(newPlayerPosition.position.x + -chasePlayer.dir * 18, newPlayerPosition.position.y + 1);
        chasePlayer.moveSpeed *= 1.3f;

        Vector3 cameraTargetPosition = player.transform.position + (Vector3)transposer.m_TrackedObjectOffset;
        Vector3 cameraDelta = cameraTargetPosition - virtualCamera.transform.position;

        virtualCamera.OnTargetObjectWarped(player.transform, cameraDelta);


        // Restore camera settings
        transposer.m_YDamping = originalYDamping;
        transposer.m_DeadZoneHeight = originalDeadZone.y;
        transposer.m_DeadZoneWidth = originalDeadZone.x;

        // Re-enable player input
        playerMovement.enabled = true;

        // Fade in
        yield return StartCoroutine(Fade(0));

        
        // 🔓 Restore normal Rigidbody physics
        rb.gravityScale = 1f; // Or whatever your default is
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        Debug.Log("Player: " + playerMovement.moveSpeed);
        Debug.Log("Bad Guy: " + chasePlayer.moveSpeed);
        Debug.Log("---");
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float duration = 1f;
        float startAlpha = fadeImage.color.a;
        float timer = 0f;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}

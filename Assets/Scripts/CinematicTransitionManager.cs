using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CinematicTransitionManager : MonoBehaviour
{
    public GameObject player;
    public float runDuration = 2f;
    public Image fadeImage; // Full-screen UI image for fade
    public Transform cinematicRunTarget; // Point to run toward
    public Transform newPlayerPosition; // Where to teleport the player after fade

    private PlayerMovement2D playerMovement; // <-- Replace with your actual movement script name

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement2D>(); // <-- Again, use your script's class name here
    }

    public void StartCinematicTransition()
    {
        StartCoroutine(CinematicSequence());
    }

    private IEnumerator CinematicSequence()
    {
        // Disable player input
        playerMovement.enabled = false;

        // Auto-run player toward cinematicRunTarget
        float timer = 0f;
        while (timer < runDuration)
        {
            Vector2 dir = (cinematicRunTarget.position - player.transform.position).normalized;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + dir * playerMovement.moveSpeed*2 * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        // Fade out
        yield return StartCoroutine(Fade(1));

        // Move player to new position
        player.transform.position = newPlayerPosition.position;

        // Fade in
        yield return StartCoroutine(Fade(0));

        // Re-enable player input
        playerMovement.enabled = true;
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

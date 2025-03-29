using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    public CinematicTransitionManager transitionManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transitionManager.StartCinematicTransition();
        }
    }
}

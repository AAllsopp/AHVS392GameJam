using UnityEngine;

public class FlipTransition : MonoBehaviour
{
    public CinematicTransitionManager transitionManager;
    private bool isTriggered;
    void Start()
    {
        isTriggered = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isTriggered)
            {
                isTriggered = true;
                transitionManager.TurnDirection();
            }
        }
    }
}

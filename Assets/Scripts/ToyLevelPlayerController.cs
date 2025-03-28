using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class TopLevelPlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public float Speed = 1.0f;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    SpriteRenderer spriteRenderer;
    Light2D[] eyes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        eyes = GetComponentsInChildren<Light2D>();
        Debug.Log("Number of eyes: " + eyes.Length);
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        if (move.x != 0) {
            bool oldFlipX = spriteRenderer.flipX;

            spriteRenderer.flipX = move.x > 0;
            bool newFlipX = spriteRenderer.flipX;

            if (oldFlipX != newFlipX) {
                foreach (Light2D eye in eyes) {
                    eye.transform.localPosition = new Vector3(
                    eye.transform.localPosition.x * -1,
                    eye.transform.localPosition.y,
                    eye.transform.localPosition.z);
                }
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * Speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}

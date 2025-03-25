using UnityEngine;
using UnityEngine.InputSystem;

public class TopLevelPlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public float Speed = 0.01f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position = (Vector2)transform.position + move * Speed * Time.deltaTime;
        transform.position = position;



        // float horizontal = 0.0f;
        // Vector2 position = transform.position;
        // position.x = position.x + 0.1f;
        // Vector2 position = transform.position; // Cast transform.position to Vector2
        // if (LeftAction.IsPressed()) {
        //     position.x -= Speed;  // Move left on the X-axis
        // }
        // transform.position = position;
        // position.x += 0.1f;  // Move right on the X-axis
        // transform.position = new Vector3(position.x, position.y, transform.position.z);
        // if (Keyboard.current.leftArrowKey.isPressed) {
        //     horizontal = -1.0f;
        // } else if (Keyboard.current.rightArrowKey.isPressed) {
        //     horizontal = 1.0f;
        // }
        // Debug.Log(horizontal);
    }
}

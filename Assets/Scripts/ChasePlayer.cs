using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public Transform Player;
    public float moveSpeed;
    private Rigidbody2D rb;
    public float dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Player.position.x - transform.position.x > 0)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        Debug.Log(dir);

        moveSpeed = 2f;

    }

    public void recheckDir()
    {
        float oldDir = dir;
        if (Player.position.x - transform.position.x > 0)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
        if (dir != oldDir)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    void Update()
    {
        // Vector2 dir = (Player.position - transform.position).normalized;

        // Debug.Log(dir);

        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

        // transform.position += new Vector3(dir.x * moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

    
    }
}

using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Animator animator;
    private bool isFacingRight = false;
    private float horizontalInput = -1f;
    public float speed = 1f;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        // Ensure the Rigidbody2D component is assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component is missing.");
            }
        }

        StartCoroutine(ToggleHorizontalInputRoutine());
    }

    private void FixedUpdate()
    {
        // Add other logic here if needed
        MoveMonster();
    }

    private void MoveMonster()
    {
        // Check if Rigidbody2D is assigned
        if (rb == null)
        {
            return;
        }

        // Flip the Monster's face based on the direction
        if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }

        // Move the Monster using Rigidbody2D
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    private System.Collections.IEnumerator ToggleHorizontalInputRoutine()
    {
        while (true)
        {
            // Toggle the sign of horizontalInput every 1 second
            horizontalInput *= -1f;
            yield return new WaitForSeconds(1f);
        }
    }
}

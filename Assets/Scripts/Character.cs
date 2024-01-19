using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed;
    public bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        moveSpeed = 5f;
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.UpArrow)) && rb.velocity.y == 0 && isGrounded)
            rb.AddForce(Vector2.up * 700f);

        if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if (Mathf.Approximately(rb.velocity.y, 0))
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        if (rb.velocity.y > 0)
            anim.SetBool("isJumping", true);
        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void LateUpdate()
    {
        if (Mathf.Approximately(dirX, 0))
            return; // Do not change facing direction if not moving

        facingRight = dirX > 0;

        if ((facingRight && localScale.x < 0) || (!facingRight && localScale.x > 0))
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}

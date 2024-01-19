using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grounded : MonoBehaviour

{
    private GameObject Player;

    void Start()
    {
        // Assuming this script is attached to a child object of the player
        Player = transform.parent.gameObject;
    }

    void Update()
    {
        // You can add other logic here if needed
    }

    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.collider.CompareTag("Ground"))
        {
            Character move2DComponent = Player.GetComponent<Character>();
            if (move2DComponent != null)
            {
                move2DComponent.isGrounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Character move2DComponent = Player.GetComponent<Character>();
            if (move2DComponent != null)
            {
                move2DComponent.isGrounded = false;
            }
        }
    }
}

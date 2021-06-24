using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPatrol : MobBasic
{

    public bool mustPatrol;
    public bool mustTurn;

    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public LayerMask wallLayer;

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        animator.SetFloat("Speed", Mathf.Abs(walkSpeed));

    }

    void FixedUpdate()
    {
        if (mustPatrol)
        //this uses the groundCheckPosition to check if the platform ends
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    public void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(wallLayer) || bodyCollider.IsTouchingLayers(groundLayer)) 
        //if the enemy collides with wall or go to edge of platform, it will flip
        // have to add condition of groundlayer for the case whereby tiles at the side are ground tiles
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    public void Flip()
    //function to flip the enemy
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPatrol : MobBasic
{
    //public float walkSpeed;
    //public float distToPlayer;

    public bool mustPatrol;
    public bool mustTurn;

    //public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public LayerMask wallLayer;
    //public Transform player;
    //public GameObject bullet;


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

       /* distToPlayer = Vector2.Distance(transform.position, player.position);

        if(distToPlayer <= range)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
                // if the player is behind the enemy and is within range, the enemy will turn
                // to face the player
            {
                Flip();
            }

            mustPatrol = false;
            rb.velocity = Vector2.zero;
            StartCoroutine(Shoot());
        }
        else
        {
            {
                mustPatrol = true;
            }
        }
        */
    }

    void FixedUpdate()
    {
        if (mustPatrol)
        //this uses the groundCheckPosition to check if the platform ends
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(wallLayer) || bodyCollider.IsTouchingLayers(groundLayer)) 
        //if the enemy collides with wall or go to edge of platform, it will flip
        // have to add condition of groundlayer for the case whereby tiles at the side are ground tiles
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    //function to flip the enemy
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

   /* IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeBTWShots);
        GameObject newBullet = Instantiate(bullet, shootPos, Quaternion.identity);
    }
    */
}

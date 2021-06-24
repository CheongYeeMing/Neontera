using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobRangePatrol : MobPatrol
{
    public float distToPlayer;
    public float range;
    public float timeBTWShots;
    public float shootSpeed;

    public bool canShoot;

    public Transform player;
    public Transform shootPos;

    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }


        distToPlayer = Vector2.Distance(transform.position, player.position);

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

            if (canShoot)
            {
            StartCoroutine(Shoot());
            }
        }
        else
        {
            {
                mustPatrol = true;
            }
        }
        
    }

    public IEnumerator Shoot()
    {
        animator.SetBool("IsShooting", true);
        
        canShoot = false;

        yield return new WaitForSeconds(timeBTWShots);
        GameObject newProjectile = Instantiate(projectile, shootPos.position, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, 0f);
        //Debug.Log("Shoot");
        canShoot = true;

        animator.SetBool("IsShooting", false);
        
    }
    
}

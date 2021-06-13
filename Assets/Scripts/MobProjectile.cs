using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobProjectile : MonoBehaviour
{

    public float dieTime, damage;
   // public float speed;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
        //rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Rigidbody2D player = hitInfo.GetComponent<Rigidbody2D>();
        if (player != null)
        {
           // player.TakeDamage(damage);
           Vector2 force = rb.velocity * Time.deltaTime;
           player.AddForce(force);
        }
        Destroy(gameObject);
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {

        Disappear();

    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(dieTime);

        Disappear();
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBasic : MonoBehaviour
{

    public float walkSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    public int health;

    //public GameObject deathEffect;
    
    /*public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    */

    public void Die()
    {
        Destroy(gameObject);
    }
    
}

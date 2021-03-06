using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWormFireball : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected ParticleSystem particle;

    protected Rigidbody2D body;
    private Animator animator;
    protected float lifetime;
    protected float speed;

    // Start is called before the first frame update
    public virtual IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        lifetime = Random.Range(10, 15);
        speed = Random.Range(10, 20);
        if (GameObject.FindGameObjectWithTag("Character").transform.position.x < transform.position.x) transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        body.AddForce(new Vector2(Mathf.Sign(transform.localScale.x) * 120, 250));
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Instantiate(particle, collision.gameObject.transform.position, transform.rotation);
            StartCoroutine(CollideCharacter(collision.gameObject));
        }
        else if (collision.gameObject.tag == "Invincible" && collision.gameObject.layer == 8)
        {
            CollideGround(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Invincible" && collision.gameObject.layer == 9)
        {
            CollideWall(collision.gameObject);
        }
    }

    public virtual IEnumerator CollideCharacter(GameObject character)
    {
        body.velocity = Vector2.zero;
        animator.SetTrigger("Explode");
        character.GetComponent<CharacterHealth>().SetAttackedBy(gameObject);
        character.GetComponent<CharacterHealth>().TakeDamage(damage);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public void CollideGround(GameObject ground)
    {
        body.velocity = new Vector2(body.velocity.x, -body.velocity.y);
    }

    public void CollideWall(GameObject ground)
    {
        body.velocity = new Vector2(body.velocity.x, -body.velocity.y);
    }
}

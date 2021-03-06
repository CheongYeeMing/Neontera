using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleKingBullet2 : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected ParticleSystem particle;

    protected Rigidbody2D body;

    protected float lifetime;
    protected float speed;

    // Start is called before the first frame update
    public virtual IEnumerator Start()
    {
        body = GetComponent<Rigidbody2D>();
        lifetime = Random.Range(2, 20);
        speed = Random.Range(10, 20);
        if (GameObject.FindGameObjectWithTag("Character").transform.position.x < transform.position.x) transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        body.AddForce(new Vector2(Mathf.Sign(transform.localScale.x)*500,0.2f*900));
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
            Instantiate(particle, collision.gameObject.transform.position, transform.rotation);
            CollideGround(collision.gameObject);
        }
    }

    public virtual IEnumerator CollideCharacter(GameObject character)
    {
        body.velocity = Vector2.zero;
        character.GetComponent<CharacterHealth>().SetAttackedBy(gameObject);
        character.GetComponent<CharacterHealth>().TakeDamage(damage);
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }

    public IEnumerator CollideGround(GameObject ground)
    {
        body.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }
}

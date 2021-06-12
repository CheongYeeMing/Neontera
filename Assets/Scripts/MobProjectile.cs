using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobProjectile : MonoBehaviour
{

    public float dieTime, damage;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
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

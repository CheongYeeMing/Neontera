using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] public Portal Destination;
    [SerializeField] public string Location;

    [SerializeField] public bool isActivated;
    [SerializeField] GameObject PortalNameTag;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PortalNameTag.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1.2f);
        if (isActivated)
        {
            animator.SetTrigger("Activate");
        }
        else
        {
            animator.SetTrigger("Deactivate");

        }
    }

    public void Teleport(GameObject Character)
    {
        if (isActivated && !Character.GetComponent<ParallaxBackgroundManager>().isTeleporting)
        {
            Character.GetComponent<ParallaxBackgroundManager>().isTeleporting = true;
            FindObjectOfType<AudioManager>().StopEffect("Portal");
            FindObjectOfType<AudioManager>().PlayEffect("Portal");
            StopAllCoroutines();
            StartCoroutine(Character.GetComponent<ParallaxBackgroundManager>().ChangeBackground(Destination.Location, Character, Destination));
        }
    }
}

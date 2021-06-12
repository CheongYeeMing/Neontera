using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Animator animator;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float wallJumpCooldown; // Prevent instant teleportation up wall
    private float horizontalInput;
    private string currentState;
    [SerializeField] public CharacterAttack characterAttack;

    // Animation States
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    

    private void Awake()
    {
        // Grab reference for rigidbody and animaator from game object
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    public void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interupting itself
        if (currentState == newState) return;

        // Play the Animation
        animator.Play(newState);

        // Reassign current state with new state
        currentState = newState;
    }

    private void FixedUpdate()
    {
        if (CanMove() == false) return;

        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-0.3f, 0.3f, 1);
        }

        // Set animator parameters
        //animator.SetBool("run", horizontalInput != 0);
        //animator.SetBool("grounded", isGrounded());
        if (isGrounded() && !characterAttack.isAttacking)
        {
            if (horizontalInput != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }

        // Wall Jump
        if (wallJumpCooldown > 0.2f)
        {
            // Player movement
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = new Vector2(transform.localScale.x, 0.3f);
            } else
            {
                body.gravityScale = 3;
            }
            // Player jump key
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        } 
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            //animator.SetTrigger("jump");
            ChangeAnimationState(PLAYER_JUMP);
        }
        else if (onWall() && !isGrounded())
        {
            ChangeAnimationState(PLAYER_JUMP);
            // Wall grab animation !!
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, jumpPower);
                transform.localScale= new Vector3(-Mathf.Sign(transform.localScale.x) * 0.3f, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.03f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.03f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return !onWall();
    }

    public bool CanMove()
    {
        bool can = true;
        if (FindObjectOfType<Interactable>().isExamining)
        {
            can = false;
        }
        if (FindObjectOfType<InventorySystem>().isOpen)
        {
            can = false;
        }
        DialogueManager[] npc = FindObjectsOfType<DialogueManager>();
        for (int i = 0; i < npc.Length; i++)
        {
            if (npc[i].isTalking)
            {
                can = false;
                break;
            }
        }
        return can;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerController_forest : MonoBehaviour
{
    float horizontalInput;
    public float moveSpeed = 10f;

    Rigidbody2D rb2d; // ALLOWS US TO USE PHYSICS
    public float jumpSpeed = 5f;

    public Transform groundCheckPoint; // point to check ground from
    public LayerMask groundLayer; // we need check collision layer
    float groundCheckRadius = .2f;
    public float maxJumps = 1; // how many total jumps the player can do
    float jumpsLeft = 0; // jumps left is a counter of the jumps a player has left

    Animator anim;

    public float gameOverHeight = -4f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    bool CheckGrounded() //function that returns if we are on the ground or not
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        // platformer can go left and right
        horizontalInput = Input.GetAxis("Horizontal");
        float nextVelocityX = horizontalInput * moveSpeed;
        if (horizontalInput < 0) //left -x
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else if (horizontalInput > 0) // right +x
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        // platformer must be able to jump
        float nextVelocityY = rb2d.velocity.y;
        if (CheckGrounded()) // if on the ground, reset the jumps that the player has left to max
        {
            jumpsLeft = maxJumps;
        }
        if (jumpsLeft > 0 && Input.GetKeyDown(KeyCode.Space)) // if we press space
        {
            nextVelocityY = jumpSpeed;
            jumpsLeft -= 1; //decrement jump count  2 -> 1 -> 0
            AudioManager_forest.singleton.PlaySFX(AudioManager_forest.singleton.jumpSFX, 1);
        }

        if (transform.position.y < gameOverHeight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // SET ANIMATION PARAMETERS
        anim.SetFloat("XSpeed", Mathf.Abs(nextVelocityX));
       
        // anim.SetFloat("YSpeed", nextVelocityY);
       // anim.SetBool("Grounded", CheckGrounded());

        // SET our velocity
        rb2d.velocity = new Vector2(nextVelocityX, nextVelocityY);
        // VELOCITY = DISTANCE / TIME;
        // ACCELERATION = VELOCITY / TIME; 
        // RIGIDBODY DOESNT NEED TIME DELTA TIME BECAUSE UNITY IS AUTOMATICALLY DOING THAT

        // TRANSFORM TRANSLATE -> We are moving a distance in a frame.
        if(transform.position.y < gameOverHeight)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

   





}  
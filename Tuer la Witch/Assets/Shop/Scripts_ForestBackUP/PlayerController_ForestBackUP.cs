using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
public class PlayerController_ForestBackUP : MonoBehaviour
{
    float horizontalInput;
    public float moveSpeed = 10f;

    Rigidbody2D rb2d; // ALLOWS US TO USE PHYSICS
    public float jumpSpeed = 5f;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI DifficultyText;

    public Transform groundCheckPoint; // point to check ground from
    public LayerMask groundLayer; // we need check collision layer
    float groundCheckRadius = .2f;
    public float maxJumps = 1; // how many total jumps the player can do
    float jumpsLeft = 0; // jumps left is a counter of the jumps a player has left
    public float cur_health = 5;
    public float cur_coins = 0;
    public float cur_difficulty = 1;

    Animator anim;

    public float gameOverHeight = -4f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    bool KeyQ()
    {
        return Input.GetKeyDown(KeyCode.Q);
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
        /*healthText.text = "Health: " + PlayerPrefs.GetInt("Health");
        coinText.text = "Coins: " + PlayerPrefs.GetInt("Coins");
        DifficultyText.text = "Difficulty: " + PlayerPrefs.GetInt("Difficulty");*/
        float nextVelocityX = horizontalInput * moveSpeed;
        if (horizontalInput < 0) //left -x
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0) // right +x
        {
            transform.localScale = new Vector3(1, 1, 1);
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
            
        }

        if (transform.position.y < gameOverHeight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // SET ANIMATION PARAMETERS
        anim.SetFloat("XSpeed", Mathf.Abs(nextVelocityX));
        anim.SetFloat("YSpeed", nextVelocityY);
        anim.SetBool("Grounded", CheckGrounded());
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("Attacker");
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Charge");
        }

        // SET our velocity
        rb2d.velocity = new Vector2(nextVelocityX, nextVelocityY);
        // VELOCITY = DISTANCE / TIME;
        // ACCELERATION = VELOCITY / TIME; 
        // RIGIDBODY DOESNT NEED TIME DELTA TIME BECAUSE UNITY IS AUTOMATICALLY DOING THAT

        // TRANSFORM TRANSLATE -> We are moving a distance in a frame.


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
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

=======

using TMPro;

public class PlayerController : MonoBehaviour
{
    // this is a test
    public Rigidbody2D rb2d;
    public Transform groundPoint;
    public LayerMask groundMask;
    public TextMeshProUGUI healthText;
    public int health = 3;
    public float horizontalInput, verticalInput, moveSpeed = 10f, jumpSpeed = 5f, groundCheckRadius = 0.1f, min_bound = -8.38f;
    public int jumps = 1;
    public int cnt = 0;
    public bool inAttack = false;
    public bool inAttackMoveRight = false;
    public SkeletonScript SS;
    public float attackConst = 2f / 60f;
    public Animator anim;
    public PlayerDetectionScript enemyDetection;
    public bool gameContinues = true;
>>>>>>> Portal
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
<<<<<<< HEAD
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
            AudioManager.singleton.PlaySFX(AudioManager.singleton.jumpSFX, 1);
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
            AudioManager.singleton.PlaySFX(AudioManager.singleton.attackSFX, 1); 
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
=======
        SS = FindObjectOfType<SkeletonScript>();
        enemyDetection = FindObjectOfType<PlayerDetectionScript>();
    }
    public bool ContactWithGround()
    {
        return Physics2D.OverlapCircle(groundPoint.position, groundCheckRadius, groundMask);
    }
    public void changeDamage() {
        anim.SetBool("Damaged", false);
    }
    public void dying() {
        anim.SetBool("isDying", true);
    }
    public void destroyPlayer()
    {
        Destroy(gameObject);
    }
    public void checkEnemy()
    {
        if (enemyDetection.triggered)
        {
            SS.receiveDamage();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("isAlive")) {
            return;
        }
        healthText.text = "Health: " + health;
        // Getting the inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        // initializing the speed for the next frame
        float xSpeed = horizontalInput * moveSpeed;
        float ySpeed = rb2d.velocity.y;
        // if in contant with the ground, restore jumps
        if (ContactWithGround()) {
            jumps = 1;
        }
        // if W is pressed, jump
        if (verticalInput > 0 && jumps > 0) {
            ySpeed = jumpSpeed;
            --jumps;
        }
        // if player wanted to move right, adjust the facing of the sprite
        if (horizontalInput > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(horizontalInput < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // player cannot move past boundary
        if (transform.position.x < min_bound) {
            transform.position = new Vector3(min_bound, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.Q) && !inAttack)
        {
            inAttack = true;
            inAttackMoveRight = (transform.localScale.x == 1);
            ++cnt;
        }
        if (inAttack)
        {
            ++cnt;
            if (cnt == 60) {
                cnt = 0;
                inAttack = false;
                if (inAttackMoveRight) { 
                    transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                }
            }
        }
        
        // Set up animation variables
        anim.SetFloat("xSpeed", Mathf.Abs(xSpeed));
        anim.SetBool("xSpeedIsZero", (xSpeed == 0));
        anim.SetBool("Attacked", inAttack);
        // set the velocity for the next frame
        rb2d.velocity = new Vector2(xSpeed, ySpeed);
    }
    
}
>>>>>>> Portal

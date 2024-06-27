using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class PlayerController_Portal : MonoBehaviour
{
    // this is a test
    public Rigidbody2D rb2d;
    public Transform groundPoint;
    public LayerMask groundMask;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI coinText;
    public int health = 3;
    public int coins = 0;
    public float horizontalInput, verticalInput, moveSpeed = 10f, jumpSpeed = 5f, groundCheckRadius = 0.1f, min_bound = -8.38f;
    public int jumps = 2;
    public int max_jumps = 2;
    public int cnt = 0;
    public bool inAttack = false;
    public bool inAttackMoveRight = false;
    public SkeletonScript SS;
    public float attackConst = 2f / 60f;
    public Animator anim;
    public PlayerDetectionScript enemyDetection;
    public bool gameContinues = true;
    public int enemyCnt = 10;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        gameOverScreen.SetActive(true);
    }
    public void attackFinish()
    {
        inAttack = false;
    }
    public void checkEnemy()
    {
        if (enemyDetection.triggered)
        {
            SS = FindObjectOfType<SkeletonScript>();
            SS.receiveDamage();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("isAlive")) {
            return;
        }
        if (transform.position.y < -6) {
            health = 0;
            healthText.text = "Health: " + health;
            gameContinues = false;
            anim.SetBool("isAlive", false);
            SkeletonScript[] SSarray = FindObjectsOfType<SkeletonScript>();
            foreach (SkeletonScript s in SSarray)
            {
                s.anim.SetBool("PlayerIsAlive", false);
                s.SB.velocity = new Vector2(0, 0);
            }
            rb2d.velocity = new Vector2(0, 0);
            destroyPlayer();
            return;
        }
        healthText.text = "Health: " + health;
        enemyText.text = "Enemies Left: " + enemyCnt;
        coinText.text = "Coins: " + coins;
        // Getting the inputs
        horizontalInput = Input.GetAxis("Horizontal");
        // initializing the speed for the next frame
        float xSpeed = horizontalInput * moveSpeed;
        float ySpeed = rb2d.velocity.y;
        // if in contant with the ground, restore jumps
        if (ContactWithGround()) {
            jumps = max_jumps;
        }
        // if W is pressed, jump
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0 && !inAttack) {
            ySpeed = jumpSpeed;
            --jumps;
            AudioManager_Portal.singleton.PlaySFX(AudioManager_Portal.singleton.jumpSFX, 1); //Added By Daniel For Audio
        }
        // if player wanted to move right, adjust the facing of the sprite
        if (horizontalInput > 0 && !inAttack) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(horizontalInput < 0 &&!inAttack) {
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
            AudioManager_Portal.singleton.PlaySFX(AudioManager_Portal.singleton.attackSFX, 1); //Added By Daniel For Audio
        }
        if (inAttack) {
            xSpeed = 0;
        }
        // Set up animation variables
        anim.SetFloat("xSpeed", Mathf.Abs(xSpeed));
        anim.SetBool("xSpeedIsZero", (xSpeed == 0));
        anim.SetBool("Attacked", inAttack);
        // set the velocity for the next frame
        rb2d.velocity = new Vector2(xSpeed, ySpeed);
    }
    
}

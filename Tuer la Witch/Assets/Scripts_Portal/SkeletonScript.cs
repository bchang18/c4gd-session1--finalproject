using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletonScript : MonoBehaviour
{
    public GameObject player;
    public LayerMask playerLayer;
    public Rigidbody2D SB;
    public float SkeletonSpeed = 2f;
    public Animator anim;
    public SkeletonDetectionScript playerDetection;
    public PlayerController_Portal playerController;
    public bool attacked = false;
    public int enemyHealth = 2;
    public int difficulty = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController_Portal>();
        player = playerController.gameObject;
        SB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerDetection = FindObjectOfType<SkeletonDetectionScript>();
        StartCoroutine(shielding());
    }
    public void destroyEnemy()
    {
        Destroy(gameObject);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void receiveDamage(){
        anim.SetBool("Damaged", true);
        if (!anim.GetBool("Shielded"))
        {
            --enemyHealth;
            if (enemyHealth == 0) {
                --playerController.enemyCnt;
                anim.SetBool("isAlive", false);
                SB.velocity = new Vector2(0, 0);
            }
        }
    }
    public void shieldAndDamageFalse() {
        anim.SetBool("Shielded", false);
        anim.SetBool("Damaged", false);
    }
    public void checkPlayer()
    {
        if (playerDetection.triggered) {
            playerController.health--;
            playerController.anim.SetBool("Damaged", true);
            if (playerController.health >= 0) {
                playerController.healthText.text = "Health: " + playerController.health;
            }
            if (playerController.health <= 0) {
                playerController.gameContinues = false;
                playerController.anim.SetBool("isAlive", false);
                playerController.anim.SetBool("isDying", true);
                anim.SetBool("PlayerIsAlive", false);
                playerController.rb2d.velocity = new Vector2(0, 0);
                SB.velocity = new Vector2(0, 0);
                //Destroy(player);
            }
        }
    }
    IEnumerator shielding()
    {
        while (playerController.gameContinues)
        {
            float nxtShield = Random.Range(0, difficulty);
            yield return new WaitForSeconds(nxtShield);
            anim.SetBool("Shielded", true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playerController.health <= 0 || !anim.GetBool("isAlive")) {
            return;
        }
        float moveDirection = player.transform.position.x - transform.position.x;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        float xVelocity = moveDirection * SkeletonSpeed;
        /*if (!attacked)
        {
            if (playerDetection.triggered)
            {
                attacked = true;
                print("attack opened!");
            }
        }*/
        if (moveDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        SB.velocity = new Vector2(xVelocity, 0);
        anim.SetBool("Attacked", playerDetection.triggered);
        anim.SetBool("xSpeedIsZero", (xVelocity == 0));
        anim.SetFloat("xSpeed", Mathf.Abs(xVelocity));
    }
}

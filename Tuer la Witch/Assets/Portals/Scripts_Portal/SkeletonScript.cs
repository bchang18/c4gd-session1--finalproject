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
    public GameObject food;
    public GameObject coin;
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
        dropLoot();
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
    public void dropLoot() {
        float value_food= Random.Range(0f, 10f);
        float value_coin = Random.Range(0f, 10f);
        if (value_food >= 7.5)
        {
            Instantiate(food, transform.position, food.transform.rotation);
        }
        if (value_coin >= 5) {
            if (value_food >= 7.5) {
                Instantiate(coin, new Vector2(transform.position.x - 1, transform.position.y), coin.transform.rotation);
            }
            else {
                Instantiate(coin, transform.position, coin.transform.rotation);
            }
        }
    }
    public void shieldAndDamageFalse() {
        anim.SetBool("Shielded", false);
        anim.SetBool("Damaged", false);
    }
    public void checkPlayer()
    {
        if (!anim.GetBool("PlayerIsAlive")) {
            return;
        }
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
                SkeletonScript[] SSarray = FindObjectsOfType<SkeletonScript>();
                foreach (SkeletonScript s in SSarray)
                {
                    s.anim.SetBool("PlayerIsAlive", false);
                    s.SB.velocity = new Vector2(0, 0);
                }
                playerController.rb2d.velocity = new Vector2(0, 0);
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
        if (!anim.GetBool("PlayerIsAlive") || playerController.health <= 0 || !anim.GetBool("isAlive")) {
            return;
        }
        float moveDirection_x = player.transform.position.x - transform.position.x;
        float moveDirection_y = player.transform.position.y - transform.position.y;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        float xVelocity = moveDirection_x * SkeletonSpeed;
        float yVelocity = moveDirection_y * SkeletonSpeed;
        FoodScript[] foodArray = FindObjectsOfType<FoodScript>();
        foreach (FoodScript f in foodArray) {
            Physics2D.IgnoreCollision(f.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        /*if (!attacked)
        {
            if (playerDetection.triggered)
            {
                attacked = true;
                print("attack opened!");
            }
        }*/
        if (moveDirection_x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        SB.velocity = new Vector2(xVelocity, yVelocity);
        anim.SetBool("Attacked", playerDetection.triggered);
        anim.SetBool("xSpeedIsZero", (xVelocity == 0));
        anim.SetFloat("xSpeed", Mathf.Abs(xVelocity));
    }
}

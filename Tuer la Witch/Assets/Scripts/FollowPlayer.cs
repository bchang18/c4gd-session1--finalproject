using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public PlayerController playerScript;
    public float attackConst = 2f / 60f;
    public void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (playerScript.inAttack)
        {
            if (playerScript.inAttackMoveRight)
            {
                transform.position = new Vector3(transform.position.x - attackConst, transform.position.y, transform.position.z);
                print("decremented 2/60");
            }
            else
            {
                transform.position = new Vector3(transform.position.x + attackConst, transform.position.y, transform.position.z);
                print("incremented 2/60");
            }
        }
        else { 
             transform.position = new Vector3(Mathf.Max(0, player.transform.position.x), transform.position.y, transform.position.z);
        }
    }
}

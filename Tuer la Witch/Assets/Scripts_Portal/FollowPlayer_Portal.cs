using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer_Portal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public PlayerController_Portal playerScript;
    public float attackConst = 2f / 60f;
    public void Start()
    {
        player = FindObjectOfType<PlayerController_Portal>().gameObject;
        playerScript = player.GetComponent<PlayerController_Portal>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (player == null) {
            return;
        }
        if (playerScript.inAttack)
        {
            if (playerScript.inAttackMoveRight)
            {
                transform.position = new Vector3(transform.position.x - attackConst, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + attackConst, transform.position.y, transform.position.z);
            }
        }
        else { 
             transform.position = new Vector3(Mathf.Max(0, player.transform.position.x), transform.position.y, transform.position.z);
        }
    }
}

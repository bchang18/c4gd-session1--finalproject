using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer_Portal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public PlayerController_Portal playerScript;
    public float cameraMinBound = -1.42f;
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
        transform.position = new Vector3(Mathf.Max(cameraMinBound, player.transform.position.x), transform.position.y, transform.position.z);
    }
}

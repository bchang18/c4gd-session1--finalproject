using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer_Village : MonoBehaviour
{
    public GameObject player; //target to follow

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
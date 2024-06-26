using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow_ForestBackUP : MonoBehaviour
{
    public GameObject player; //target to follow

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
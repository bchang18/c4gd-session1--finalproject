using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_forest : MonoBehaviour
{
    float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
        repeatWidth = collider2D.size.x;
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Camera.main.transform.position.x > transform.position.x + repeatWidth)
        {
            transform.position = new Vector3(transform.position.x + repeatWidth, transform.position.y, transform.position.z);
        }
        if (Camera.main.transform.position.x < transform.position.x - repeatWidth)
        {
            transform.position = new Vector3(transform.position.x - repeatWidth, transform.position.y, transform.position.z);
        }*/


    }
}

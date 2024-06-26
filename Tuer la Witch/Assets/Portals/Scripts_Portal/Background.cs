using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    float cameraWidth, backgroundWidth = 22.66162f, offset = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Screen.width / Screen.height;
        backgroundWidth = 22.66162f;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = backgroundWidth;
        if (Camera.main.transform.position.x + (cameraWidth / 2) > transform.position.x + 1.5*backgroundWidth - offset)
        {
            transform.position = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);
        }else if (Camera.main.transform.position.x - (cameraWidth / 2) < transform.position.x - 1.5*backgroundWidth + offset)
        {
            transform.position = new Vector3(transform.position.x - delta, transform.position.y, transform.position.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public PlayerController_Portal player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController_Portal>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            player.coins++;
            Destroy(gameObject);
        }
    }
}

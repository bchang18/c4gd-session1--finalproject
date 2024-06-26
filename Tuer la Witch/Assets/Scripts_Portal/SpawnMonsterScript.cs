using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsterScript : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public PlayerController_Portal player;
    public float spawnRate = 12f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController_Portal>();
        StartCoroutine(spawnMonsters());
    }
    IEnumerator spawnMonsters()
    {
        while (player.gameContinues)
        {
            print("spawned!");
            Instantiate(skeletonPrefab, new Vector2(transform.position.x, skeletonPrefab.transform.position.y), skeletonPrefab.transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

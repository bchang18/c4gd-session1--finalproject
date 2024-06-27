using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnMonsterScript : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public PlayerController_Portal player;
    public float spawnRate = 8f;
    public int enemiesLeft = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController_Portal>();
        StartCoroutine(spawnMonsters());
    }
    IEnumerator spawnMonsters()
    {
        while (player.gameContinues && enemiesLeft > 0)
        {
            --enemiesLeft;
            Instantiate(skeletonPrefab, new Vector2(transform.position.x, skeletonPrefab.transform.position.y), skeletonPrefab.transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
        if (enemiesLeft == 0)
        {
            SceneManager.LoadSceneAsync(3);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

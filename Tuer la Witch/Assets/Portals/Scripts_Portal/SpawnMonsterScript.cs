using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnMonsterScript : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public PlayerController_Portal player;
    public float spawnRate = 10f;
    public int enemiesLeft;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController_Portal>();
        enemiesLeft = 10 + 2* (PlayerPrefs.GetInt("Difficulty") - 1);
        spawnRate -= PlayerPrefs.GetInt("Difficulty") + 1;
        StartCoroutine(spawnMonsters());
    }
    public void restartGame()
    {
        SceneManager.LoadSceneAsync(3);
    }
    IEnumerator spawnMonsters()
    {
        while (player.gameContinues && enemiesLeft > 0)
        {
            --enemiesLeft;
            Instantiate(skeletonPrefab, new Vector2(transform.position.x, skeletonPrefab.transform.position.y), skeletonPrefab.transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

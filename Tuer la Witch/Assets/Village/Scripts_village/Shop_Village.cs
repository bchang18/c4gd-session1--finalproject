using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop_Village : MonoBehaviour
{
    public void MoveToShop()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void MoveToBattle() {
        SceneManager.LoadSceneAsync(4);
    }
}

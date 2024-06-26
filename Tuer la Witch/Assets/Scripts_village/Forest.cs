using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Forest : MonoBehaviour
{
    public void MoveToForest()
    {
        SceneManager.LoadSceneAsync(2);
    }
}

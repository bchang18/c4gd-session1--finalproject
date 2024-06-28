using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    public bool isEnabled = true;
    public bool isType = false;

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z) && playerIsClose)
        {
            if (dialogueText.text != "" && dialogueText.text != dialogue[index])
            {
                dialogueText.text = dialogue[index];
                StopCoroutine(Typing());
                isType = false;
            }
            else { 
                if (dialoguePanel.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    isType = true;
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
            }
            
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);

        }
    }


    public void zeroText()
    {
        isType = false;
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

    }



    //Typing Speed
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            if (!isType) {
                yield break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }



    public void NextLine()
    {
        SceneManager.LoadSceneAsync(3);


    }





    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isEnabled)
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isEnabled)
        {
            playerIsClose = false;
            zeroText();
        }
    }
}

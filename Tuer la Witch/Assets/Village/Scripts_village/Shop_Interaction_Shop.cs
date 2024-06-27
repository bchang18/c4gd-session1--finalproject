using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Interaction_Shop : MonoBehaviour
{
    public GameObject shopPanal;
    //public Text dialogueText;
    public string[] dialogue;
    private int index;

    //public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && playerIsClose)
        {
            if (shopPanal.activeInHierarchy)
            {
                //zeroText();
            }
            else
            {
                shopPanal.SetActive(true);
                //StartCoroutine(Typing());
            }
        }

        /*if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }*/
    }


    /*public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        shopPanal.SetActive(false);
    }



    //Typing Speed
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    


    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());

        }
        else
        {
            zeroText();
        }
    }
    */
    public void ExitShop()
    {
        shopPanal.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            //zeroText();
        }
    }
}

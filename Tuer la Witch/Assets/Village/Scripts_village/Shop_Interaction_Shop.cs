using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shop_Interaction_Shop : MonoBehaviour
{
    public GameObject shopPanal;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject shopButton;
    public GameObject potionPicture;

    //public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;


    void Start()
    {
        shopButton.SetActive(false);
        potionPicture.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && playerIsClose)
        {
            if (shopPanal.activeInHierarchy)
            {

                zeroText();
            }
            else
            {
                shopPanal.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if(!playerIsClose)
        {
            ExitShop();
        }
        

        if (dialogueText.text == dialogue[0])
        {
            print("hi");
            shopButton.SetActive(true);
            potionPicture.SetActive(true);
        }

        


    }


    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        //shopPanal.SetActive(false);
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
        //contButton.SetActive(false);

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
    
    public void ExitShop()
    {
        zeroText();
        shopButton.SetActive(false);
        potionPicture.SetActive(false);
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
            zeroText();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shop_Interaction_Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;

    public GameObject shopButton;
    public GameObject potionPicture;
    public PlayerController_ForestBackUP player;

    //public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    public bool isActive = false;


    void Start()
    {
        shopPanel.SetActive(false);
        player = FindObjectOfType<PlayerController_ForestBackUP>();
    }
    //Typing Speed
    IEnumerator Typing()
    {
        if (index == 0 && dialogueText.text != dialogue[index] || index == 1 && dialogueText.text != dialogue[index] || index == 2 || index == 3)
        {
            shopButton.SetActive(false);
            potionPicture.SetActive(false);
        }
        foreach (char letter in dialogue[index].ToCharArray())
        {
            if (!isActive) {
                if (index == 0) { 
                    ++index;
                }
                if (dialogueText.text == dialogue[1]) {
                    shopButton.SetActive(true);
                    potionPicture.SetActive(true);
                }
                yield break;
            }
            dialogueText.text += letter;
            
            yield return new WaitForSeconds(wordSpeed);
        }
        if (dialogueText.text == dialogue[1])
        {
            shopButton.SetActive(true);
            potionPicture.SetActive(true);
        }
        isActive = false;
        if (index == 0) {
            index++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && playerIsClose)
        {
            if (index == 2 || index == 3) {
                if (dialogueText.text == dialogue[index])
                {
                    ExitShop();
                }
                else {
                    shopButton.SetActive(false);
                    potionPicture.SetActive(false);
                }
            
            }
            if (!isActive && dialogueText.text != dialogue[index])
            {
                // play the text
                isActive = true;
                shopPanel.SetActive(true);
                dialogueText.text = "";
                StartCoroutine(Typing());
                StopCoroutine(Typing());
            }
            else {
                dialogueText.text = dialogue[index];
                isActive = false;
                StopCoroutine(Typing());
            }
        }
        if (!playerIsClose)
        {
            ExitShop();
        }


        




    }

    public void buyPotion() {
        if (player.coins < 3)
        {
            index = 2;
            isActive = true;
            shopPanel.SetActive(true);
            dialogueText.text = "";
            StartCoroutine(Typing());
            StopCoroutine(Typing());
        }
        else {
            PlayerPrefs.SetInt("Health", player.health + 1);
            PlayerPrefs.SetInt("Coins", player.coins - 3);
            index = 3;
            isActive = true;
            shopPanel.SetActive(true);
            dialogueText.text = "";
            StartCoroutine(Typing());
            StopCoroutine(Typing());
        }
    }

    public void ExitShop()
    {
        dialogueText.text = "";
        shopButton.SetActive(false);
        potionPicture.SetActive(false);
        shopPanel.SetActive(false);
        index = 0;
        isActive = false;
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
        }
    }
}

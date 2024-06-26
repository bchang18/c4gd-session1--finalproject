using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Text_forest : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        if (textComponent.text == lines[0]+ "\n" + lines[1])
            gameObject.SetActive(false);




    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
       
       foreach (char c in lines[index].ToCharArray())
       {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
       }
 
    
    }
    void NextLine()
    {
        
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text += "\n";
            StartCoroutine(TypeLine());  
        }
        

    }

}

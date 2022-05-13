using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    //defines area where dialogue box will appear if player is nearby
    public Collider2D dialogueArea;

    //UI panel 
    public GameObject dialogueBox;

    //reference to dialogue text game object
    public TMP_Text dialogueText;
    
    //actual text that will be seen - changed in Unity inspector
    public string text;

    private void Start()
    {
        //untrue by default so dialogue isn't appearing when not triggered
        dialogueBox.SetActive(false);
        //assigning where the dialogue area is going
        dialogueArea = gameObject.GetComponent<CircleCollider2D>();
        dialogueText = dialogueBox.GetComponentInChildren<TMP_Text>();
        dialogueText.text = text;
    }

    //opens dialogue on trigger enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueBox.SetActive(true);
        }
    }

    //closes dialogue on trigger exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
        }
    }

}

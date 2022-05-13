using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject DDOLGO;
    DDOL DDOLScript;
    SceneTransition ST;

    private void Awake()
    {
        DDOLGO = GameObject.FindGameObjectWithTag("DDOL");
        DDOLScript = DDOLGO.GetComponent<DDOL>();
        ST = DDOLGO.GetComponentInChildren<SceneTransition>();
    }


    public void PlayGame()
    {
        //changes scene to play scene on call
        DDOLScript.inventory.transform.parent.gameObject.SetActive(true);
        StartCoroutine(ST.NextScene(2));
    }

    public void ExitGame()
    {
        //Quits out from Unity Game
        Application.Quit();
    }

}

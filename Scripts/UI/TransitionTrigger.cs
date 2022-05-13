using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTrigger : MonoBehaviour
{
    SceneTransition ST;
    public GameObject DDOLGO;
    DDOL DDOLScript;

    GameObject checkpoint;

    public int nextScene;

    [SerializeField] int EnemyNumber;
    public int BackgroundNumber;

    private void Start()
    {
        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
        DDOLGO = GameObject.FindGameObjectWithTag("DDOL");
        DDOLScript = DDOLGO.GetComponentInChildren<DDOL>();
        ST = DDOLGO.GetComponentInChildren<SceneTransition>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if Player is colliding with scene trigger - if scene trigger has tag "BattleTransition" then action below
        if (collision.CompareTag("Player") && gameObject.CompareTag("BattleTransition"))
        {
            //moves the checkpoint trigger to the left slightly once activated - stops player from constantly looping back into battle scene
            checkpoint.transform.position = new Vector3 (gameObject.transform.position.x - 3, gameObject.transform.position.y, gameObject.transform.position.z);
            DDOLScript.FindCheckpoint();
            //selects what enemy to chose 
            DDOLScript.SelectEnemyNumber(EnemyNumber);
            //selects what background to chose
            DDOLScript.SelectBackgroundNumber(BackgroundNumber);
            //starts the battle
            StartCoroutine(ST.BattleScene());
        }
        //if scene has "SceneTransition" tag then action below
        if (collision.CompareTag("Player") && gameObject.CompareTag("SceneTransition"))
        {
            StartCoroutine(ST.NextScene(nextScene));
        }
    }
}

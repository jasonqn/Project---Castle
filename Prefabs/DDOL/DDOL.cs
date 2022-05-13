using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Singleton Pattern restricts the instantiation of a class to one instance 

public class DDOL : MonoBehaviour
{
    //static class ensures it can persist globally
    private static DDOL _instance;

    //bool ensures only one instance of the class can be instantiated
    //at any given time during each scene - if 2 instantiations
    //of this class occur one will destroy itself completely
    //this ensures persistence within each scene and that the player
    //isn't restored to default values upon entering a new scene
    private static bool created = false;
    
    
    public GameObject player;
    private Player playerScript;

    //this bool checks if the Player has entered a "CombatScene"
    //if they have then they cannot move or access the inventory
    public bool isInCombat = false;

    //checkpoint position - allows us to set where to place the Player when transitioning between scenes
    public Vector3 startingPos = new Vector3 (0,0,0);

    //reference to inventory system
    public GameObject inventory;

    //saves the player position to enable checkpoints
    public Vector3 checkpoint;

    //each enemy and scene are assigned a number
    public int selectedEnemyNumber;
    public int selectedBackgroundNumber;


    //Awake function starts Singleton Pattern - if other instance of DDOL object is present they destroys themselves
    private void Awake()
    {
        
        if (!created)
        {
            //if no DDOL object found then make this DDOL object the instance
            DontDestroyOnLoad(gameObject);  
            _instance = this;
            created = true;
        }
        else
        {
            //other DDOL's destroy themselves if their already exists an instance of DDOL
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        FindPlayer();
        FindCheckpoint();
    }

    //finds the player and the players position once the game starts
    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    //assigns what the starting position will be when transitioning between scenes
    public void FindCheckpoint()
    {
        startingPos = GameObject.FindGameObjectWithTag("Checkpoint").transform.position;
    }
    
    //Checks if were in combat and sets what conditions we want when in combat or not i.e. moving/not moving and access to inventory
    public void InCombat()
    {
        isInCombat = !isInCombat;
        //prevents movement when in combat
        playerScript.CanMove();
        //deactivates the inventory Game Object when in combat 
        inventory.SetActive(!isInCombat);
    }

    //currently the input variable is inputed into the Unity editor
    public void SelectEnemyNumber(int input)
    {
        selectedEnemyNumber = input;
    }

    public void SelectBackgroundNumber(int input)
    {
        selectedBackgroundNumber = input;
    }

}

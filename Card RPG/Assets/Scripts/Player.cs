using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    //Loading variables
    public Vector3 myTransform;
    public static Player instance = null;

    //Combat Stats
    public int playerHealth;
    public float timeRequiredForEnergyRegen;   
    public float timeUntilNextEnergy;

    public bool isBlocking;
    public int nextTurnFireDamageMultiplier;
    public int nextTurnPhysicalDamageMultiplier;
    public List<Card.ecardName> currentDeck;
    public List<Card.ecardName> availableCards;   //Later when we unlock cards we'll collect them here.

    public int currentEnergy;

    private void Awake()
    { 
        // Don't destroy on load
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Starto");
    }
	
	// Update is called once per frame
	void Update () {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "Test_Combat" && currentScene.name != "Main Menu")
        {
            myTransform = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }

    public void PlayerTakenDamage(int damage)
    {
        playerHealth -= damage;
    }

    //START FUNCTIONS
    private void InitializeCombatStats()
    {
        playerHealth = 100;
        nextTurnFireDamageMultiplier = 1;
        nextTurnPhysicalDamageMultiplier = 1;
        currentEnergy = 0;
    }
}

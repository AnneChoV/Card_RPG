using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public bool isInFrontLine;
    public List<Card.ecardName> currentDeck;
    public List<Card.ecardName> availableCards;   //Later when we unlock cards we'll collect them here.

    public Canvas canvas;
    public int currentEnergy;

    public Sprite[] heartSprites;
    public Image[] heartDisplays;

    int numFullHearts;

    PlayerStats playerStats;

    // Use this for initialization
    void Start () {
        playerStats = FindObjectOfType<PlayerStats>();
        playerHealth = playerStats.playerHealth;
        SetHealthDisplay();
    }
	
    public void SetHealthDisplay() //WILL NOT SHOW WITH OVER 16 HP.
    {
        numFullHearts = playerHealth / 4;

        if (playerHealth < 0)
        {
            for (int i = 0; i < heartDisplays.Length; i++)
            {
                heartDisplays[i].sprite = heartSprites[0];
            }
            return;
        }

        for (int i = 0; i < heartDisplays.Length; i++)
        {
            if (i < numFullHearts)
            {
                heartDisplays[i].sprite = heartSprites[4];
            }
            else if (i > numFullHearts)
            {
                heartDisplays[i].sprite = heartSprites[0];
            }
            // Decide how far around this heart is (i.e. 1/4 or 2/4 or 3/4)
            else
            {
                int r = playerHealth % 4;
                heartDisplays[i].sprite = heartSprites[r];
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "Test_Combat" && currentScene.name != "Main Menu")
        {
            myTransform = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        

    }


    public void PlayerTakenDamage(int damage)
    {
        playerHealth -= damage;
        SetHealthDisplay();
    }

    //START FUNCTIONS
    private void InitializeCombatStats()
    {
        nextTurnFireDamageMultiplier = 1;
        nextTurnPhysicalDamageMultiplier = 1;
        currentEnergy = 0;
    }
}

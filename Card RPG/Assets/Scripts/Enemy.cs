using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//green stuff.
//arrows on sideppp
public class Enemy : MonoBehaviour {

    //Combat Stats
    public int enemyHealth;
    public bool isBlocking;
    public bool isInFrontLine = true;
    public Card currentCard;
    public GameObject cardPrefab;
    public int nextTurnFireDamageMultiplier;
    public int nextTurnPhysicalDamageMultiplier;
    public List<Card.ecardName> currentDeck;    //Will need to set this up per monster.
    public List<Card.ecardName> TestDeck;    //Will need to set this up per monster.

    public Canvas canvas;

    public int currentEnergy;
    public float timeRequiredForEnergyRegen;
    public float timeUntilNextEnergy;

    public Sprite[] heartSprites;
    public Image[] heartDisplays;
    int numFullHearts;

    void Start ()
    {
        GameObject currentCardGO = Instantiate(cardPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, canvas.transform) as GameObject;
        currentCard = currentCardGO.GetComponent<Card>();
        RectTransform currentCardRT = (RectTransform)currentCard.transform;
        currentCardRT.anchoredPosition = new Vector3(8000.0f, 220.0f, 0.0f);
        SetUpCombatStats();

        SetHealthDisplay();
    }	

	void Update () {
		
	}

    public void SetHealthDisplay() //WILL NOT SHOW WITH OVER 16 HP (make more hearts in editor or some shit).
    {
        if (enemyHealth < 0)
        {
                for (int i = 0; i < heartDisplays.Length; i++)
                {
                    heartDisplays[i].sprite = heartSprites[0];
                }

            return;
        }
        numFullHearts = enemyHealth / 4;

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
                int r = enemyHealth % 4;
                heartDisplays[i].sprite = heartSprites[r];
            }
        }
    }

    //HELPER FUNCTIONS FOR COMBAT MANAGER
    public void EnemyTakenDamage(int damage)
    {
        enemyHealth -= damage;
        SetHealthDisplay();
    }

    //START FUNCTIONS
    private void SetUpCombatStats()
    {
        //We only have one enemy atm, so theyre all the same.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int currentEnergy;
    public float timeRequiredForEnergyRegen;
    public float timeUntilNextEnergy;

    void Start ()
    {
        GameObject currentCardGO = Instantiate(cardPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        currentCard = currentCardGO.GetComponent<Card>();
        SetUpCombatStats();
    }	

	void Update () {
		
	}


    //HELPER FUNCTIONS FOR COMBAT MANAGER
    public void EnemyTakenDamage(int damage)
    {
        enemyHealth -= damage;
    }

    //START FUNCTIONS
    private void SetUpCombatStats()
    {
        //We only have one enemy atm, so theyre all the same.
    }
}

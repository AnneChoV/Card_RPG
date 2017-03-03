using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Combat Stats
    public int enemyHealth;
    public bool isBlocking;
    public float totalTimeBetweenTurns;
    public float turnTimer;
    public Card currentCard;
    public int nextTurnFireDamageMultiplier;
    public int nextTurnPhysicalDamageMultiplier;
    public List<Card.ecardName> currentDeck;    //Will need to set this up per monster.


    void Start ()
    {
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
        enemyHealth = 100;
        totalTimeBetweenTurns = 2.5f;
    }
}

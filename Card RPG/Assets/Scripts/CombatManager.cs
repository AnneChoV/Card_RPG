﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {

    //Make cards highlight while usable.
    //make cards go up and scale while selected.
    //oil placeholder range thing - if we're lucky this is done. >_<

    //Hacky fun times
    public Color oiledColour;
    public Color defaultColour;

    [Header("Particle Systems")]
    //This won't hold up if enemies aren't hard coded
    //Probably better to search child prefabs or something clever like that
    public ParticleSystem playerParticles;
    public ParticleSystem enemyParticles;

    SceneChanger sceneChanger;
    PlayerStats playerStats;

    //Prefabs
    [Header("Prefabs")]
    public GameObject cardPrefab;

    //Important Game Objects
    [Header("Game Objects")]
    public Player player;
    public GameObject characterObject;
    public Enemy enemy;
    public GameObject enemyObject;
    public GameObject playerObject;
    public GameObject winState;
    public GameObject loseState;

    //Player Combat Cards
    [Header("Player Combat Cards")]
    public List<Card> cardHand;
    public Card currentlySelectedCard;

    //UI Stuff
    [Header("UI Stuff")]
    public Canvas UICanvas;
    public Text playerHpText;
    public Text enemyHpText;
    public Text playerEnergyText;
    public Text enemyEnergyText;
    public Text playerRangeLocationText;
    public Text enemyRangeLocationText;

    public GameObject playerEnergySlider;
    public GameObject enemyEnergySlider;


    private bool GameOver = false; 




    // STARTING FUNCTIONS - READ FROM HERE!
    void Start ()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
        playerStats = FindObjectOfType<PlayerStats>();
        SetPlayerAndEnemyStats();
    }	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            player.isInFrontLine = !player.isInFrontLine;
            if (player.isInFrontLine == true)
            {
                playerRangeLocationText.text = "Front Line";
                characterObject.transform.position = new Vector3(-3.0f, 1.9f, 0.0f);
            }
            else
            {
                playerRangeLocationText.text = "Back Line";
                characterObject.transform.position = new Vector3(-6.0f, 1.9f, 0.0f);
            }
        }
        ProcessTimeEvents();

        //This is dirty
        if (player.nextTurnFireDamageMultiplier != 1)
        {
            SpriteRenderer enemyRenderer = enemyObject.GetComponent<SpriteRenderer>();
            enemyRenderer.color = oiledColour;
        }else
        {
            SpriteRenderer enemyRenderer = enemyObject.GetComponent<SpriteRenderer>();
            enemyRenderer.color = defaultColour;
        }

        if (enemy.nextTurnFireDamageMultiplier != 1)
        {
            SpriteRenderer playerRenderer = playerObject.GetComponent<SpriteRenderer>();
            playerRenderer.color = oiledColour;
        }
        else
        {
            SpriteRenderer playerRenderer = playerObject.GetComponent<SpriteRenderer>();
            playerRenderer.color = defaultColour;
        }
    }
 
    //ON UPDATE FUNCTIONS:
    void ProcessTimeEvents()
    {
        ProcessEnemyTurns();
        ProcessPlayerAndEnemyEnergy();
    }

    //  ON UPDATE HELPER FUNCTIONS
        //Process Clicking functions:
    public void ProcessPlayerCardUsing(ParticleSystem clickParticle)
    {
        if (currentlySelectedCard.tier * 33 < player.currentEnergy) //if we have the energy to use the card
        {
            //Whatever happens now, this card is going out. Open the flood gates for J_U_I_C_E
            FX_CardUse("Player");
            //Make the cool bling shit happen
            clickParticle.Emit(1);
            clickParticle.transform.parent = null;

            if (currentlySelectedCard.IsUseableShortRange == true && player.isInFrontLine == false)
            {
                currentlySelectedCard.minDamage /= 2;
                currentlySelectedCard.maxDamage /= 2;
            }
            else if (currentlySelectedCard.IsUseableLongRange == true && player.isInFrontLine == true)
            {
                currentlySelectedCard.minDamage /= 2;
                currentlySelectedCard.maxDamage /= 2;
            }
            if (currentlySelectedCard.cardClass == Card.ecardClass.DAMAGE)  //DAMAGE
            {
                //NEED TO DO CHECKS FOR IF IN SHORT RANGE OR LONG RANGE.
                if (enemy.isBlocking == true)
                {
                    enemy.isBlocking = false;
                }
                else
                {
                    if (currentlySelectedCard.cardElement == Card.ecardElement.PHYSICAL)
                    {
                                          {
                            int currentDamage = Random.Range(currentlySelectedCard.minDamage * player.nextTurnPhysicalDamageMultiplier,
                                                                currentlySelectedCard.maxDamage * player.nextTurnPhysicalDamageMultiplier);

                            Debug.Log("You used " + currentlySelectedCard.cardName.ToString() + " for " + currentDamage + " damage.");
                            if (currentDamage == 0)
                            {
                                currentDamage++;
                            }
                            EnemyTakenDamage(currentDamage);
                        }
                    }
                    if (currentlySelectedCard.cardElement == Card.ecardElement.FIRE)
                    {
                        int currentDamage = Random.Range(currentlySelectedCard.minDamage * player.nextTurnFireDamageMultiplier,
                                                            currentlySelectedCard.maxDamage * player.nextTurnFireDamageMultiplier);
                        EnemyTakenDamage(currentDamage);
                        if (currentDamage == 0)
                        {
                            currentDamage++;
                        }
                        Debug.Log("You used " + currentlySelectedCard.cardName.ToString() + " for " + currentDamage + " damage.");
                    }

                }
            }
            else if (currentlySelectedCard.cardClass == Card.ecardClass.HEAL)   //HEAL
            {

                PlayerTakenDamage(-(Random.Range(currentlySelectedCard.minDamage, currentlySelectedCard.maxDamage)));
                if (player.playerHealth > 12)
                {
                    player.playerHealth = 12;
                }
            }
            else if (currentlySelectedCard.cardClass == Card.ecardClass.BLOCK)
            {
                player.isBlocking = true;
            }
            //Reset turn values.
            player.nextTurnFireDamageMultiplier = 1;
            player.nextTurnPhysicalDamageMultiplier = 1;

            if (currentlySelectedCard.cardClass == Card.ecardClass.NEXTTURNBOOST)   //BOOSTS, need to be after the resets.
            {
                //Will have to do these by name, because different effects
                if (currentlySelectedCard.cardName == Card.ecardName.OIL)
                {
                    player.nextTurnFireDamageMultiplier = 3;
                }
            }


            player.currentEnergy -= currentlySelectedCard.tier * 33;
            currentlySelectedCard.CreateCard(ChooseRandomCardFromList(player.currentDeck));
        }

    }
    public void PlayerTakenDamage(int damage)
    {
        player.PlayerTakenDamage(damage);
        player.SetHealthDisplay();
        playerHpText.text = "Player Health:" + player.playerHealth;

        if (player.playerHealth <= 0)
        {
            PlayerLost();
        }
    }
    public void EnemyTakenDamage(int damage)
    {
        enemy.EnemyTakenDamage(damage);
        enemy.SetHealthDisplay();
        enemyHpText.text = "Enemy Health: " + enemy.enemyHealth;

        if (enemy.enemyHealth <= 0)
        {
            PlayerWon();
            playerStats.playerHealth = player.playerHealth;
        }
    }
    Card.ecardName ChooseRandomCardFromList(List<Card.ecardName> availableCards)
    {
        int cardChosen = Random.Range(0, availableCards.Count);
        return availableCards[cardChosen];
    }

       
    void SelectCard(Card selectedCard)
    {
        if (currentlySelectedCard != null)
        {
            DeselectCard();
        }
        currentlySelectedCard = selectedCard;
        
        currentlySelectedCard.transform.localScale = Vector3.Lerp(currentlySelectedCard.transform.localScale, currentlySelectedCard.transform.localScale * 2, 1.5f);
        currentlySelectedCard.transform.position = Vector3.Lerp(currentlySelectedCard.transform.position, new Vector3(currentlySelectedCard.transform.position.x, -2.0f, -1.0f), 1.5f);
        currentlySelectedCard.GetComponent<BoxCollider2D>().size = new Vector2(8, 13);
    }
    void DeselectCard()
    {
        if (currentlySelectedCard != null)  //Check we wont break anything
        {
            currentlySelectedCard.transform.position = new Vector3(currentlySelectedCard.transform.position.x, -3.5f, 0.0f);
            currentlySelectedCard.transform.localScale = new Vector3(0.2f, 0.2f, 0.0f);
            currentlySelectedCard.GetComponent<BoxCollider2D>().size = new Vector2(8, 11);
            currentlySelectedCard = null;
        }
    }

        //ProcessTimeEvents Functions:
    void ProcessEnemyTurns()
    {
        ProcessEnemyCardUsing();
    }
    void ProcessEnemyCardUsing()
    {
        if (enemy.currentEnergy < enemy.currentCard.tier * 33)   //check the enemy has enough energy to play their card.
        {
            return;
        }
        else
        {
            Debug.Log("Enemy using: " + enemy.currentCard.cardName.ToString());
            //Enemy juice time
            FX_CardUse("Enemy");

            if (enemy.currentCard.cardClass == Card.ecardClass.DAMAGE)  //DAMAGE
            {
                if (enemy.currentCard.IsUseableShortRange == true && enemy.isInFrontLine == false)
                {
                    //float minDamage = enemy.currentCard.minDamage;
                    //float maxDamage = enemy.u
                    enemy.currentCard.minDamage /= 2;
                    enemy.currentCard.maxDamage /= 2;
                }
                else if (enemy.currentCard.IsUseableLongRange == true && enemy.isInFrontLine == true)
                {
                    enemy.currentCard.minDamage /= 2;
                    enemy.currentCard.maxDamage /= 2;
                }
                if (player.isBlocking == true)
                {
                    player.isBlocking = false;
                }
                else
                {
                    if (enemy.currentCard.cardElement == Card.ecardElement.PHYSICAL)
                    {
                        int damageTaken = Random.Range(enemy.currentCard.minDamage * player.nextTurnPhysicalDamageMultiplier,
                                                            enemy.currentCard.maxDamage * player.nextTurnPhysicalDamageMultiplier);
                        if (damageTaken == 0)
                        {
                            damageTaken++;
                        }
                        PlayerTakenDamage(damageTaken);
                        Debug.Log("It did " + damageTaken + " damage.");
                    }
                    if (enemy.currentCard.cardElement == Card.ecardElement.FIRE)
                    {
                        int damageTaken = Random.Range(enemy.currentCard.minDamage * player.nextTurnFireDamageMultiplier,
                                                            enemy.currentCard.maxDamage * player.nextTurnFireDamageMultiplier);
                        if (damageTaken == 0)
                        {
                            damageTaken++;
                        }
                        PlayerTakenDamage(damageTaken);
                        Debug.Log("It did " + damageTaken + " damage.");
                    }

                }
            }
            else if (enemy.currentCard.cardClass == Card.ecardClass.HEAL)   //HEAL
            {
                EnemyTakenDamage(-(Random.Range(enemy.currentCard.minDamage, enemy.currentCard.maxDamage)));
                if (enemy.enemyHealth > 12)
                {
                    enemy.enemyHealth = 12;
                }
            }
            else if (enemy.currentCard.cardClass == Card.ecardClass.BLOCK)
            {
                enemy.isBlocking = true;
            }
            enemy.nextTurnFireDamageMultiplier = 1;
            enemy.nextTurnPhysicalDamageMultiplier = 1;

            if (enemy.currentCard.cardClass == Card.ecardClass.NEXTTURNBOOST)
            {
                //Will have to do these by name, because different effects
                if (enemy.currentCard.cardName == Card.ecardName.OIL)
                {
                    enemy.nextTurnFireDamageMultiplier = 3;
                }
            }
            enemy.currentEnergy -= enemy.currentCard.tier * 33;
            enemy.currentCard.CreateCard(ChooseRandomCardFromList(enemy.currentDeck));
        }
    }
    void ProcessPlayerAndEnemyEnergy()
    {
        if (!GameOver)
        {
            if (player.currentEnergy < 100)
            {
                player.timeUntilNextEnergy -= Time.deltaTime;
                if (player.timeUntilNextEnergy <= 0)
                {
                    player.currentEnergy += 1;
                    playerEnergyText.text = "Player Energy%: " + player.currentEnergy;
                    player.timeUntilNextEnergy = player.timeRequiredForEnergyRegen;
                }
            }
            if (enemy.currentEnergy < 100)
            {
                enemy.timeUntilNextEnergy -= Time.deltaTime;
                if (enemy.timeUntilNextEnergy <= 0)
                {
                    enemy.currentEnergy += 1;
                    enemyEnergyText.text = "Enemy Energy%: " + enemy.currentEnergy;
                    enemy.timeUntilNextEnergy = enemy.timeRequiredForEnergyRegen;
                }
            }
            RectTransform playerEnergySliderRT = playerEnergySlider.GetComponent<RectTransform>();
            playerEnergySliderRT.anchoredPosition = new Vector3(2 * player.currentEnergy - 100, 0.0f, 0.0f);

            RectTransform enemyEnergySliderRT = enemyEnergySlider.GetComponent<RectTransform>();
            enemyEnergySliderRT.anchoredPosition = new Vector3(2 * enemy.currentEnergy - 100, 0.0f, 0.0f);
        }

    }

    //  ON START FUNCTIONS
    void SetPlayerAndEnemyStats()
    {
        playerHpText.text = "Player Health:" + player.playerHealth;
        enemyHpText.text = "Enemy Health: " + enemy.enemyHealth;
        if (player.isInFrontLine == true)
        {
            playerRangeLocationText.text = "Front Line";
        }
        else
        {
            playerRangeLocationText.text = "Back Line";
        }
        if (enemy.isInFrontLine == true)
        {
            enemyRangeLocationText.text = "Front Line";
        }
        else
        {
            enemyRangeLocationText.text = "Back Line";
        }

        SetUpPlayerCards();
        enemy.currentCard.CreateCard(ChooseRandomCardFromList(enemy.currentDeck));  //give enemy a random card from their deck to use first.

    }
    void SetUpPlayerCards()
    {
        cardHand = new List<Card>();

        //65 88

        for (int x = 0; x < 4; x++)
        {
            GameObject currentCard = Instantiate(cardPrefab, new Vector3(-4.0f + x * 2, -3.5f, 0.0f), Quaternion.identity, UICanvas.transform) as GameObject;
            Card currentCardClass = currentCard.GetComponent<Card>();
            RectTransform currentCardRT = currentCard.GetComponent<RectTransform>();
            currentCardRT.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            currentCardRT.anchoredPosition = new Vector3(-300.0f + x * 150, -150.0f, 0.0f);

            currentCardClass.combatManager = this;

            cardHand.Add(currentCardClass);

            //Turn it into a random card from the players deck and give it stats
            cardHand[x].CreateCard(ChooseRandomCardFromList(player.currentDeck)); //Picks a card from the list.
        }
    }
    void PlayRandomEnemyCard()
    {
        ProcessEnemyCardUsing();
    }

    void FX_CardUse(string user) //There's probably an enum or something for this
    {
        if (user == "Player")
        {
            enemyParticles.Emit(20);
        }else
        {
            playerParticles.Emit(20);
        }
    }

    // WIN OR LOSE STATES
    void PlayerWon()
    {
        winState.SetActive(true);
        GameOver = true;
        StartCoroutine(PlayerWonEnum());
    }

    void PlayerLost()
    {
        loseState.SetActive(true);
        GameOver = true;
        StartCoroutine(PlayerLostEnum());
    }

    IEnumerator PlayerWonEnum()
    {
        yield return new WaitForSeconds(3.0f);
        sceneChanger.SceneLoad("Test_Map");
    }

    IEnumerator PlayerLostEnum()
    {
        yield return new WaitForSeconds(3.0f);
        sceneChanger.SceneLoad("Test_Map");
    }
}

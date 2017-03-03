using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {
    //Current To Do: Get cards to negate energy
    //Then make cards createable within unity using picture sprites.
    //Then you can highlight useable cards etc so you can see when you can use them easier.

    //Prefabs
    public GameObject cardPrefab;

    //Important Game Objects
    public Player player;
    public Enemy enemy;

    //Player Combat Cards
    public List<Card> cardHand;
    public Card currentlySelectedCard;

    //UI Stuff
    public Text playerHpText;
    public Text enemyHpText;
    public Text playerEnergyText;

    // STARTING FUNCTIONS - READ FROM HERE!
    void Start ()
    {
        SetPlayerAndEnemyHp();
        SetUpCards();
    }	

	void Update ()
    {
        ProcessClicking();
        ProcessRayCasting();    //If we have frame issues, probably change this first.
        ProcessTimeEvents();
    }


    //  ON UPDATE FUNCTIONS
    void ProcessClicking()
    {
        //MOUSE INPUT (raycasting to gameobjects).
        if (Input.GetMouseButtonDown(0))
        {
            if (currentlySelectedCard != null) 
            {
                //currentlySelectedCard.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //Wanted to be able to drag. Will ask bf later :<
                ProcessPlayerCardUsing();
            }
        }
    }
    void ProcessRayCasting()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)   //If we hit something
        {
            // Card Selection
            if (hit.collider.gameObject.name == "Card(Clone)")
            {
                SelectCard(hit.collider.gameObject.GetComponent<Card>());
            }
            else
            {
                DeselectCard();
            }
        }
        else //If nothing hit by mouse raycast
        {
            DeselectCard();
        }
    }

    void ProcessTimeEvents()
    {
        ProcessEnemyTurns();
        ProcessPlayerEnergy();
    }

    //  ON UPDATE HELPER FUNCTIONS
        //Process Clicking functions:
    void ProcessPlayerCardUsing()
    {
        if (currentlySelectedCard.tier * 33 < player.currentEnergy) //if we have the energy to use the card
        {
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
                        EnemyTakenDamage(Random.Range(currentlySelectedCard.minDamage * player.nextTurnPhysicalDamageMultiplier,
                                                            currentlySelectedCard.maxDamage * player.nextTurnPhysicalDamageMultiplier));
                    }
                    if (currentlySelectedCard.cardElement == Card.ecardElement.FIRE)
                    {
                        EnemyTakenDamage(Random.Range(currentlySelectedCard.minDamage * player.nextTurnFireDamageMultiplier,
                                                            currentlySelectedCard.maxDamage * player.nextTurnFireDamageMultiplier));
                    }

                }
            }
            else if (currentlySelectedCard.cardClass == Card.ecardClass.HEAL)   //HEAL
            {
                PlayerTakenDamage(-(Random.Range(currentlySelectedCard.minDamage, currentlySelectedCard.maxDamage)));
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
                if (currentlySelectedCard.cardName == Card.ecardName.OILCARD)
                {
                    player.nextTurnFireDamageMultiplier = 2;
                }
            }


            player.currentEnergy -= currentlySelectedCard.tier * 33;
            currentlySelectedCard.CreateCard(ChooseRandomCardFromList(player.currentDeck));
        }

    }
    public void PlayerTakenDamage(int damage)
    {
        player.PlayerTakenDamage(damage);
        playerHpText.text = "Player Health:" + player.playerHealth;
    }
    public void EnemyTakenDamage(int damage)
    {
        enemy.EnemyTakenDamage(damage);
        enemyHpText.text = "Enemy Health: " + enemy.enemyHealth;
    }
    Card.ecardName ChooseRandomCardFromList(List<Card.ecardName> availableCards)
    {
        int cardChosen = Random.Range(0, availableCards.Count);
        return availableCards[cardChosen];
    }

        //Process RayCasting Functions:
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
        enemy.turnTimer -= Time.deltaTime;

        if (enemy.turnTimer <= 0.0f)
        {
            Debug.Log("Enemyturn");
            PlayRandomEnemyCard();
            enemy.turnTimer = enemy.totalTimeBetweenTurns;
        }
    }
    void ProcessEnemyCardUsing(Card currentcard)
    {
        if (currentcard.cardClass == Card.ecardClass.DAMAGE)  //DAMAGE
        {
            //NEED TO DO CHECKS FOR IF IN SHORT RANGE OR LONG RANGE.
            if (player.isBlocking == true)
            {
                player.isBlocking = false;
            }
            else
            {
                if (currentcard.cardElement == Card.ecardElement.PHYSICAL)
                {
                    PlayerTakenDamage(Random.Range(currentcard.minDamage * player.nextTurnPhysicalDamageMultiplier,
                                                        currentcard.maxDamage * player.nextTurnPhysicalDamageMultiplier));
                }
                if (currentcard.cardElement == Card.ecardElement.FIRE)
                {
                    PlayerTakenDamage(Random.Range(currentcard.minDamage * player.nextTurnFireDamageMultiplier,
                                                        currentcard.maxDamage * player.nextTurnFireDamageMultiplier));
                }

            }
        }
        else if (currentcard.cardClass == Card.ecardClass.HEAL)   //HEAL
        {
            EnemyTakenDamage(-(Random.Range(currentcard.minDamage, currentcard.maxDamage)));
        }
        else if (currentcard.cardClass == Card.ecardClass.BLOCK)
        {
            enemy.isBlocking = true;
        }
        else if (currentcard.cardClass == Card.ecardClass.NEXTTURNBOOST)
        {
            //Will have to do these by name, because different effects
            if (currentcard.cardName == Card.ecardName.OILCARD)
            {
                enemy.nextTurnFireDamageMultiplier = 2;
            }
        }
    }

    void ProcessPlayerEnergy()
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
    }

    //  ON START FUNCTIONS
    void SetPlayerAndEnemyHp()
    {
        playerHpText.text = "Player Health:" + player.playerHealth;
        enemyHpText.text = "Enemy Health: " + enemy.enemyHealth;
        Debug.Log(enemy.enemyHealth);
    }
    void SetUpCards()
    {
        cardHand = new List<Card>();

        for (int x = 0; x < 4; x++)
        { 
            GameObject currentCard = Instantiate(cardPrefab, new Vector3(-4.0f + x * 2, -3.5f, 0.0f), Quaternion.identity, transform) as GameObject;
            Card currentCardClass = currentCard.GetComponent<Card>();
            cardHand.Add(currentCardClass);

            //Turn it into a random card from the players deck and give it stats
            cardHand[x].CreateCard(ChooseRandomCardFromList(player.currentDeck)); //Picks a card from the list.
        }
    }
    void PlayRandomEnemyCard()
    {
        enemy.currentCard.CreateCard(ChooseRandomCardFromList(enemy.currentDeck));
        ProcessEnemyCardUsing(enemy.currentCard);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour {
    public Sprite splayerDeck;
    public GameObject playerDeck;
    public GameObject cardPrefab;
    public List<GameObject> cardObjects;

    List<Card> cardHand;

	// Use this for initialization
	void Start () {

        cardObjects = new List<GameObject>();
        cardHand = new List<Card>();

        Debug.Log(FindObjectOfType<Player>().health);

        for (int x = 0; x < 5; x++)
        {
            GameObject currentCard = Instantiate(cardPrefab);
            cardObjects.Add(currentCard);
            Card currentCardClass = currentCard.GetComponent<Card>();
            
            currentCardClass.CreateCard(Card.ecardName.ATTACKCARD);
            cardHand.Add(currentCardClass);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("o"))
        {
            FindObjectOfType<Player>().health -= 10.0f;
            Debug.Log("Player was hit!");
        }

        if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene("Test_Map");
        }
    }
}

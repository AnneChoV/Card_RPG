using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour {
    public GameObject playerDeck;
    public GameObject cardPrefab;

    public List<Card> cardHand;

    public Card currentlySelectedCard;

	// Use this for initialization
	void Start () {
        cardHand = new List<Card>();

        Debug.Log(FindObjectOfType<Player>().health);

        for (int x = 0; x < 4; x++)
        {
            GameObject currentCard = Instantiate(cardPrefab, new Vector3(-4.0f + x*2, -3.5f, 0.0f), Quaternion.identity, transform) as GameObject;
            Card currentCardClass = currentCard.GetComponent<Card>();
            cardHand.Add(currentCardClass);

            //Turn it into a random card and give it stats
            int cardChosen = Random.Range(1, 6);
            cardHand[x].CreateCard((Card.ecardName)cardChosen);
        }        
    }
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown("o"))
  //      {
  //          FindObjectOfType<Player>().health -= 10.0f;
  //          Debug.Log("Player was hit!");
  //      }

  //      if (Input.GetKeyDown("p"))
  //      {
  //          SceneManager.LoadScene("Test_Map");
  //      }


        //MOUSE INPUT (raycasting to gameobjects).
        if (Input.GetMouseButtonDown(0))
        { 
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)   //If we hit something
            {
                // Card Selection
                if (hit.collider.gameObject.name == "Card(Clone)")
                {
                    currentlySelectedCard = hit.collider.gameObject.GetComponent<Card>();
                }
                else
                {
                    currentlySelectedCard = null;
                }
            }
            else //If nothing hit by mouse raycast
            {
                currentlySelectedCard = null;
            }
        }
    }

    public void ProcessClick(Card clickedCard)
    {
        Debug.Log("Card Clicked!");
    }
}

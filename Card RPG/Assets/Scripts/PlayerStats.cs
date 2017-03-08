using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    public int playerHealth;

    int numFullHearts;

    public Sprite[] heartSprites;
    public Image[] heartDisplays;
    private GameObject[] hearts;

    public static PlayerStats instance = null;


    // Use this for initialization
    void Awake()
    {
        //Check if there is already an instance of GameStateManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of GameStateManager.
            Destroy(gameObject);

        //Set GameStateManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }
        // Use this for initialization
        void Start () {

        playerHealth = 12;
       
    }

    // Update is called once per frame
    void Update () {
       if (SceneManager.GetActiveScene().name == "Test_Map")
        {
            hearts = GameObject.FindGameObjectsWithTag("PlayerHeart");

            for (int i = 0; i < 4; i++)
            {
                heartDisplays[i] = hearts[i].GetComponent<Image>();

            }
            SetHealthDisplay();
        }
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

}

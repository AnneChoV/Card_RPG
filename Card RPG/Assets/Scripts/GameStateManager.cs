using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour {

    public static GameStateManager instance = null;

    public bool Map_1_to_3 = false;
    public bool Map_2_to_3 = false;
    public bool toMap1 = false;

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


}

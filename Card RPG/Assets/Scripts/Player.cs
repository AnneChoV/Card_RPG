using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float health;
    public Vector3 myTransform;

    public static Player instance = null;

    private void Awake()
    {
        // Don't destroy on load
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        health = 100.0f;
        Debug.Log("Starto");
	}
	
	// Update is called once per frame
	void Update () {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "Test_Combat")
        {
            myTransform = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
}

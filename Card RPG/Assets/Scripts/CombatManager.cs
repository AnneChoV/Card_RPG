using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(FindObjectOfType<Player>().health);
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

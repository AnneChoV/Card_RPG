using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(GameObject.FindGameObjectWithTag("Player").transform.position);
        Debug.Log(FindObjectOfType<Player>().health);
    }
	
	// Update is called once per frame
	void Update () {

    }
}

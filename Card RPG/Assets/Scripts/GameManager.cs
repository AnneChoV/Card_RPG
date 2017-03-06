﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("o"))
        {
            FindObjectOfType<PlayerStats>().playerHealth -= 10.0f;
        }

        if(Input.GetKeyDown("p"))
        {
            FindObjectOfType<EnemyStats>().enemyHealth -= 10.0f;
        }
    }
	

}

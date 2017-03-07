using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    SceneChanger sceneChanger;

	// Use this for initialization
	void Start () {
        sceneChanger = FindObjectOfType<SceneChanger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "ToMap_1")
        {
            sceneChanger.SceneLoad("Map_1");
        }

        if (gameObject.name == "ToMap_2")
        {
            sceneChanger.SceneLoad("Map_2");
        }

        if (gameObject.name == "ToMap_3")
        {
            sceneChanger.SceneLoad("Test_Map");
        }
    }
}

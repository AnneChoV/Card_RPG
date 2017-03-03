using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public float playerHealth;
    public float playerSpeed;

    public GameObject DialogueBox;

    EnemyStats enemy;

	// Use this for initialization
	void Start () {
        playerHealth = 100f;
        playerSpeed = 5.0f;

        enemy = FindObjectOfType<EnemyStats>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * playerSpeed * Time.deltaTime;

        if(Input.GetKeyDown("a"))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKeyDown("d"))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKeyDown("t"))
        {
            if(enemy.colWithPlayer == true)
            {
                DialogueBox.SetActive(true);
            }
        }
    }

}

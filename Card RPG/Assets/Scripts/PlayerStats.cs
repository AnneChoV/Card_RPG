using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private float minX, maxX, minY, maxY;

    public float playerHealth;
    public float playerSpeed;

    public GameObject DialogueBox;

    EnemyStats enemy;

    SceneChanger sceneChanger;
	// Use this for initialization
	void Start () {
        playerHealth = 100f;
        playerSpeed = 5.0f;

        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(-0.24f, -1.4f, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1.85f, 2.35f, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;

        enemy = FindObjectOfType<EnemyStats>();
        sceneChanger = FindObjectOfType<SceneChanger>();
	}
	
	// Update is called once per frame
	void Update () {

        // Get current position
        Vector3 pos = transform.position;

        // Horizontal contraint
        if (pos.x < minX) pos.x = minX;
        if (pos.x > maxX) pos.x = maxX;

        // vertical contraint
        if (pos.y < minY) pos.y = minY;
        if (pos.y > maxY) pos.y = maxY;

        // Update position
        transform.position = pos;


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

        if (Input.GetKeyDown("b"))
        {
            if (enemy.colWithPlayer == true)
            {
                sceneChanger.SceneLoad("Test_Combat");
            }
        }
    }

}

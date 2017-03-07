using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    private Vector3 min, max;

    public float playerHealth;
    public float playerSpeed;

    public GameObject DialogueBox;
    public BoxCollider2D PlayerBounds;

    EnemyStats enemy;

    SceneChanger sceneChanger;
    GameStateManager gameStateManager;

    private Vector3 Spawn_1 = new Vector3(-12.21f, 6.91f, 0);
    private Vector3 Spawn_2 = new Vector3(15.26f, 3.83f, 0);
    // Use this for initialization
    void Start () {
        playerHealth = 100f;
        playerSpeed = 5.0f;

        min.x = PlayerBounds.bounds.min.x;
        max.x = PlayerBounds.bounds.max.x;
        min.y = PlayerBounds.bounds.min.y;
        max.y = PlayerBounds.bounds.max.y;

        enemy = FindObjectOfType<EnemyStats>();
        sceneChanger = FindObjectOfType<SceneChanger>();

        gameStateManager = FindObjectOfType<GameStateManager>();

        if (gameStateManager.Map_1_to_3 == true && SceneManager.GetActiveScene().name == "Test_Map")
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = Spawn_1;
            gameStateManager.Map_1_to_3 = false;
        }

        if (gameStateManager.Map_2_to_3 == true && SceneManager.GetActiveScene().name == "Test_Map")
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = Spawn_2;
            gameStateManager.Map_2_to_3 = false;
        }
    }

    // Update is called once per frame
    void Update () {

        // Get current position
        Vector3 pos = transform.position;
        // Horizontal contraint
        if (pos.x < min.x) pos.x = min.x;
        if (pos.x > max.x) pos.x = max.x;

        // vertical contraint
        if (pos.y < min.y) pos.y = min.y;
        if (pos.y > max.y) pos.y = max.y;

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

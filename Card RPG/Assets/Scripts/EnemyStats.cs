using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public float enemyHealth;
    public float enemyMovementSpeed;
    public float enemyRotationSpeed;
    public BoxCollider2D PlayerBounds;

    public Transform myTransform;
    public Transform target; // enemy target point

    public bool colWithPlayer;
    SpriteRenderer spriteRenderer;

    private Vector3 min, max;

    private void Awake()
    {
        myTransform = transform;
    }

    // Use this for initialization
    void Start () {

        enemyHealth = 100f;
        enemyMovementSpeed = 1.0f;
        enemyRotationSpeed = 3.0f;

        min.x = PlayerBounds.bounds.min.x;
        max.x = PlayerBounds.bounds.max.x;
        min.y = PlayerBounds.bounds.min.y;
        max.y = PlayerBounds.bounds.max.y;

        target = GameObject.FindGameObjectWithTag("Player").transform; // Find player
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        GoToTarget();        
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colWithPlayer = true;
            transform.Find("Button").gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colWithPlayer = false;
            transform.Find("Button").gameObject.SetActive(false);
        }
    }


    void GoToTarget()
    {
        Vector3 targetDirection = GetTargetDirection(myTransform.position, target.position);
        if (targetDirection.x > 0.0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (targetDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (Vector3.Distance(transform.position, target.position) > 1.0f && colWithPlayer != true)
        {
            myTransform.position += targetDirection * enemyMovementSpeed * Time.deltaTime;
        }
    }

    Vector3 GetTargetDirection(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 targetDirection = (endPosition -  startPosition);
        targetDirection.Normalize();
        return targetDirection;
    }
}

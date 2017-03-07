using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public float enemyHealth;
    public float enemyMovementSpeed;
    public float enemyRotationSpeed;

    public Transform myTransform;
    public Transform target; // enemy target point

    public bool colWithPlayer;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        myTransform = transform;
    }

    // Use this for initialization
    void Start () {

        enemyHealth = 100f;
        enemyMovementSpeed = 3.0f;
        enemyRotationSpeed = 3.0f;

        target = GameObject.FindGameObjectWithTag("Player").transform; // Find player
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(target);

        //transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        //myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), enemyRotationSpeed * Time.deltaTime);
       //myTransform.position += myTransform.forward * enemyMovementSpeed * Time.deltaTime;



            //transform.Translate(new Vector3(enemyMovementSpeed * Time.deltaTime, 0, 0));
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

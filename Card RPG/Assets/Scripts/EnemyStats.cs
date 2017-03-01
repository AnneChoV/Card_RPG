﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public float enemyHealth;
    public float enemyMovementSpeed;
    public float enemyRotationSpeed;

    public Transform myTransform;
    public Transform target; // enemy target point

    private void Awake()
    {
        //myTransform = transform;
    }

    // Use this for initialization
    void Start () {

        enemyHealth = 100f;
        enemyMovementSpeed = 3.0f;
        enemyRotationSpeed = 3.0f;

        target = GameObject.FindGameObjectWithTag("Player").transform; // Find player
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        //myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), enemyRotationSpeed * Time.deltaTime);
        //myTransform.position += myTransform.forward * enemyMovementSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) > 1.0f)
        {
            transform.Translate(new Vector3(enemyMovementSpeed * Time.deltaTime, 0, 0));
        }
    }
}

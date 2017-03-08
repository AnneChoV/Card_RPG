using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoving : MonoBehaviour {
    private float actualRankMoveLerpSpeed;
    public float targetRankMoveLerpSpeed;
    float _timeStartedLerping;

    bool goingLeft;

    Vector3 position1;
    Vector3 position2;
    // Use this for initialization
    void Start () {
        position1 = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
        position2 = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
        goingLeft = true;
        targetRankMoveLerpSpeed = 8;
    }

    // Update is called once per frame
    void Update()
    {
        actualRankMoveLerpSpeed = Mathf.Lerp(actualRankMoveLerpSpeed, targetRankMoveLerpSpeed, 2.0f * Time.deltaTime);

        if (goingLeft == true)
        {
            transform.position = Vector3.Lerp(transform.position, position1, actualRankMoveLerpSpeed * Time.deltaTime);
            if (transform.position.x <= position1.x + 0.1f)
            {
                goingLeft = false;
            }
        }
        else if (goingLeft == false)
        {
            transform.position = Vector3.Lerp(transform.position, position2, actualRankMoveLerpSpeed * Time.deltaTime);
            if (transform.position.x >= position2.x - 0.1f)
            {
                goingLeft = true;
            }
        }

    }
}   

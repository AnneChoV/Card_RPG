using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {


    //PRIVATE VARIABLES:
    enum ecardClass
    {
        DAMAGE, //normal attack and fire for now - fire long attack short
        HEAL,
        BLOCK,
        NEXTTURNBOOST
    }

    enum ecardElement
    {
        PHYSICAL,
        FIRE
    }

    //not sure how to do the image. Anne Senpai?
    int tier;
    string description;
    ecardClass cardClass;
    ecardElement cardElement;



	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    ecardClass GetCardClass ()
    {
        return cardClass;
    }

}

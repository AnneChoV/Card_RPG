using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardColouring : MonoBehaviour {
    //Hacky as fuck, but it gets the point across I hope
    [Header("Colour Settings, make sure to tag children")]
    //This stuff should probably be set up globally in a manager or something
    //If there's any way to get these colours from a sprite or a texture, that would be *f a b*
    public Color CurseBack;
    public Color CurseField;
    public Color CurseText;

    public Color FireBack;
    public Color FireField;
    public Color FireText;

    [Header("Card Type Index")]
    //For testing, 0 = Curse, 1 = Fire
    public int TypeID;

	void Start () {
		//Apply chosen colour to base
        foreach(Transform child in transform)
        {
            if(child.gameObject.tag == "CardBase")
            {
                Image childRenderer = child.GetComponent<Image>();
                if (TypeID == 0)
                {
                    childRenderer.color = CurseBack;
                }else
                {
                    childRenderer.color = FireBack;
                }
            }
        }

        //Apply chosen colour to field
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "CardField")
            {
                Image childRenderer = child.GetComponent<Image>();
                if (TypeID == 0)
                {
                    childRenderer.color = CurseField;
                }
                else
                {
                    childRenderer.color = FireField;
                }
            }
        }

        //Apply chosen colour to text/icons
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "CardText")
            {
                Image childRenderer = child.GetComponent<Image>();
                if (TypeID == 0)
                {
                    childRenderer.color = CurseText;
                }
                else
                {
                    childRenderer.color = FireText;
                }
            }
        }
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}

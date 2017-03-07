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

    public Color RestoreBack;
    public Color RestoreField;
    public Color RestoreText;

    public Color PhysicalBack;
    public Color PhysicalField;
    public Color PhysicalText;


[Header("Card Type Index")]
    //For testing, 0 = Curse, 1 = Fire
    public int TypeID;

	void Start () {
		//Apply chosen colour to base

    }
    
	
	// Update is called once per frame
	void Update () {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "CardBase")
            {
                Image childRenderer = child.GetComponent<Image>();
                if (TypeID == 0)
                {
                    childRenderer.color = CurseBack;
                }
                else if (TypeID == 1)
                {
                    childRenderer.color = FireBack;
                }
                else if (TypeID == 2)
                {
                    childRenderer.color = RestoreBack;
                }
                else if (TypeID == 3)
                {
                    childRenderer.color = PhysicalBack;
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
                else if (TypeID == 1)
                {
                    childRenderer.color = FireField;
                }
                else if (TypeID == 2)
                {
                    childRenderer.color = RestoreField;
                }
                else if (TypeID == 3)
                {
                    childRenderer.color = PhysicalField;
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
                else if (TypeID == 1)
                {
                    childRenderer.color = FireText;
                }
                else if (TypeID == 2)
                {
                    childRenderer.color = RestoreText;
                }
                else if (TypeID == 3)
                {
                    childRenderer.color = PhysicalText;
                }
            }
        }
    }
}

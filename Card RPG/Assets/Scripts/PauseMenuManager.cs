using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {

    public GameObject InventoryPanel;
    public GameObject CardPanel;
    public GameObject OptionPanel;
    public GameObject QuitPanel;

    public GameObject PanelButtons;

    private bool ButtonState = false;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PauseMenuButton()
    {
        if (ButtonState == false)
        {
            InventoryPanel.SetActive(true);
            PanelButtons.SetActive(true);

            ButtonState = true;
        }
        else
        {
            InventoryPanel.SetActive(false);
            CardPanel.SetActive(false);
            OptionPanel.SetActive(false);
            QuitPanel.SetActive(false);

            PanelButtons.SetActive(false);

            ButtonState = false;
        }
    }


    public void InventoryButton()
    {
        if (InventoryPanel.activeInHierarchy == false)
        {
            InventoryPanel.SetActive(true);
        }

        if (CardPanel.activeInHierarchy == true)
        {
            CardPanel.SetActive(false);
        }

        if (OptionPanel.activeInHierarchy == true)
        {
            OptionPanel.SetActive(false);
        }

        if (QuitPanel.activeInHierarchy == true)
        {
            QuitPanel.SetActive(false);
        }
    }

    public void CardsButton()
    {
        if (InventoryPanel.activeInHierarchy == true)
        {
            InventoryPanel.SetActive(false);
        }

        if (CardPanel.activeInHierarchy == false)
        {
            CardPanel.SetActive(true);
        }

        if (OptionPanel.activeInHierarchy == true)
        {
            OptionPanel.SetActive(false);
        }

        if (QuitPanel.activeInHierarchy == true)
        {
            QuitPanel.SetActive(false);
        }
    }

    public void OptionsButton()
    {
        if (InventoryPanel.activeInHierarchy == true)
        {
            InventoryPanel.SetActive(false);
        }

        if (CardPanel.activeInHierarchy == true)
        {
            CardPanel.SetActive(false);
        }

        if (OptionPanel.activeInHierarchy == false)
        {
            OptionPanel.SetActive(true);
        }

        if (QuitPanel.activeInHierarchy == true)
        {
            QuitPanel.SetActive(false);
        }
    }

    public void QuitButton()
    {
        if (InventoryPanel.activeInHierarchy == true)
        {
            InventoryPanel.SetActive(false);
        }

        if (CardPanel.activeInHierarchy == true)
        {
            CardPanel.SetActive(false);
        }

        if (OptionPanel.activeInHierarchy == true)
        {
            OptionPanel.SetActive(false);
        }

        if (QuitPanel.activeInHierarchy == false)
        {
            QuitPanel.SetActive(true);
        }
    }
}

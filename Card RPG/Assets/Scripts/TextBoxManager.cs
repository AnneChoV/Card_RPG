using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
    public Text text;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerStats player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerStats>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        text.text = textLines[currentLine];
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }	

        if (currentLine > endAtLine)
        {
            textBox.SetActive(false);
            currentLine = 0;
        }
	}
}

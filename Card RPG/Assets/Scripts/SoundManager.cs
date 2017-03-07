using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public AudioSource themeSource;
    public AudioSource efxSource;

    public AudioClip mainTheme;
    public AudioClip battleTheme;

    private AudioClip selectedTheme;
    //public AudioClip[] starClicks;

    public static SoundManager instance = null;

    private bool battleOver = false;

    // Use this for initialization
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            selectedTheme = mainTheme;
        }

        if (SceneManager.GetActiveScene().name == "Test_Map")
        {
            selectedTheme = mainTheme;
        }

        if (SceneManager.GetActiveScene().name == "Not not Combat Scene")
        {
            selectedTheme = battleTheme;
        }

        efxSource = GameObject.Find("Game Manager").GetComponent<AudioSource>();
        themeSource = GameObject.Find("Game Manager").GetComponent<AudioSource>();

        themeSource.PlayOneShot(selectedTheme);

        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }


    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        ////Set the clip of our efxSource audio source to the clip passed in as a parameter.
        //efxSource.clip = clip;

        //Play the clip.
        efxSource.PlayOneShot(clip);
    }

    //public void PlayStar()
    //{
    //    int randomClick = Random.Range(0, starClicks.Length);
    //    AudioClip starClick = starClicks[randomClick];
    //    efxSource.PlayOneShot(starClick);
    //}

    public void playTheme(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            selectedTheme = mainTheme;
        }

        if (sceneName == "Test_Map")
        {
            if (battleOver == true)
            {
                themeSource.Stop();
                selectedTheme = mainTheme;
                themeSource.PlayOneShot(selectedTheme);
                Debug.Log("Map");
            }
        }

        if (sceneName == "Test_Combat")
        {
            themeSource.Stop();
            selectedTheme = battleTheme;
            themeSource.PlayOneShot(selectedTheme);
            Debug.Log("Combat");
        }
    }

}

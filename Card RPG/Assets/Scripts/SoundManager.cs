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

    public AudioClip buttonClick;
    public AudioClip dialogueSound;
    public AudioClip stanceChange;
    public AudioClip rangedAttack;
    public AudioClip meleeAttack;
    public AudioClip unconcious;
    public AudioClip block;
    public AudioClip heal;
    public AudioClip oil;
    public AudioClip fire;

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


    public void ButtonClick()
    {
        efxSource.PlayOneShot(buttonClick);
    }

    public void DialogueSound()
    {
        efxSource.PlayOneShot(dialogueSound, 3.0f);
    }

    public void StanceChange()
    {
        efxSource.PlayOneShot(stanceChange);
    }
    public void RangedAttack()
    {
        efxSource.PlayOneShot(rangedAttack);
    }
    public void MeleeAttack()
    {
        efxSource.PlayOneShot(meleeAttack);
    }
    public void Unconcious()
    {
        efxSource.PlayOneShot(unconcious);
    }

    public void Block()
    {
        efxSource.PlayOneShot(block);
    }
    public void Heal()
    {
        efxSource.PlayOneShot(heal);
    }
    public void Oil()
    {
        efxSource.PlayOneShot(oil);
    }
    public void Fire()
    {
        efxSource.PlayOneShot(fire);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    SoundManager soundManager;
    GameStateManager gameStateManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        gameStateManager = FindObjectOfType<GameStateManager>();
    }

    IEnumerator Fading()
    {
        float fadeTime = GameObject.Find("Game Manager").GetComponent<Fader>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
    }


    public void SceneLoad(string SceneName)
    {
        Fading();
        SceneManager.LoadScene(SceneName);
        soundManager.playTheme(SceneName);

        if (SceneName == "Map_1")
        {
            gameStateManager.Map_1_to_3 = true;
        }

        if (SceneName == "Map_2")
        {
            gameStateManager.Map_2_to_3 = true;
        }
    }

    public void restartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ContinueButton()
    {
        SceneLoad("Test_Map");
    }

    public void NewButton()
    {

    }

    public void LoadButton()
    {

    }

    public void OptionsButton()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}

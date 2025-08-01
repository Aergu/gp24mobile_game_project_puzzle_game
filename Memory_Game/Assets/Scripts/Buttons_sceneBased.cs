using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class MainMenuController : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        // Load the game scene (make sure it's added in Build Settings)
        SceneManager.LoadScene("GameScene");
    }

    public void OnOptionsButtonPressed()
    {
        // Load the options scene or open options panel
        SceneManager.LoadScene("OptionsScene");
        // Alternatively, show an options panel instead of a scene
        // optionsPanel.SetActive(true);
    }

    public void OnExitButtonPressed()
    {
        // Quit app
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, Playing, Options, Paused }

public class GameManager : MonoBehaviour
{
    public GameState currentState;

    public void StartGame()
    {
        currentState = GameState.Playing;
        // Hide menus, enable game logic
    }

    public void OpenOptions()
    {
        currentState = GameState.Options;
        // Show options panel
    }

    public void ReturnToMenu()
    {
        currentState = GameState.MainMenu;
        // Show Main menu
    }
}

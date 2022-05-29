using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool inMenu = true;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        inMenu = false;
    }

    public void QuitGame()
    {
        Debug.Log("clicked");
        Application.Quit();
    }

    public bool InMenu()
    {
        return inMenu;
    }
}

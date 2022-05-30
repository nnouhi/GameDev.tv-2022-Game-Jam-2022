using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool inMenu = true;
    [SerializeField] private GameObject InfoMenu;
    [SerializeField] private GameObject EndingMenu;
    
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

    public void DisplayInfo()
    {
        InfoMenu.SetActive(true);
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

    public void DisplayEnding()
    {
        EndingMenu.SetActive(true);
    }

    public void ResetGame()
    {

        SceneManager.LoadScene(0);
    }
}
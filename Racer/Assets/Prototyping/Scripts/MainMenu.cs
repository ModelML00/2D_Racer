using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject credits;

    public void LoadLevel(int level) {
        SceneManager.LoadScene(level);
    }

    public void CreditsScreen() {
        credits.SetActive(true);
        menu.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Back() {
        credits.SetActive(false);
        menu.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject quitPrompt;

    bool gameIsPaused;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(!gameIsPaused) {
                Pause();
            } else {
                Resume();
            }
        }
    }

    void Pause() {
        gameIsPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        gameIsPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitPrompt() {
        if(!quitPrompt.activeInHierarchy) {
            quitPrompt.SetActive(true);
        } else {
            quitPrompt.SetActive(false);
        }

    }

    public void QuitMenu() {
        SceneManager.LoadScene(0);
    }

    public void QuitDesktop() {
        Application.Quit();
    }
}

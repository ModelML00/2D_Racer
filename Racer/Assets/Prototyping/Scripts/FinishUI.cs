using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishUI : MonoBehaviour
{

    public float fadeSpeed;

    public float slowDownSpeed;

    public Image finishBackground;
    public Text playerTime;
    public Text bestTime;

    public GameObject finishText;
    public GameObject bestTimeText;
    public GameObject newBestTimeText;
    public GameObject finalTimeText;

    public HUDCotrol HUD;

    public IEnumerator FinishSequence() {
        HUD.trackTime = false;
        finishText.SetActive(true);
        StartCoroutine(SlowTime());

        float opacity = 0f;
        float t = 0f;
        while(finishBackground.color.a < 1f) {
            t += fadeSpeed * Time.unscaledDeltaTime;
            opacity = Mathf.Lerp(0f, 1f, t);
            finishBackground.color = new Color(0f, 0f, 0f, opacity);
            yield return null;
        }

        finalTimeText.SetActive(true);
        float finalTime = (HUD.minutes * 60f) + HUD.currentTime;
        yield return new WaitForSecondsRealtime(2f);
        
        playerTime.text = HUD.timer.text;
        playerTime.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);

        if(finalTime < PlayerPrefs.GetFloat("Best_Time", 0f) || PlayerPrefs.GetFloat("Best_Time", 0f) == 0f) {
            newBestTimeText.SetActive(true);
            PlayerPrefs.SetFloat("Best_Time", finalTime);
        } else {
            bestTimeText.SetActive(true);
        }
        yield return new WaitForSecondsRealtime(2f);

        float currentBestTime = PlayerPrefs.GetFloat("Best_Time", 0f);
        
        float minutes = 0f;
        if(currentBestTime > 60f) {
            minutes = (currentBestTime / 60f);
            float minCheck = Mathf.Round(minutes);
            if(minCheck > minutes) {
                minutes = minCheck - 1f;
            } else {
                minutes = minCheck;
            }
        }
        float seconds = currentBestTime - (minutes * 60f);
        bestTime.text = minutes.ToString("00") + " : " + seconds.ToString("00.00");
        bestTime.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(10f);

        ReturnToMainMenu();
        Time.timeScale = 1f;
    }

    IEnumerator SlowTime() {
        float t = 0f;
        while(Time.timeScale > 0f) {
            t += slowDownSpeed * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
    }


    void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }

    [ContextMenu("Reset Best Time")]
    public void ResetBestTime() {
        PlayerPrefs.SetFloat("Best_Time", 0f);
    }


}

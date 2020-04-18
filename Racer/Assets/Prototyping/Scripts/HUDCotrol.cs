using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCotrol : MonoBehaviour
{
    public Text timer;

    public Text speedometer;

    float currentTime;

    float minutes;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= 60f) {
            currentTime = 0f;
            minutes++;
        }

        timer.text = minutes.ToString("00") + " : " + currentTime.ToString("00.00");
        speedometer.text = Movement.speed.ToString("000");
    }

    float SaveTime() {
        float timeToSave = minutes * 60f + currentTime;
        return timeToSave;
    }
}

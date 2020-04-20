using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCotrol : MonoBehaviour
{
    public Text timer;

    public Text speedometer;

    public Transform speedometerHand;

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

        speedometerHand.rotation = Quaternion.Euler(0f, 0f, -90f * Movement.speedometerSpeed);
    }

    float SaveTime() {
        float timeToSave = minutes * 60f + currentTime;
        return timeToSave;
    }
}

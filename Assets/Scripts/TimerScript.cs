using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI completionTime;
    float elapsedTime;
  
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes=Mathf.FloorToInt(elapsedTime/60);
        int seconda=Mathf.FloorToInt(elapsedTime%60);
        timerText.text=string.Format("{0:00}:{1:00}",minutes,seconda);
        completionTime.text = timerText.text;
    }
}

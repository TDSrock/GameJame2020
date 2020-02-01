using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI startText;
    public AudioSource clickSound;
    private bool timeLeft = true;

    private void Start() {
       string minutes = Mathf.Floor(timer / 60).ToString("00");
       string seconds = (timer % 60).ToString("00");
       StartCoroutine(ChangeTime());
    }

    void FixedUpdate() {
        timer -= Time.deltaTime;
        if (timeLeft) {
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = Mathf.Floor(timer % 60).ToString("00");
            startText.text = minutes + ":" + seconds;
            if (timer < 1) {
                timeLeft = false;
                //FANCY EVENT DIE ER NOG IN MOET
            }
        }
        

    }

    IEnumerator ChangeTime() {

        while (timeLeft) {
            clickSound.Play();
            yield return new WaitForSecondsRealtime(1);
            
        }
    }

}

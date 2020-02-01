using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerRectangle : MonoBehaviour
{
    public float startTimer;
    private float timer;
    public Image ImageRectangle;
    private bool timeLeft = true;

    private void Start() {
        timer = startTimer;
    }

    void Update() {
        
        if (timeLeft) {
            timer -= Time.deltaTime;
            ImageRectangle.fillAmount = timer / startTimer;
            if (timer < 0) {
                timeLeft = false;
                //FANCY EVENT DIE ER NOG IN MOET
            }
            

        }


    }
}

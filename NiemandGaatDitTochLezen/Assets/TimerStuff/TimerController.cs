using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI gameTimerText;
    public int countDownTimer;
    public int gameTimer;
    public AudioSource clickSound;
    public GameObject countDownObject;
    private bool timeLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        string minutes = Mathf.Floor(gameTimer / 60).ToString("00");
        string seconds = (gameTimer % 60).ToString("00");
        gameTimerText.text = minutes + ":" + seconds;
       
        StartCoroutine(StartCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartCountDown() {
        while (countDownTimer > 0) {
            countDownTimer--;
            countDownText.text = Mathf.Floor(countDownTimer).ToString("0");
            clickSound.Play();
            yield return new WaitForSeconds(1);
        }
        countDownObject.SetActive(false);
        StartCoroutine(StartGameTimer());


    }

    IEnumerator StartGameTimer() {
        while (gameTimer > 0) {
            gameTimer--;
            string minutes = Mathf.Floor(gameTimer / 60).ToString("00");
            string seconds = Mathf.Floor(gameTimer % 60).ToString("00");
            gameTimerText.text = minutes + ":" + seconds;
            clickSound.Play();
            yield return new WaitForSeconds(1);
        }
        // do fancy event
    }

}

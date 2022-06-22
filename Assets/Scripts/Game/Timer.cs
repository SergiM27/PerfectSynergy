using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Timer : MonoBehaviour
{
    public Text textTimerSeconds;
    public Text textTimerMiliseconds;
    public bool startTimer;
    public float time;
    public GameObject gameOverMenu;
    public GameObject[] clickEvents;

    private void Start()
    {
        textTimerSeconds.text = "5";
        textTimerMiliseconds.text = "00";
        startTimer = false;
        gameOverMenu.SetActive(false);
    }

    void Update()
    {
        if (startTimer)
        {
            float t = time = time - Time.deltaTime;
            string seconds = ((int)t % 60).ToString("0");
            string miliseconds = ((t - (int)t) *100).ToString("00");

            if (t <= 0.00f)
            {
                //invocar gameover
                textTimerSeconds.text = "0";
                textTimerMiliseconds.text = "00";
                enabled = false;
                foreach (GameObject eventTrigger in clickEvents)
                {
                    eventTrigger.gameObject.GetComponent<Button>().enabled=false;
                }
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().GameOverTimer();
                GameManager.gameOver = true;
            }
            else
            {
                textTimerSeconds.text = seconds;
                textTimerMiliseconds.text = miliseconds;
            }
        }

    }
}
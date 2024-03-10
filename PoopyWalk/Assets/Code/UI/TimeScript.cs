using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeScript : MonoBehaviour
{
    public Text timerText;
    private float initialTime = 90.0f;
    public float levelOneCountdown = 90.0f;
    public bool stopTimer = false;
    public static TimeScript instance;
    public GameObject dead,live,pop;
    void Start() {
        UpdateTimer(levelOneCountdown);
        if(instance == null)
          instance = this;
        live.SetActive(true);
        dead.SetActive(false);
         pop.SetActive(false);
    }

    private void UpdateTimer(float timer) {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        if (minutes == 0 && seconds == 30) {
            timerText.color = Color.red;
        }
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float ObtainTime() {
        return initialTime - levelOneCountdown;
    }

    private void GameOver(){

    }

    void Update() {
        if (levelOneCountdown > 0.0f && !stopTimer) {
            levelOneCountdown -= Time.deltaTime;
            UpdateTimer(levelOneCountdown);
        }else{
            //Debug.Log("Tutorial pass!");
            dead.SetActive(true);
            live.SetActive(false);
            pop.SetActive(true);
            //SceneManager.LoadScene(1);
        }
    }
}

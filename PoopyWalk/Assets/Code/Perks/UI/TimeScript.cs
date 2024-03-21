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
    public AnimationClip nuevoClip;
    public Animator animator;
    public PlayerController player;
    public GameObject image;
    public Button boton;
    void Start() {
        UpdateTimer(levelOneCountdown);
        if(instance == null)
          instance = this;
          
          boton.gameObject.SetActive(false);
          image.SetActive(false);
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
            //Debug.Log("Tutorial pass!");
            
        }else{
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

           
            animator.Play(nuevoClip.name, 0, currentState.normalizedTime); 
            player.isPause=true;
            if(!player.wins){
            image.SetActive(true);
            
            boton.gameObject.SetActive(true);
            

            }
            

        }
    }
}
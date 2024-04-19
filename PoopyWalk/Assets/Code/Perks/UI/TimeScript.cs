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
    public GameObject image,pop;
    public Button boton;
    public int countgameover=0;
    public bool gameove=false;
    public AudioClip audioClip; 
     private AudioSource audioSource;
    void Start() {
        UpdateTimer(levelOneCountdown);
        if(instance == null)
          instance = this;
          
          boton.gameObject.SetActive(false);
          image.SetActive(false);
          pop.SetActive(false);
          gameove=false;
          audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Si no hay AudioSource, lo creamos y lo adjuntamos al mismo GameObject
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Asigna el clip de audio al AudioSource
        audioSource.clip = audioClip;

        // Desactiva el AudioSource al inicio
        audioSource.enabled = false;
    }

    private void UpdateTimer(float timer) {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        if (minutes == 0 && seconds == 15) {
            timerText.color = Color.red;
            if (!audioSource.enabled)
            {
                audioSource.enabled = true;
                audioSource.Play();
                
            }
        }
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float ObtainTime() {
        return initialTime - levelOneCountdown;
    }

    private void GameOver(){

    }

    void Update() {
        
        if (levelOneCountdown >= 1.0f && !stopTimer) {
            levelOneCountdown -= Time.deltaTime;
            UpdateTimer(levelOneCountdown);
            //Debug.Log("Tutorial pass!");
             
        }else{
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
             
           //audioSource.enabled = false;
            animator.Play(nuevoClip.name, 0, currentState.normalizedTime); 
            player.isPause=true;
            pop.SetActive(true);
            countgameover++;
            if(countgameover>150){

                gameove=true;
                countgameover=0;

            }


            if(!player.wins&&gameove){
            image.SetActive(true);
            
            boton.gameObject.SetActive(true);
            

            }
            

        }
    }
}

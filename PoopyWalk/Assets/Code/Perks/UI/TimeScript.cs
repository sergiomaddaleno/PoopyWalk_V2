using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeScript : MonoBehaviour
{
    public Text timerText;

    public float levelOneCountdown = 75.0f;
    public bool stopTimer = false;
    public static TimeScript instance;
    public AnimationClip nuevoClip;
    public Animator animator;
    public PlayerController player;
    public GameObject image,pop;
    public Button boton;
    public Button play_again_boton;

    public GameObject red_border;
    public float blinkInterval = 0.5f; 
    private Coroutine blinkCoroutine;

    public int countgameover=0;
    public bool gameove=false;
    public AudioClip audioClip; 
     private AudioSource audioSource;
    void Start() {
        UpdateTimer(levelOneCountdown);
        if(instance == null)
          instance = this;
          
          boton.gameObject.SetActive(false);
          play_again_boton.gameObject.SetActive(false);

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

            red_border.SetActive(false);

    }

    private void UpdateTimer(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        if (minutes == 0 && seconds == 15)
        {
            timerText.color = Color.red;
            red_border.SetActive(true);
            if (blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkBorder());
            }
            if (!audioSource.enabled)
            {
                audioSource.enabled = true;
                audioSource.Play();
            }
        }
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator BlinkBorder()
    {
        while (true)
        {
            red_border.SetActive(!red_border.activeSelf); // Alterna entre activar y desactivar el borde
            yield return new WaitForSeconds(blinkInterval); // Espera el intervalo de parpadeo
        }
    }


    public float ObtainTime() {
        return levelOneCountdown;
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
            play_again_boton.gameObject.SetActive(true);
            

            }
            

        }
    }
}

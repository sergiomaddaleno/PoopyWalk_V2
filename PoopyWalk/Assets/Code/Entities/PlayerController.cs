using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public GameObject fartVFX, pauseMenu,Wc,win,game;
    public Transform player;
    public Button boton;
    public Button play_again_boton;

    public SpriteRenderer sprite;
    public Animator animator;
    public LayerMask groundMask;
    public Rigidbody2D rb;
    public ScreenPoopScript screen;
    public GameDataController gm;
    public float speed = 7.0f, fuel = 0.0f;
    private bool grounded = false;
    public bool timetopluge; 
    public float plugtime;
    public bool isPause = false;
    public bool timeslow;
    public float timetoslow;
    public GameObject plug;
    public ParticleSystem perk_particles;

    public AudioSource[] audio;
    public AudioClip[] audioclip;
    public int timetomessage=8000;
    public bool showmessage=true;
    public Joystick movementJoystick;
    public float joystickSpeedMultiplier = 50.0f;
    public bool wins;
    public int countstars;
    public CountLevelStars count_level_stars;
    private bool UI_jump;
    public bool UI_pause;
    public TimeScript instance;
     string sceneName;
     public Color greyColor = new Color(0.5f, 0.5f, 0.5f, 1f); 
      public float greyTintStrength = 0.5f;
      private Color originalColor = Color.white; 
    
    /*
    1 -- Pedo impulso
    2 -- Kaka
    3 -- ouch
    4 -- Pedo menu
    5 -- Closing Door
    */


    void Start(){
        timeslow = false;
        timetoslow = 500.0f;
        plug.SetActive(false);
        timetopluge = false;
        plugtime = 400;
        UI_jump = false;
        UI_pause = false;
        boton.gameObject.SetActive(false);
        play_again_boton.gameObject.SetActive(false);

        win.SetActive(false);

        wins=false;
        countstars=0;
        sceneName = SceneManager.GetActiveScene().name;
        originalColor = sprite.color;
    }


    void Update() {

        if(!isPause){

        }
        float horizontalInput = movementJoystick.Direction.x;
        Vector3 horizontalMovement = new Vector3(horizontalInput, 0, 0);
        
        Vector3 movement = horizontalMovement;
        if(!isPause){

            transform.position += movement * speed * joystickSpeedMultiplier * Time.deltaTime;
        }

        if (Mathf.Abs(horizontalInput) > 0) {
            sprite.flipX = horizontalInput < 0;
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !isPause) {
            this.transform.position += Vector3.left * speed * Time.deltaTime;
            sprite.flipX = true;
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !isPause) {
            this.transform.position += Vector3.right * speed * Time.deltaTime;
            transform.position += movement * speed * joystickSpeedMultiplier * Time.deltaTime;
            sprite.flipX = false;
        }

        if (Mathf.Abs(horizontalInput) > 0 || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
            animator.SetBool("Running", true);
        }else{
            animator.SetBool("Running", false);
        }

       

        if (timetopluge)
        {

            Vector3 posicionActual = plug.transform.position;
          posicionActual.y += 0.05f;
          plug.transform.position = posicionActual;
            plugtime--;
            if (plug.transform.position.y >= 12.0f)
            {

                timetopluge = false;
                plugtime = 400;
                plug.SetActive(false);
                 
            }

        }

        if(timetomessage>5800){
          Wc.SetActive(true);
          
        }else{
             Wc.SetActive(false);
             if(timetomessage<=0){
                timetomessage=8000;
             }
        }
        
        timetomessage--;


      
        Vector3 position = player.transform.position;
        position.y = Mathf.Clamp(position.y, -2.0f, 10.0f);
        transform.position = position;

        grounded = Physics2D.Raycast(this.transform.position, Vector2.down, 2.0f, groundMask.value);
        animator.SetBool("Flying", !grounded);

        if ((Input.GetKeyDown(KeyCode.Space) || UI_jump) && fuel > 0.0f && !isPause) {
            UI_jump = false;
            rb.velocity = new Vector2(rb.velocity.x, 12.0f);
            fuel -= 25.0f;
            Instantiate(fartVFX, this.transform.position, Quaternion.identity);
            audio[0].PlayOneShot(audioclip[0]);
        }

        if (Input.GetKeyDown(KeyCode.P) || UI_pause) {
            UI_pause = true;
            Time.timeScale = 0.0f;
            isPause = true;
            pauseMenu.SetActive(true);
        }

        

        
    }

    public IEnumerator ChangeScenes()
    {
      yield return new WaitForSeconds(1.0f);
      audio[0].PlayOneShot(audioclip[1]);
      yield return new WaitForSeconds(2.5f);
      SceneManager.LoadScene(2);
    }

    public void OnCollisionEnter2D(Collision2D coll) {

        if(coll.gameObject.CompareTag("SodaPerk")){
          Destroy(coll.gameObject);
          if (fuel < 100.0f) {
              fuel += 100.0f - fuel;
          }
          Instantiate(perk_particles, transform.position, Quaternion.identity);
        }

        if (coll.gameObject.CompareTag("PoopEnemy")) {
            screen.isDirty = true;
        }

        if (coll.gameObject.CompareTag("PaperPerk")) {
            Destroy(coll.gameObject);
            screen.isPaper = true;
          Instantiate(perk_particles, transform.position, Quaternion.identity);

        }
        if (coll.gameObject.CompareTag("CorkPerk")) {
          Destroy(coll.gameObject);
          TimeScript.instance.levelOneCountdown += 5.0f;
          plug.SetActive(true);
           Vector3 nuevaPosicion = plug.transform.position;
                nuevaPosicion.y = coll.transform.position.y;
                nuevaPosicion.x = coll.transform.position.x;
                plug.transform.position = nuevaPosicion;
          timetopluge = true;
          Instantiate(perk_particles, transform.position, Quaternion.identity);

        }

         if (coll.gameObject.CompareTag("StarPerk")) {
            Destroy(coll.gameObject);
            
           count_level_stars.stars++;
            countstars++;
          Instantiate(perk_particles, transform.position, Quaternion.identity);
        
        }

        if (coll.gameObject.CompareTag("StarPerkTutorial")) {
            Destroy(coll.gameObject);
          Instantiate(perk_particles, transform.position, Quaternion.identity);

        }

        if (coll.gameObject.CompareTag("TimeTutorial")) {
            instance.levelOneCountdown = 16.0f;
            Destroy(coll.gameObject);
        }
    }
      IEnumerator poopsound()
        {
            yield return new WaitForSeconds(1.0f);
            audio[1].Play();
          }
       public void OnTriggerEnter2D(Collider2D coll) {
         if(coll.gameObject.CompareTag("HumanEnemy"))
         {
           speed = 2.0f;
           sprite.color = greyColor;
      
         }

        if(coll.gameObject.CompareTag("Door")){
            
          coll.GetComponent<Animator>().SetTrigger("Arrival");
          TimeScript.instance.stopTimer = true;
          wins=true;
          if(countstars>=PlayerPrefs.GetInt("StarPerk",0)){
          PlayerPrefs.SetInt("StarPerk",countstars);
          PlayerPrefs.Save();
          }

          
           PlayerPrefs.SetInt("Level2",1);
           PlayerPrefs.Save();  
          

          Debug.Log("save");
          Debug.Log(countstars);
          if(GameManager.game.isLevel1){
            float aux_time = TimeScript.instance.ObtainTime();
            int minutes = Mathf.FloorToInt(aux_time / 60);
            int seconds = Mathf.FloorToInt(aux_time % 60);
            if (minutes < GameManager.game.minutes) {
                GameManager.game.minutes = minutes;
                GameManager.game.seconds = seconds;
                gm.DataSave();
            }else if (minutes == GameManager.game.minutes) {
                if (seconds < GameManager.game.seconds) {
                    GameManager.game.minutes = minutes;
                    GameManager.game.seconds = seconds;
                    gm.DataSave();
                }
            }
          }
        GameManager.game.isLevel1 = false;
        boton.gameObject.SetActive(true);
        play_again_boton.gameObject.SetActive(true);

        win.SetActive(true);
        game.SetActive(false);
          speed = 0.0f;
          fuel = 0.0f;
           // Debug.Log("GameOver");
           audio[0].PlayOneShot(audioclip[4]);
           //StartCoroutine(ChangeScenes());
        }
    }

    public void OnTriggerExit2D(Collider2D coll) {
        
        if (coll.gameObject.CompareTag("HumanEnemy"))
        {
            speed = 7.0f;
            sprite.color = originalColor; 
        }

    }

    public void GetTrueJump(){
        UI_jump = true;
    } 

    public void GetFalseJump(){
        UI_jump = false;
    } 

    public void GetTruePause(){
        UI_pause = true;
    } 

    public void GetFalsePause(){
        UI_pause = false;
    } 
}

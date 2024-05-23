using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsControler : MonoBehaviour
{
    public int star,star2;
    public GameObject star1,star2_,star3,star12,star22,star32,key,fon;
    public int levelcompleted;
    // Start is called before the first frame update
    void Start()
    {
        star=PlayerPrefs.GetInt("StarPerks",0);
        star2=PlayerPrefs.GetInt("StarPerks2",0);
        levelcompleted=PlayerPrefs.GetInt("Levels2",0);
        star1.SetActive(false);
        star2_.SetActive(false);
        star3.SetActive(false);
         star12.SetActive(false);
        star22.SetActive(false);
        star32.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(star==1){

          star1.SetActive(true);
        }

        if(star==2){

           star1.SetActive(true);
           star2_.SetActive(true);
        }

        if(star==3){

           star1.SetActive(true);
           star2_.SetActive(true);
           star3.SetActive(true);
        }

        if(star2==1){

          star12.SetActive(true);
        }

        if(star2==2){

           star12.SetActive(true);
           star22.SetActive(true);
        }

        if(star2==3){

           star12.SetActive(true);
           star22.SetActive(true);
           star32.SetActive(true);
        }

        if(levelcompleted==1){
            key.SetActive(false);
            fon.SetActive(false);
        }else{
            key.SetActive(true);
            fon.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsControler : MonoBehaviour
{
    public int star;
    public GameObject star1,star2,star3,key,fon;
    public int levelcompleted;
    // Start is called before the first frame update
    void Start()
    {
        star=PlayerPrefs.GetInt("StarPerk",0);
        levelcompleted=PlayerPrefs.GetInt("Level2",0);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(star==1){

          star1.SetActive(true);
        }

        if(star==2){

           star1.SetActive(true);
           star2.SetActive(true);
        }

        if(star==3){

           star1.SetActive(true);
           star2.SetActive(true);
           star3.SetActive(true);
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

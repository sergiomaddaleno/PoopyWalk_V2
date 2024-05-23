using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript2 : MonoBehaviour
{
  public Text text;
  public GameDataController gm;
 
  private void Start() {
   
  }

  private void Update() {
    text.text = string.Format("{0:00}", PlayerPrefs.GetFloat("time2",0.0f));
    
  }
}

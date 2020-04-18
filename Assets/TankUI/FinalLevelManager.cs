using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelManager : MonoBehaviour
{
  public int CurProgress = 0;
  public GameObject[] BossColl;
  public static FinalLevelManager Ins;

  private void Awake()
  {
    Ins = this;
  }

  public void AddProgress()
  {
    CurProgress++;
    if (CurProgress == 2)
    {
      foreach (var VARIABLE in BossColl)
      {
        VARIABLE.gameObject.SetActive(false);        
      }
    }
  }
}

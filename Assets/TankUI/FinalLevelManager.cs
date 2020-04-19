using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelManager : MonoBehaviour
{
    public int CurProgress = 0;
    public GameObject[] BossColl;
    public static FinalLevelManager Ins;
    public Transform spawnPos;
    public GameObject Light;
    public GameObject Medium;
    public GameObject Heavy;


    private void Awake()
    {
        Spawn(Menu_Switch.tank_id);
        Ins = this;
    }
    private void Spawn(int id)
    {
        switch (id)
        {
            case 1:
                GameObject.Instantiate(Light, spawnPos.position, Quaternion.identity);
                break;
            case 2:
                GameObject.Instantiate(Medium, spawnPos.position, Quaternion.identity);
                break;
            case 3:
                GameObject.Instantiate(Heavy, spawnPos.position, Quaternion.identity);
                break;
        }
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

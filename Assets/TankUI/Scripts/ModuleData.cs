using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleData : MonoBehaviour
{

    [SerializeField] private string playerName;
    public string PlayName => playerName;
    
    public GameObject EnemyQuad, PlayerQuad;
    public void OnGameStart(string playerName)
    {
        this.playerName = playerName;
    }
}

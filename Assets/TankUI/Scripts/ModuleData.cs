using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleData : MonoBehaviour
{
    
    [SerializeField] private string playerName;
    [SerializeField] private int currScore;
    [SerializeField] private ScoreHistoryData scoreData;
    public string PlayName => playerName;
    
    public GameObject EnemyQuad, PlayerQuad;
    public void OnGameStart(string playerName)
    {
        this.playerName = playerName;
    }

    public void AddScore(int score = 50)
    {
        currScore += 50;
        ModuleRoot.Ins.UIModule.UpdateScoreText(currScore.ToString());
    }

    public void ResetScore()
    {
        currScore =0;
        ModuleRoot.Ins.UIModule.UpdateScoreText(currScore.ToString());
    }

    public void SaveScore()
    {
        foreach (var VARIABLE in scoreData.ScoreLst)
        {
            if (VARIABLE.Name == playerName)
            {
                VARIABLE.Score = currScore;
                return;
            }
        }
        scoreData.ScoreLst.Add(new PlayerData(){Name = playerName,Score = currScore});
    }

    public List<PlayerData> GetScoreLst()
    {
         scoreData.ScoreLst.Sort(
             delegate(PlayerData data, PlayerData playerData) { return playerData.Score.CompareTo(data.Score); }
             );
         return scoreData.ScoreLst;
    }

    private void Update()
    {
      
}
}

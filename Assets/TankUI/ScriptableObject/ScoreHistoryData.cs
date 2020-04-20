using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData",menuName = "DataMenu/Crate")]
public class ScoreHistoryData : ScriptableObject
{
    public List<PlayerData> ScoreLst;
}

[Serializable]
public class PlayerData
{
    public string Name;
    public int Score;
}

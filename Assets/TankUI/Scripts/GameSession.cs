using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // Start is called before the first frame update
    private static string userId;
    private static string userScore;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setUserId(string userId)
    {
        GameSession.userId = userId;
    }

    public static string getUserId()
    {
        return userId;
    }

    public static void setUserScore(string userScore)
    {
        GameSession.userScore = userScore;
    }

    public static string getUserScore()
    {
        return userScore;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public int playerHP = 5;
    RoomManager roomManager;
    int level = 4;

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        roomManager = GetComponent<RoomManager>();

        InitGame();
    }

    void InitGame()
    {
        roomManager.SetupScene(level);
    }
}

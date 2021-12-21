using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager 
{
    private static MyGameManager _instance;
    public static MyGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MyGameManager();
            }
            return _instance;
        }
    }
    public void PauseGame()
    {
        Debug.Log("PauseGame");
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Debug.Log("ResumeGame");
        Time.timeScale = 1;
    }
}

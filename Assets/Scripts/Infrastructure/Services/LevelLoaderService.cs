using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderService : ILevelLoaderService
{
    public event Action onLevelFinished;

    public int CurrentLevel
    {
        get => PlayerPrefs.GetInt("Level", 0);
        
        set => PlayerPrefs.SetInt("Level", value);
    }

    public void LoadNextLevel()
    {
        onLevelFinished?.Invoke();
        SceneManager.LoadScene(CurrentLevel++);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }
}
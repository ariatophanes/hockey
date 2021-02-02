using System;

public interface ILevelLoaderService
{
    event Action onLevelFinished; 
    
    int CurrentLevel { get; set; }

    void LoadNextLevel();
    void RestartLevel();
}
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using Zenject;

public class AnalyticsService : MonoBehaviour
{
    private ILevelLoaderService levelLoaderService;

    [Inject]
    public void Construct(ILevelLoaderService levelLoaderService)
    {
        this.levelLoaderService = levelLoaderService;
    }
    
    private void Start()
    {
        GameAnalytics.Initialize();
    }

    private void Subscribe()
    {
        levelLoaderService.onLevelFinished += HandleLevelFinished;
    }

    private void Unsubscribe()
    {
        levelLoaderService.onLevelFinished -= HandleLevelFinished;
    }

    private void HandleLevelFinished()
    {
        GameAnalytics.NewDesignEvent("Level Finished", levelLoaderService.CurrentLevel);
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}

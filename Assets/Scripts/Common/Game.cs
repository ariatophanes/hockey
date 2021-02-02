using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using Zenject;

public class Game : MonoBehaviour
{
    private bool gameEnded = false;
    
    private const float WinWaitingTime = 0.5f;
    private const float FailWaitingTime = 0.25f;

    private ILevelLoaderService levelLoaderService;
    private IPhysicalObjectFactory physicalObjectFactory;
    private IPhysicalObject blackPuckPhysicalObject;

    [Inject]
    public void Construct(ILevelLoaderService levelLoaderService, IPhysicalObjectFactory physicalObjectFactory)
    {
        this.physicalObjectFactory = physicalObjectFactory;
        this.levelLoaderService = levelLoaderService;

        SubscribeToPhysicalObjectFactory();
    }
    
    private void SubscribeToPhysicalObjectFactory()
    {
        physicalObjectFactory.onBlackPuckCreated += HandleBlackPuckCreated;
    }
    
    private void UnsubscribeFromPhysicalObjectFactory()
    {
        physicalObjectFactory.onBlackPuckCreated -= HandleBlackPuckCreated;
    }

    private void HandleBlackPuckCreated(IPhysicalObject physicalObject)
    {
        blackPuckPhysicalObject = physicalObject;
        SubscribeToBlackPuck();
    }

    private void SubscribeToBlackPuck()
    {
        blackPuckPhysicalObject.onStop += FailTimer;
        blackPuckPhysicalObject.onCollidedWithObject += HandleBlackPuckCollidedWithObjectEvent;
        blackPuckPhysicalObject.onCollidedWithTrigger += HandleBlackPuckCollidedWithObjectEvent;
    }

    private void UnsubscribeFromBlackPuck()
    {
        blackPuckPhysicalObject.onStop -= FailTimer;
        blackPuckPhysicalObject.onCollidedWithObject -= HandleBlackPuckCollidedWithObjectEvent;
        blackPuckPhysicalObject.onCollidedWithTrigger -= HandleBlackPuckCollidedWithObjectEvent;
    }
    
    private void HandleBlackPuckCollidedWithObjectEvent(string tag)
    {
        switch (tag)
        {
            case "Goal":
                WinTimer();
                break;
            case "Red Puck":
                FailTimer();
                break;
        }
    }

    void WinTimer()
    {
        if (gameEnded) return;
        
        gameEnded = true;
        Invoke("Win",  WinWaitingTime);
    }
    
    void Win()
    {
        levelLoaderService.LoadNextLevel();
    }

    void FailTimer()
    {
        if (gameEnded) return;
        
        gameEnded = true;
        Invoke("Fail",  WinWaitingTime);
    }

    void Fail()
    {
        levelLoaderService.RestartLevel();
    }

    private void OnDestroy()
    {
        UnsubscribeFromPhysicalObjectFactory();
        UnsubscribeFromBlackPuck();
    }
}
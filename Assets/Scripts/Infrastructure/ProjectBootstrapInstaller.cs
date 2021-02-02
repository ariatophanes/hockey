using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using Zenject;

public class ProjectBootstrapInstaller : MonoInstaller
{
    public GameObject GameAnalyticsPrefab;
    
    public override void InstallBindings()
    {
        BindInputService();
        BindLevelLoaderService();
        BindGameAnalytics();
        BindAnalyticsService();
    }

    private void BindAnalyticsService()
    {
        Container
            .Bind<AnalyticsService>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }

    private void BindGameAnalytics()
    {
        Container
            .Bind<GameAnalytics>()
            .FromComponentInNewPrefab(GameAnalyticsPrefab)
            .AsSingle()
            .NonLazy();
    }

    private void BindLevelLoaderService()
    {
        Container
            .Bind<ILevelLoaderService>()
            .To<LevelLoaderService>()
            .AsSingle();
    }

    private void BindInputService()
    {
        Container
            .BindInterfacesTo<TouchInputService>()
            .AsSingle();
    }
}

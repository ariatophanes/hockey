using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneBootstrapInstaller : MonoInstaller
{
    public LineRenderer lr;
    
    public override void InstallBindings()
    {
        BindGoalSpawner();
        
        BindLineRenderer();
        
        BindThrowService();
        
        BindCamera();
        
        BindThrower();
        
        BindUI();
        
        BindPhysicalObjectFactory();
    }

    private void BindRedPucksSpawner()
    {
        Container
            .Bind<RedPucksSpawner>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }

    private void BindUI()
    {
        Container
            .Bind<GameUI>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }

    private void BindGoalSpawner()
    {
        Container
            .Bind<GoalSpawner>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }

    private void BindPhysicalObjectFactory()
    {
        Container
            .Bind<IPhysicalObjectFactory>()
            .To<PhysicalObjectFactory>()
            .AsSingle();
    }

    private void BindThrower()
    {
        Container
            .Bind<Thrower>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }

    private void BindCamera()
    {
        Container
            .Bind<Camera>()
            .FromInstance(Camera.main)
            .AsSingle();
    }

    private void BindLineRenderer()
    {
        Container
            .Bind<LineRenderer>()
            .FromInstance(lr)
            .AsSingle();
    }

    private void BindThrowService()
    {
        Container
            .Bind<IThrowService>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }
}

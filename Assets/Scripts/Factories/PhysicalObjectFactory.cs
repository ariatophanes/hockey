using System;
using UnityEngine;
using Zenject;

public class PhysicalObjectFactory : IPhysicalObjectFactory
{
    private const string PrefabBlackPuckPath = "Prefabs/Black Puck";
    private const string PrefabRedPuckPath = "Prefabs/Red Puck";
    private const string PrefabGoalPath = "Prefabs/Goal";
    
    public event Action<IPhysicalObject> onBlackPuckCreated;

    private GameObject blackPuckPrefab, redPuckPrefab, goalPrefab;

    private DiContainer diContainer;

    public enum ObjectType
    {
        BlackPuck,
        RedPuck,
        Goal
    }

    public PhysicalObjectFactory(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }

    public IPhysicalObject Create(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.BlackPuck:
                IPhysicalObject physicalObject = diContainer.InstantiatePrefabResource(PrefabBlackPuckPath).GetComponent<PhysicalObject>();
                onBlackPuckCreated?.Invoke(physicalObject);
                return physicalObject;
            case ObjectType.RedPuck:
                return diContainer.InstantiatePrefabResource(PrefabRedPuckPath).GetComponent<PhysicalObject>();
            case ObjectType.Goal:
                return diContainer.InstantiatePrefabResource(PrefabGoalPath).GetComponent<PhysicalObject>();
            default:
                return null;
        }
    }
}
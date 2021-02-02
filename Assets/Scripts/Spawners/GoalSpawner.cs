using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GoalSpawner : MonoBehaviour
{
    public Transform[] spawnPositions;
    private IPhysicalObjectFactory physicalObjectFactory;
    private IPhysicalObject goal;

    [Inject]
    public void Construct(IPhysicalObjectFactory physicalObjectFactory)
    {
        this.physicalObjectFactory = physicalObjectFactory;
        
        SpawnGoal();
    }

    private void SpawnGoal()
    {
        goal = physicalObjectFactory.Create(PhysicalObjectFactory.ObjectType.Goal);
        goal.SetPosition(GetRandomSpawnPos());
    }

    private Vector2 GetRandomSpawnPos()
    {
        return spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
    }
}

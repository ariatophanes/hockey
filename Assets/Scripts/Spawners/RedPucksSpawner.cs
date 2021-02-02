using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RedPucksSpawner : MonoBehaviour
{
    private IPhysicalObjectFactory physicalObjectFactory;

    public Transform[] spawnPoints;

    [Inject]
    public void Construct(IPhysicalObjectFactory  physicalObjectFactory)
    {
        this.physicalObjectFactory = physicalObjectFactory;
        
        SpawnPucks();
    }

    private void SpawnPucks()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            physicalObjectFactory.Create(PhysicalObjectFactory.ObjectType.RedPuck).SetPosition(spawnPoints[i].position);
        }
    }
    
}

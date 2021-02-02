using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Thrower : MonoBehaviour
{
    private GameObject dummyPlayerPuck;
    private IThrowService throwService;
    private IPhysicalObjectFactory physicalObjectFactory;

    private const float ThrowForce = 10;
    
    [Inject]
    public void Construct(IThrowService throwService, IPhysicalObjectFactory physicalObjectFactory)
    {
        this.throwService = throwService;
        this.physicalObjectFactory = physicalObjectFactory;
        
        CreateDummyPuck();
        
        Subscribe();
    }

    private void CreateDummyPuck()
    {
        dummyPlayerPuck = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Dummy Puck"));
    }

    private void Subscribe()
    {
        throwService.onThrow += HandleThrow;
    }

    private void Unsubscribe()
    {
        throwService.onThrow -= HandleThrow;
    }

    private void HandleThrow(Vector3 dir)
    {
        dummyPlayerPuck.SetActive(false);
        IPhysicalObject physicalObject = physicalObjectFactory.Create(PhysicalObjectFactory.ObjectType.BlackPuck);
        physicalObject.ApplyForce(dir * ThrowForce);
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
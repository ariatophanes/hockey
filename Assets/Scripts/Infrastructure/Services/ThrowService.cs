using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ThrowService : MonoBehaviour, IThrowService
{
    public event Action<Vector3> onThrow;

    private bool serviceEnabled;
    public bool ServiceEnabled
    {
        get => serviceEnabled;
        set
        {
            serviceEnabled = value;
            if(!value) 
                Unsubscribe();
            else
                Subscribe();
        }
    }

    private IInputService inputService;
    private LineRenderer lr;

    private Vector3 lineEndPos;
    private Vector2 transformedDelta;
    
    private const float MaxLineLength = 2.5f;
    private const float MinLineLength = 0.5f;
    private const float LineLerpSpeed = 10;
    private const float Elasticity = 15;

    [Inject]
    public void Construct(LineRenderer lr,IInputService inputService)
    {
        this.inputService = inputService;
        this.lr = lr;

        ServiceEnabled = true;
    }

    private void Subscribe()
    {
        inputService.onDrag += HandleDrag;
        inputService.onEndDrag += HandleEndDrag;
        inputService.onStartDrag += HandleStartDrag;
    }

    private void Unsubscribe()
    {
        inputService.onDrag -= HandleDrag;
        inputService.onEndDrag -= HandleEndDrag;
        inputService.onStartDrag -= HandleStartDrag;
    }

    private void HandleDrag(object sender, OnDragEventArgs args)
    {
        DrawLine(args.delta);
    }

    private void HandleEndDrag(object sender, OnEndDragEventArgs args)
    {
        if (ServiceEnabled)
        {
            if (transformedDelta.magnitude < MinLineLength) return;
            onThrow?.Invoke(transformedDelta);
            ServiceEnabled = false;
        }
        
        lr.enabled = false;
    }

    private void HandleStartDrag(object sender, OnStartDragEventArgs args)
    {
        lineEndPos = transform.position;
        lr.enabled = true;
    }

    private void DrawLine(Vector3 delta)
    {
        transformedDelta = delta;
        transformedDelta.x /= Screen.width;
        transformedDelta.y /= Screen.height;
        
        transformedDelta *= Elasticity;
        transformedDelta = Vector3.ClampMagnitude(transformedDelta, MaxLineLength);

        Vector2 startPos = transform.position;
        Vector2 endPos = (Vector2) transform.position + transformedDelta;

        lineEndPos = Vector3.Lerp(lineEndPos, endPos, LineLerpSpeed * Time.deltaTime);

        lr.SetPosition(0, startPos);
        lr.SetPosition(1, lineEndPos);
    }
}
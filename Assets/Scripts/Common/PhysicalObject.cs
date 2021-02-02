using System;
using UnityEngine;

public class PhysicalObject : MonoBehaviour, IPhysicalObject
{
    private const float StopVelocityValue = 0.3f;
    private const float VelocityMultiplier = 0.98f;

    public event Action onStop;
    public event Action<string> onCollidedWithObject, onCollidedWithTrigger;

    private Rigidbody2D rb;
    private bool hasVelocity;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyForce(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        hasVelocity = true;
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    private void Update()
    {
        if (!hasVelocity) return;
        
        rb.velocity *= VelocityMultiplier;
        
        if (rb.velocity.magnitude > StopVelocityValue) return;
        
        onStop?.Invoke();
        hasVelocity = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onCollidedWithObject?.Invoke(collision.collider.tag);
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        onCollidedWithTrigger?.Invoke(collider.tag);
    }
}
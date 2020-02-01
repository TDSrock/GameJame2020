using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectSuper : MonoBehaviour
{
    private enum Weight
    {
        light,
        medium,
        heavy
    }

    [SerializeField]
    Weight objectWeight;

    [SerializeField]
    float speedThreshold = 0;

    [SerializeField]
    Vector3 velocity;

    Rigidbody body;

    float weightScale = 1;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        switch (objectWeight)
        {
            case Weight.light:
                weightScale = 3;
                return;
            case Weight.medium:
                weightScale = 1;
                return;
            case Weight.heavy:
                weightScale = 0.5f;
                return;
        }
    }
    
    public virtual void FixedUpdate()
    {
        if (velocity.magnitude > 0)
        {
            Move(velocity, Time.fixedDeltaTime);
            velocity *= 0.95f;
        }

        if (velocity.magnitude <= 0.1f) velocity = Vector3.zero;
    }

    public virtual void CollideWith(PlayerController player)
    {
        Debug.Log(player.Velocity.magnitude);
        if (player.Velocity.magnitude > speedThreshold)
        {
            Vector3 direction = (transform.transform.position - player.transform.position).normalized;
            velocity = direction * player.Velocity.magnitude * weightScale;
        }

        else if (player.Velocity.magnitude >= speedThreshold / 2)
        {
            //Shake(); 
        }
    }

    public virtual void Move(Vector3 velocity, float deltaTime)
    {
        this.transform.position += velocity * deltaTime;
    }

}

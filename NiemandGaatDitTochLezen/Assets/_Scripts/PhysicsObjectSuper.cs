using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectSuper : MonoBehaviour
{
    [SerializeField]
    float speedThreshold;

    [SerializeField]
    Vector3 velocity;

    Rigidbody body;

    float maxSpeed;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
    }

    public virtual void FixedUpdate()
    {
        if (velocity.magnitude > 0)
        {
            body.isKinematic = false;
            Move(velocity, Time.fixedDeltaTime);    
        }

        if (!body.isKinematic && velocity.magnitude <= 0) body.isKinematic = true;

        velocity *= 0.95f;
        if (velocity.magnitude <= 0.1f) velocity = Vector3.zero;
    }

    public virtual void CollideWith(PlayerController player)
    {
        if (player.Velocity.magnitude > speedThreshold)
        {
            velocity = player.Velocity;
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

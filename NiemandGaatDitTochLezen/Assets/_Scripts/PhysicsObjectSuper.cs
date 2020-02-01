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
            //velocity = player.Velocity;
            Vector3 direction = (transform.transform.position - player.transform.position).normalized;
            velocity = direction * player.Velocity.magnitude * 2;
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

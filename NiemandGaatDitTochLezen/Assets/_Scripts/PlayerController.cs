using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody body;

    [SerializeField]
    float moveSpeed;

    [HideInInspector]
    public Vector3 moveDirection;

    [HideInInspector]
    public Vector3 Velocity { get { return moveDirection * moveSpeed; } }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();

    }

    private void FixedUpdate()
    {
        Move();
        Debug.Log(body.velocity.magnitude);
    }

    void GetInput()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (moveDirection.magnitude > 1) moveDirection = moveDirection.normalized;
    }

    private void Move()
    {
        body.velocity = moveDirection * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PhysicsObjectSuper physicsObject = collision.gameObject.GetComponent<PhysicsObjectSuper>();
        if (physicsObject) physicsObject.CollideWith(this);
    }
}

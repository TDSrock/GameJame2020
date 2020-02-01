using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    Vector3 moveDirection;

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
    }

    void GetInput()
    {
        moveDirection = Vector3.Normalize(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
    }

    private void Move(float deltaTime)
    {
        transform.position += moveDirection * moveSpeed * deltaTime;
    }
}

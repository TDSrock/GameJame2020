using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;

public class PlayerController : MonoBehaviour
{
    Rigidbody body;

    [SerializeField]
    float moveSpeed = 5;

    [HideInInspector]
    public Vector3 moveDirection;

    [HideInInspector]
    public Vector3 Velocity { get { return moveDirection * moveSpeed; } }

    public KeyCode interactionKey = KeyCode.E;
    [Header("Interactable UI")]
    public StringReference interactionTextHint;

    Rigidbody rb;


    public List<IInteractable> interactables = new List<IInteractable>();


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = Vector3.zero;
        if(interactables.Count != 0)

        {
            IInteractable closest = interactables.GetClostestsInteractable(this.transform.position);
            interactionTextHint.Value = string.Format("Press {0} to interact with {1}", interactionKey, closest.GetObjectName());
            if (Input.GetKeyUp(interactionKey))
            {
                closest.OnInteract();
            }
        }
        else
        {
            interactionTextHint.Value = "";
        }
        GetInput();

    }

    private void FixedUpdate()
    {
        Move();
        //Debug.Log(rb.velocity.magnitude);
    }

    void GetInput()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (moveDirection.magnitude > 1) moveDirection = moveDirection.normalized;
    }

    private void Move()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PhysicsObjectSuper physicsObject = collision.gameObject.GetComponent<PhysicsObjectSuper>();
        if (physicsObject) physicsObject.CollideWith(this);
    }

    void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            if(!interactables.Contains(interactable))
                interactables.Add(interactable);//add the interactable to the list
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactables.Remove(interactable);//remove the interactable from the list
        }
    }
}

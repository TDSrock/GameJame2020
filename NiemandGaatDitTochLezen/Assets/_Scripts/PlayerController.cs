using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text interactionTextHint;


    public List<IInteractable> interactables = new List<IInteractable>();

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (interactables.Count != 0)
        {
            IInteractable closest = interactables.GetClostestsInteractable(this.transform.position);
            interactionTextHint.gameObject.SetActive(true);
            interactionTextHint.text = string.Format("Press {0} to interact with {1}", interactionKey, closest.GetObjectName());
            if (Input.GetKeyUp(interactionKey))
            {
                closest.OnInteract();
            }
        }
        else
        {
            interactionTextHint.gameObject.SetActive(false);
        }
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

    void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
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

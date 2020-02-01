using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    Vector3 moveDirection;
    public KeyCode interactionKey = KeyCode.E;
    [Header("Interactable UI")]
    public Text interactionTextHint;
    Rigidbody rb;

    public List<IInteractable> interactables = new List<IInteractable>();

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = Vector3.zero;
        if(interactables.Count != 0)
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

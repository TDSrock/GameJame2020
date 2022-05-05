using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;

public class PlayerController : MonoBehaviour
{
    public Animator animat;

    [SerializeField]
    float moveSpeed = 5;

    [HideInInspector]
    public Vector3 moveDirection;

    [HideInInspector]
    public Vector3 Velocity { get { return moveDirection * moveSpeed; } }
    public LayerMask interactionMask;
    public KeyCode interactionKey = KeyCode.E;
    public float interactionRange = 3f;
    [Header("Interactable UI")]
    public StringReference interactionTextHint;

    Rigidbody rb;


    public List<IInteractable> interactables = new List<IInteractable>();

    [SerializeField]
    AudioSource walkingAudio, hitAudio;

    float timeWalking;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animat = GetComponent<Animator>();
    }

    void Update()
    {

        rb.velocity = Vector3.zero;


        interactables.Clear();
        Collider[] buffer = Physics.OverlapSphere(this.transform.position, interactionRange, interactionMask);
        foreach (var obj in buffer)
        {
            IInteractable interactable = obj.GetComponent<IInteractable>();
            if (interactable == null) interactable = obj.GetComponentInParent<IInteractable>();
            RaycastHit hitInfo;
            if (interactable != null)
            {
                Ray ray = new Ray(transform.position + new Vector3(0, 1f, 0), interactable.GetPosition() - transform.position);
                if (Physics.Raycast(ray, out hitInfo, interactionRange))
                {
                    //Debug.Log(interactable.GameObject.name + " " + hitInfo.collider.gameObject.name);
                    if (interactable.GameObject == hitInfo.collider.gameObject || hitInfo.collider.gameObject.transform.IsChildOf(interactable.GameObject.transform))
                        interactables.Add(interactable);
                }
            }
        }
        if (interactables.Count != 0)
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

        Move();

        if (rb.velocity.magnitude > 0 && !walkingAudio.isPlaying)
        {
            walkingAudio.Play();
        }
        else if (rb.velocity.magnitude <= 0) { walkingAudio.Stop(); }

        if (rb.velocity.magnitude > 0)
        {
            timeWalking++;
            transform.forward = rb.velocity.normalized;
            animat.SetBool("IsMoving", true);
        } else { animat.SetBool("IsMoving", false); }
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

        if (rb.velocity.magnitude > 2 && timeWalking > 5 && !collision.gameObject.CompareTag("Floor"))
        {
            hitAudio.Play();
            timeWalking = 0;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, interactionRange);
        foreach(var a in interactables)
        {
            Gizmos.DrawLine(this.transform.position + new Vector3(0,1f,0), a.GetPosition());
        }
    }
}

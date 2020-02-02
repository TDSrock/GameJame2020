using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;
using SjorsGielen.UsefullScripts;

public class FrontView : MonoBehaviour, IInteractable
{
    [Header("Must set these in scene or we get fucked")]
    public FrontViewInteractor frontViewInteractor;
    public Camera playerCamera;
    protected SmartCameraFollow smartCameraFollow;
    public PlayerController player;
    protected SkinnedMeshRenderer playerRender;

    [Header("Prefab variables")]
    public GameObject frontViewCameraLocation;
    public bool isInteractingWith = false;
    public KeyCode closeInteractionKey = KeyCode.P;

    Collider[] interactionColliders;
    Vector3 cachedCameraPosition;
    Quaternion cachedCameraRotation;

    public FloatReference cameraLerpTime;

    [Header("UI shit")]
    [Tooltip("Is used for the text hint for the player")]
    public string objectName;

    public void Start()
    {
        interactionColliders = GetComponents<Collider>();
        smartCameraFollow = playerCamera.GetComponent<SmartCameraFollow>();
        playerRender = player.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void Update()
    {
        if (isInteractingWith)
        {
            if (Input.GetKeyUp(closeInteractionKey))
            {
                this.isInteractingWith = false;
                this.CloseInteraction();
            }
        }
    }

    public void OnInteract()
    {
        foreach(var col in interactionColliders)
        {
            if(col.isTrigger)
                col.enabled = false;
        }
        StopAllCoroutines();
        StartCoroutine(MoveCamera(frontViewCameraLocation.transform.position, frontViewCameraLocation.transform.rotation));
    }

    IEnumerator MoveCamera(Vector3 goalPos, Quaternion goalRot)
    {

        if (goalPos == frontViewCameraLocation.transform.position)
        {
            playerRender.enabled = false;
            cachedCameraPosition = playerCamera.transform.position;
            cachedCameraRotation = playerCamera.transform.rotation;
            smartCameraFollow.enabled = false;
            player.enabled = false;//take away control from the character
            player.interactionTextHint.Value = "";
        }
        else
        {
            player.enabled = true;//if you can see the player you should be able to control the player, feels weird otherwise
            playerRender.enabled = true;
        }
        float timeLerping = 0;
        while (timeLerping <= cameraLerpTime)
        {
            timeLerping += Time.deltaTime;
            if (timeLerping != 0)
            {
                float percent = timeLerping / cameraLerpTime;
                playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, goalPos, percent);
                playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, goalRot, percent);
            }

            yield return null;
        }
        playerCamera.transform.position = goalPos;//hard set at goal location.

        if (goalPos == cachedCameraPosition)
        {
            smartCameraFollow.enabled = true;
            playerRender.enabled = true;
            player.interactionTextHint.Value = "";
        }
        else
        {
            smartCameraFollow.enabled = false;
            playerRender.enabled = false;
            SetupInteraction();//afaik idk if this is going to be needed up whatever
        }

    }

    public void OnStopInteract()
    {
        foreach (var col in interactionColliders)
        {
            if (col.isTrigger)
                col.enabled = true;
        }
        frontViewInteractor.enabled = false;
        StopAllCoroutines();
        StartCoroutine( MoveCamera(cachedCameraPosition, cachedCameraRotation));
    }
    
    /// <summary>
    /// Function to setup the interaction with this specific interatble frontview element.
    /// </summary>
    virtual public void SetupInteraction()
    {
        isInteractingWith = true;
        frontViewInteractor.enabled = true;
    }

    /// <summary>
    /// Function to remove anything setup bu the SetupInteraction function
    /// </summary>
    virtual public void CloseInteraction()
    {
        isInteractingWith = false;
        OnStopInteract();
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public virtual string GetObjectName()
    {
        return objectName;
    }
}

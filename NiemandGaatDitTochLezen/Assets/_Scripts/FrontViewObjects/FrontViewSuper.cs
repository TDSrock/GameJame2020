using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;
using SjorsGielen.UsefullScripts;

abstract public class FrontViewSuper : MonoBehaviour, IInteractable
{

    public Camera playerCamera;
    protected SmartCameraFollow smartCameraFollow;
    public GameObject frontViewCameraLocation;
    public PlayerController player;
    protected MeshRenderer playerRender;

    Vector3 cachedCameraPosition;

    public FloatReference cameraLerpTime;

    [Header("UI shit")]
    [Tooltip("Is used for the text hint for the player")]
    public string objectName;

    public void Start()
    {
        smartCameraFollow = playerCamera.GetComponent<SmartCameraFollow>();
        playerRender = player.GetComponent<MeshRenderer>();
    }

    public void OnInteract()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCamera(frontViewCameraLocation.transform.position));
    }

    IEnumerator MoveCamera(Vector3 goal)
    {
        if (goal == frontViewCameraLocation.transform.position)
        {
            cachedCameraPosition = playerCamera.transform.position;
            smartCameraFollow.enabled = false;
            player.enabled = false;//take away control from the character
            playerRender.enabled = true;
            player.interactionTextHint.gameObject.SetActive(false);
        }
        float timeLerping = 0;
        while (timeLerping <= cameraLerpTime)
        {

            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, goal, timeLerping/cameraLerpTime);
            timeLerping += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.position = goal;//hard set at goal location.

        if (goal != frontViewCameraLocation.transform.position)
        {
            smartCameraFollow.enabled = true;
            player.enabled = true;
            playerRender.enabled = true;
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
        StopAllCoroutines();
        StartCoroutine( MoveCamera(cachedCameraPosition));
    }
    
    /// <summary>
    /// Function to setup the interaction with this specific interatble frontview element.
    /// </summary>
    abstract public void SetupInteraction();

    /// <summary>
    /// Function to remove anything setup bu the SetupInteraction function
    /// </summary>
    virtual public void CloseInteraction()
    {
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

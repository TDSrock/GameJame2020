using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;

abstract public class FrontViewSuper : MonoBehaviour, IInteractable
{
    public Camera playerCamera;
    public GameObject frontViewCameraLocation;
    public CharacterController player;

    Vector3 cachedCameraPosition;

    [Range(0, 1)]
    public FloatReference cameraLerpTime;

    public void Start()
    {

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
            player.enabled = false;//take away control from the character
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
            player.enabled = true;
        }
        else
        {
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
    abstract public void CloseInteraction();
}

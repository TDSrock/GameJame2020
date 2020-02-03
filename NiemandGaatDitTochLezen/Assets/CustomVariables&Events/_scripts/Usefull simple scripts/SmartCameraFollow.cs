using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SjorsGielen.CustomVariables.RangeVariables;
using SjorsGielen.Extensions;

namespace SjorsGielen.UsefullScripts
{
    public class SmartCameraFollow : MonoBehaviour
    {
        public enum LockedAxis
        {
            x,
            y,
            z
        }

        public enum FollowMode
        {
            SnapFocus,
            DragFocus
        }
        public LockedAxis lockedAxis = LockedAxis.z;
        public FollowMode followMode = FollowMode.SnapFocus;
        Vector3 startOffset;
        Vector3 offset;
        Vector3 targetOffset;
        public float cameraPullDistance;

        //TODO Add maximum bounds the effect can reach
        [Tooltip("Maximum amount the script can pull")]
        public float maximumPullEffect;
        public GameObject toFollowObject;
        [Tooltip("The distance the camera will look for objects that match the layermask so they can have an effect on the camera")]
        public float cameraPullEffect = 1f;
        [Tooltip("The layermask that is used to check for objects that should affect the camera")]
        public LayerMask cameraPullMask;
        //TODO Use asset to simplify and add tooltips
        private float cameraHeight = 45f;
        [MinMaxRange(-50,50)]
        public FloatRange cameraHeightBounds = new FloatRange(1,20);
        [Tooltip("The speed at which the camera will move towards it's inteded location")]
        [Range(0, 1)]
        public float lerpSpeed = .5f;
#if UNITY_EDITOR
        [Header("Debug")]
        public bool populateDebugList = false;

        public List<GameObject> EffectingCameraDebugList = new List<GameObject>();
#endif
        void Start()
        {
            maximumPullEffect = Mathf.Abs(maximumPullEffect);
            //Calculate and store the offset value by getting the distance between the player's position and camera's position.
            startOffset = transform.position - toFollowObject.transform.position;
            offset = startOffset;
            switch (lockedAxis)
            {
                case LockedAxis.x:
                    cameraHeight = startOffset.x;
                    break;
                case LockedAxis.y:
                    cameraHeight = startOffset.y;
                    break;
                case LockedAxis.z:
                    cameraHeight = startOffset.z;
                    break;
                default:
                    Debug.LogError("New locked axis created, please add behavior!");
                    break;
            }
        }

        private void Update()
        {
            //TODO maybe remove implicit controlls. Consider if tooling should have implicit controlls built in.
            cameraHeight -= Input.mouseScrollDelta.y;
            cameraHeight = Mathf.Clamp(cameraHeight, cameraHeightBounds.MinValue, cameraHeightBounds.MaxValue);
        }

        // LateUpdate is called after Update each frame
        void LateUpdate()
        {
            if (cameraPullEffect != 0 && cameraPullDistance != 0)
            {
                Collider[] objectsFoundInRange = Physics.OverlapSphere(toFollowObject.transform.position, cameraPullDistance, cameraPullMask);
#if UNITY_EDITOR
                EffectingCameraDebugList.Clear();//clear this each frame
#endif
                if (objectsFoundInRange.Length != 0)
                {
                    targetOffset = startOffset;
                    foreach (Collider gameObject in objectsFoundInRange)
                    {
#if UNITY_EDITOR
                        if (populateDebugList)
                            EffectingCameraDebugList.Add(gameObject.gameObject);
#endif
                        targetOffset -= (toFollowObject.transform.position - gameObject.transform.position) * cameraPullEffect *
                                (cameraPullDistance - Vector3.Distance(toFollowObject.transform.position, gameObject.transform.position)) / cameraPullDistance;
                    }
                    targetOffset /= objectsFoundInRange.Length;
                }
                else
                {
                    targetOffset = startOffset;
                }
            }
            else
            {
                targetOffset = startOffset;//remain at this offset forever
            }

            //maintain camera height, but if it is changed, we do want to lerp it along.
            //also check if we are not exceeding the limits, if so clamp ourselfs back
            switch (lockedAxis)
                {
                    //TODO FIX BUG HERE
                    case LockedAxis.x:
                        targetOffset = targetOffset.Clamp(startOffset - new Vector3(0, maximumPullEffect, maximumPullEffect), startOffset + new Vector3(0, maximumPullEffect, maximumPullEffect));
                        startOffset.x = cameraHeight;
                        targetOffset.x = cameraHeight;
                        break;
                    case LockedAxis.y:
                        targetOffset = targetOffset.Clamp(startOffset - new Vector3(maximumPullEffect, 0, maximumPullEffect), startOffset + new Vector3(maximumPullEffect, 0, maximumPullEffect));
                        startOffset.y = cameraHeight;
                        targetOffset.y = cameraHeight;
                        break;
                case LockedAxis.z:
                        targetOffset = targetOffset.Clamp(startOffset - new Vector3(maximumPullEffect, maximumPullEffect, 0), startOffset + new Vector3(maximumPullEffect, maximumPullEffect, 0));
                        startOffset.z = cameraHeight;
                        targetOffset.z = cameraHeight;
                        break;
                    default:
                        Debug.LogError("New locked axis created, please add behavior!(Late update)");
                        break;
                }

            //commit the movement in different ways depeding on the mode selected.
            switch (followMode)
            {
                //Moves along with the player 'snappingly'.
                case FollowMode.SnapFocus:
                    offset = Vector3.Lerp(offset, targetOffset, lerpSpeed);
                    transform.position = toFollowObject.transform.position + offset;
                    break;
                 //lerps after the player, will lag behind a little bit.
                case FollowMode.DragFocus:
                    offset = Vector3.Lerp(offset, targetOffset + toFollowObject.transform.position, lerpSpeed);
                    transform.position = offset;
                    break;
                default:
                    Debug.LogWarningFormat("'{0}' Unexpected followmode found, please add behavior.", followMode);
                    break;
            }

        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 drawSquareAroundPoint;
            if (EditorApplication.isPlaying)
            {
                drawSquareAroundPoint = toFollowObject.transform.position + startOffset;
            }
            else
            {
                drawSquareAroundPoint = this.transform.position;
            }
            Gizmos.color = Color.yellow;
            switch (lockedAxis)
            {                
                case LockedAxis.x:
                    Gizmos.DrawWireCube(drawSquareAroundPoint, new Vector3(0, maximumPullEffect, maximumPullEffect) * 2);
                    break;
                case LockedAxis.y:
                    Gizmos.DrawWireCube(drawSquareAroundPoint, new Vector3(maximumPullEffect, 0, maximumPullEffect) * 2);
                    break;
                case LockedAxis.z:
                    Gizmos.DrawWireCube(drawSquareAroundPoint, new Vector3(maximumPullEffect, maximumPullEffect, 0) * 2);
                    break;
                default:
                    Debug.LogError("New locked axis created, please add behavior!(Gizmo)");
                    break;
            }
            if (!toFollowObject)
                return;

            foreach(var gb in EffectingCameraDebugList)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(toFollowObject.transform.position, gb.transform.position);
            }
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(toFollowObject.transform.position, cameraPullDistance);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(this.transform.position, toFollowObject.transform.position + this.targetOffset);
        }
#endif
    }
}

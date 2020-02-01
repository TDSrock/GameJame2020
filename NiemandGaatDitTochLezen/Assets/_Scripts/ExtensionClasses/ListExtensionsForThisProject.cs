using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensionsForThisProject
{
    public static IInteractable GetClostestsInteractable(this List<IInteractable> list, Vector3 position)
    {
        IInteractable closest =null;
        var shortestDistance = Mathf.Infinity;

        foreach (var otherPosition in list)
        {
            var distance = (position - otherPosition.GetPosition()).sqrMagnitude;

            if (distance < shortestDistance)
            {
                closest = otherPosition;
                shortestDistance = distance;
            }
        }

        return closest;
    }
}

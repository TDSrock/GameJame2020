using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SjorsGielen.Extensions{
    public static class Vector3Extension
    {
        /// <summary>
        /// Clamps the vector3 using Mathf clamp on each component respectivly.
        /// </summary>
        /// <param name="min">The minimum component</param>
        /// <param name="max">The maxiumum component</param>
        /// <returns>The Vector3 clamped between the min and max vectors provided</returns>
        public static Vector3 Clamp(this Vector3 v1, Vector3 min, Vector3 max)
        {
            return new Vector3(Mathf.Clamp(v1.x, min.x, max.x),
                Mathf.Clamp(v1.y, min.y, max.y),
                Mathf.Clamp(v1.z, min.z, max.z));
        }

        /// <summary>
        /// Finds the position closest to the given one.
        /// </summary>
        /// <param name="position">World position.</param>
        /// <param name="otherPositions">Other world positions.</param>
        /// <returns>Closest position.</returns>
        public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
        {
            var closest = Vector3.zero;
            var shortestDistance = Mathf.Infinity;

            foreach (var otherPosition in otherPositions)
            {
                var distance = (position - otherPosition).sqrMagnitude;

                if (distance < shortestDistance)
                {
                    closest = otherPosition;
                    shortestDistance = distance;
                }
            }

            return closest;
        }
    }
}

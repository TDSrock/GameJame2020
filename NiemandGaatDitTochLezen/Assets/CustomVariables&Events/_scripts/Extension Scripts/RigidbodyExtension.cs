﻿using UnityEngine;

namespace SjorsGielen.Extensions
{
    /// <summary>
    /// Extension methods for UnityEngine.Rigidbody.
    /// </summary>
    public static class RigidbodyExtension
    {
        /// <summary>
        /// Changes the direction of a rigidbody without changing its speed.
        /// </summary>
        /// <param name="rigidbody">Rigidbody.</param>
        /// <param name="direction">New direction.</param>
        public static void ChangeDirection(this Rigidbody rigidbody, Vector3 direction)
        {
            rigidbody.velocity = direction * rigidbody.velocity.magnitude;
        }
    }
}
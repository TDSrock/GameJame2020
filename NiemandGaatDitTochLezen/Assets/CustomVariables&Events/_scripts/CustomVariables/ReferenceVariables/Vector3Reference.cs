using System;
using UnityEngine;

namespace SjorsGielen.CustomVariables.ReferenceVariables
{
    [Serializable]
    public class Vector3Reference : AbstractVariableReference<Vector3Variable, Vector3>
    {
        public static implicit operator Vector3(Vector3Reference reference)
        {
            return reference.Value;
        }
    }
}

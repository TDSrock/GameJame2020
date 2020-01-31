using System;

namespace SjorsGielen.CustomVariables.ReferenceVariables
{
    [Serializable]
    public class FloatReference : AbstractVariableReference<FloatVariable, float>
    {
        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }
    }
}

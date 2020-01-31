using System;
using SjorsGielen.CustomVariables.RangeVariables;

namespace SjorsGielen.CustomVariables.ReferenceVariables
{
    [Serializable]
    public class FloatRangeReference : AbstractVariableReference<FloatRangeVariable, FloatRange>
    {
        public static implicit operator FloatRange(FloatRangeReference reference)
        {
            return reference.Value;
        }

        public static implicit operator FloatRangeReference(FloatRange v)
        {
            return v;
        }
    }
}

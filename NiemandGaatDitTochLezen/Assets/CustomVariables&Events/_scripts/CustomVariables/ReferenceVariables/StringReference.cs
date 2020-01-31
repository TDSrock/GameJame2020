using System;

namespace SjorsGielen.CustomVariables.ReferenceVariables
{
    [Serializable]
    public class StringReference : AbstractVariableReference<StringVariable, string>
    {
        public static implicit operator string(StringReference reference)
        {
            return reference.Value;
        }
    }
}

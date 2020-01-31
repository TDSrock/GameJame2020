using System;
using UnityEngine;
using SjorsGielen.CustomVariables.RangeVariables;

namespace SjorsGielen.CustomVariables.ReferenceVariables
{
    [Serializable]
    abstract public class AbstractVariableReference<T, U>
        where T : AbstractVariable<U>
    {
        public bool UseConstant = true;
        [MinMaxRange(-20, 100)]
        [SerializeField] U ConstantValue;
        [SerializeField] T Variable = null;

        public U Value
        {
            get
            {
                return UseConstant ? ConstantValue : Variable.Value;
            }
            set
            {
                if(UseConstant)
                    ConstantValue = value;
                else
                    Variable.Value = value;
            }
        }

        public static implicit operator U(AbstractVariableReference<T, U> reference)
        {
            return reference.Value;
        }

        public static implicit operator AbstractVariableReference<T,U>(U v)
        {
            return v;
        }
    }
}

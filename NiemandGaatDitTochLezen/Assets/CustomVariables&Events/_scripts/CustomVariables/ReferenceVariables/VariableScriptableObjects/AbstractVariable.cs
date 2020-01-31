using UnityEngine;
using SjorsGielen.CustomVariables.RangeVariables;

namespace SjorsGielen.CustomVariables
{
    public abstract class AbstractVariable<T> : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        //To change the min-max range of all minmaxrange variables edit the below values.
        [MinMaxRange(-100, 100)][SerializeField]
        private T value;
        public T Value {
            set { this.value = value; }
            get { return this.value; }
        }

        /// <summary>
        /// Sets the value of this variable to the param value
        /// </summary>
        /// <param name="value">Value this variable should equate too</param>
        public virtual void SetValue(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Sets the value of this variable to the param value
        /// </summary>
        /// <param name="value">Value this variable should equate too</param>
        public virtual void SetValue(AbstractVariable<T> value)
        {
            Value = value.Value;
        }

        /// <summary>
        /// Adds the amount to this Variables value.
        /// </summary>
        /// <param name="amount">The amount to add</param>
        public abstract void ApplyChange(T amount);

        /// <summary>
        /// Adds the amount to this Variables value.
        /// </summary>
        /// <param name="amount">The amount to add</param>
        public abstract void ApplyChange(AbstractVariable<T> amount);

    }

}

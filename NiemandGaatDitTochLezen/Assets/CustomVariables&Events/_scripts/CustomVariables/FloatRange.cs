using System;
using UnityEngine;

namespace SjorsGielen.CustomVariables.RangeVariables
{
    [Serializable]
    public class FloatRange : AbstractRangeVariable<float>
    {
        public FloatRange(float min, float max)
        {
            this.MinValue = min; this.MaxValue = max;
        }

        public override float GetRandomValue()
        {
            return UnityEngine.Random.Range(this.MinValue, this.MaxValue);
        }



        /// <summary>
        /// Returns the string using rounded values
        /// </summary>
        /// <returns>minValue - maxValue</returns>
        public virtual string ToRoundString()
        {
            return string.Format("{0} - {1}", Mathf.RoundToInt(this.MinValue), Mathf.RoundToInt(this.MaxValue));
        }

        /// <summary>
        /// Returns the string using ceiled values
        /// </summary>
        /// <returns>minValue - maxValue</returns>
        public virtual string ToCeilingString()
        {
            return string.Format("{0} - {1}", Mathf.CeilToInt(this.MinValue), Mathf.CeilToInt(this.MaxValue));
        }

        /// <summary>
        /// Returns the string using floored values
        /// </summary>
        /// <returns>minValue - maxValue</returns>
        public virtual string ToFloorString()
        {
            return string.Format("{0} - {1}", Mathf.FloorToInt(this.MinValue), Mathf.FloorToInt(this.MaxValue));
        }

        public override bool IsInRange(float valueToCheckIfIsInRange)
        {
            return (valueToCheckIfIsInRange > MinValue && valueToCheckIfIsInRange < MaxValue);
        }
    }
}

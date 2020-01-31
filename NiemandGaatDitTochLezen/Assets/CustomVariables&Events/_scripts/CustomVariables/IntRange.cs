using System;

namespace SjorsGielen.CustomVariables.RangeVariables
{
    [Serializable]
    public class IntRange : AbstractRangeVariable<int>
    {
        public override int GetRandomValue()
        {
            return UnityEngine.Random.Range(this.MinValue, this.MaxValue);
        }

        public override bool IsInRange(int valueToCheckIfIsInRange)
        {
            return (valueToCheckIfIsInRange > MinValue && valueToCheckIfIsInRange < MaxValue);
        }
    }
}

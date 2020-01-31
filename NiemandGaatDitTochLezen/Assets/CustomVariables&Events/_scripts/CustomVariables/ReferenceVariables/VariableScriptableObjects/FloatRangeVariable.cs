using UnityEngine;
using SjorsGielen.CustomVariables.RangeVariables;
namespace SjorsGielen.CustomVariables
{
    [CreateAssetMenu(fileName = "NewFloatRangeVariable", menuName = "ReferenceVariables/FloatRangeVariable")]
    public class FloatRangeVariable : AbstractVariable<FloatRange>
    {
        public override void ApplyChange(FloatRange amount)
        {
            Value.MinValue += amount.MinValue;
            Value.MaxValue += amount.MaxValue;
        }

        public override void ApplyChange(AbstractVariable<FloatRange> amount)
        {
            Value.MinValue += amount.Value.MinValue;
            Value.MaxValue += amount.Value.MaxValue;
        }
    }
}

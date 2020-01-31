using UnityEngine;
namespace SjorsGielen.CustomVariables
{
    [CreateAssetMenu(fileName = "NewStringVariable", menuName = "ReferenceVariables/FloatRangeVariable")]
    public class StringVariable : AbstractVariable<string>
    {
        public override void ApplyChange(string amount)
        {
            Value += amount;
        }

        public override void ApplyChange(AbstractVariable<string> amount)
        {
            Value += amount.Value;
        }
    }
}

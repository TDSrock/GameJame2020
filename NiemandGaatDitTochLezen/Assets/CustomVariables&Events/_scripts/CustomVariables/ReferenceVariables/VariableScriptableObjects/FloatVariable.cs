using UnityEngine;
namespace SjorsGielen.CustomVariables
{
    [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "ReferenceVariables/FloatVariable")]
    public class FloatVariable : AbstractVariable<float>
    {
        public override void ApplyChange(float amount)
        {
            Value += amount;
        }

        public override void ApplyChange(AbstractVariable<float> amount)
        {
            Value += amount.Value;
        }
    }
}

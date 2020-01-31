using UnityEngine;
namespace SjorsGielen.CustomVariables
{
    [CreateAssetMenu(fileName = "NewDoubleVariable", menuName = "ReferenceVariables/DoubleVariable")]
    public class DoubleVariable : AbstractVariable<double>
    {
        public override void ApplyChange(double amount)
        {
            Value += amount;
        }

        public override void ApplyChange(AbstractVariable<double> amount)
        {
            Value += amount.Value;
        }
    }
}

using UnityEngine;
namespace SjorsGielen.CustomVariables
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = "ReferenceVariables/IntVariable")]
    public class IntVariable : AbstractVariable<int>
    {
        public override void ApplyChange(int amount)
        {
            Value += amount;
        }

        public override void ApplyChange(AbstractVariable<int> amount)
        {
            Value += amount.Value;
        }
    }
}

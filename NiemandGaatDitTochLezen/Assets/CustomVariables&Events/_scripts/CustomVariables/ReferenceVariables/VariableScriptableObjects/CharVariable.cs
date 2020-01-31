using UnityEngine;
namespace SjorsGielen.CustomVariables
{
    [CreateAssetMenu(fileName = "NewCharVariable", menuName = "ReferenceVariables/CharVariable")]
    public class CharVariable : AbstractVariable<char>
    {
        public override void ApplyChange(char amount)
        {
            Value += amount;
        }

        public override void ApplyChange(AbstractVariable<char> amount)
        {
            Value += amount.Value;
        }
    }
}

using UnityEngine;
namespace SjorsGielen.CustomVariables
{
    [CreateAssetMenu(fileName = "NewStringVariable", menuName = "ReferenceVariables/StringVariable")]
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

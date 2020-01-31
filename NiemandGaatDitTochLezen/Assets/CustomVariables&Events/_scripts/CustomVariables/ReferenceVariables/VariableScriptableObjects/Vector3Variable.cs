using UnityEngine;
namespace SjorsGielen.CustomVariables
{
    [CreateAssetMenu(fileName = "NewVector3Variable", menuName = "ReferenceVariables/Vector3Variable")]
    public class Vector3Variable : AbstractVariable<Vector3>
    {
        public override void ApplyChange(Vector3 amount)
        {
            Value += amount;
        }

        public override void ApplyChange(AbstractVariable<Vector3> amount)
        {
            Value += amount.Value;
        }
    }
}

using UnityEditor;
using UnityEngine;

namespace SjorsGielen.CustomVariables.RangeVariables.Editors
{
    [CustomPropertyDrawer(typeof(FloatRange))]
    public class FloatRangeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 32;
        }

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float rangeMin = 0;
            float rangeMax = 1;

            var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof(MinMaxRangeAttribute), true);
            if(ranges.Length > 0)
            {
                rangeMin = ranges[0].Min;
                rangeMax = ranges[0].Max;
            }
            SerializedProperty minValue = property.FindPropertyRelative("MinValue");
            SerializedProperty maxValue = property.FindPropertyRelative("MaxValue");

            var newMin = minValue.floatValue;
            var newMax = maxValue.floatValue;

            var xDivision = position.width * 0.35f;
            var yDivision = position.height * 0.4f;

            EditorGUI.LabelField(
                new Rect(
                    position.x,
                    position.y,
                    xDivision,
                    yDivision),
                label);

            string minRangeDisp = rangeMin.ToString("0.##");
            string maxRangeDisp = rangeMax.ToString("0.##");

            EditorGUI.LabelField(
                new Rect(
                    position.x + xDivision - 24f,
                    position.y + yDivision,
                    position.width,
                    yDivision),
                minRangeDisp);

            EditorGUI.LabelField(
                new Rect(
                    position.x + position.width - 24f,
                    position.y + yDivision,
                    position.width,
                    yDivision),
                maxRangeDisp);

            string deltaString = string.Format("D: {0:0.000}", (newMax - newMin));

            EditorGUI.MinMaxSlider(
                new Rect(
                    position.x + xDivision + minRangeDisp.Length * 2,
                    position.y + yDivision,
                    position.width - xDivision - 24f - maxRangeDisp.Length * 2,
                    yDivision),
                ref newMin,
                ref newMax,
                rangeMin,
                rangeMax);

            EditorGUI.LabelField(
                new Rect(
                    position.x,
                    position.y + yDivision,
                    position.width - 48f,
                    yDivision),
                new GUIContent(deltaString));

            EditorGUI.LabelField(
                new Rect(
                    position.x + xDivision - 7,
                    position.y,
                    xDivision,
                    yDivision),
                "From: ");

            EditorGUI.LabelField(
                new Rect(
                    position.x + xDivision - 7,
                    position.y,
                    xDivision,
                    yDivision),
                "From: ");

            newMin = Mathf.Clamp(
                EditorGUI.FloatField(
                    new Rect(
                        position.x + xDivision + 30,
                        position.y,
                        xDivision - 30,
                        yDivision),
                    newMin),
                rangeMin, newMax);

            EditorGUI.LabelField(
                new Rect(
                    position.x + xDivision * 2f,
                    position.y,
                    xDivision,
                    yDivision),
                "To: ");

            newMax = Mathf.Clamp(
                EditorGUI.FloatField(
                    new Rect(
                        position.x + xDivision * 2f + 24,
                        position.y,
                        xDivision - 24,
                        yDivision),
                    newMax),
                newMin,
                rangeMax);

            minValue.floatValue = newMin;
            maxValue.floatValue = newMax;
        }
    }
}
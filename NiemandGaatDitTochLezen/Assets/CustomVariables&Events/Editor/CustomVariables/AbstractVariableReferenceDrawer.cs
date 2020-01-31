using UnityEngine;
using UnityEditor;
using SjorsGielen.CustomVariables.RangeVariables;

namespace SjorsGielen.CustomVariables.ReferenceVariables.Editors
{
    [CustomPropertyDrawer(typeof(CharReference))]
    [CustomPropertyDrawer(typeof(DoubleReference))]
    [CustomPropertyDrawer(typeof(FloatReference))]
    [CustomPropertyDrawer(typeof(IntReference))]
    [CustomPropertyDrawer(typeof(StringReference))]
    [CustomPropertyDrawer(typeof(Vector3Reference))]
    [CustomPropertyDrawer(typeof(FloatRangeReference))]
    public class AbstractVariableReferenceDrawer : PropertyDrawer
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        private readonly string[] popupOptions =
            { "Use Constant", "Use Variable" };

        private readonly float extraSpaceForRangeProps = 16f;

        private readonly System.Type[] RangeTypes =
            { typeof(FloatRange), typeof(IntRange) };

        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // Get properties
            SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
            SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
            SerializedProperty variable = property.FindPropertyRelative("Variable");

            var propToDraw = useConstant.boolValue ? constantValue : variable;
            if (useConstant.boolValue)
            {
                foreach (System.Type type in RangeTypes)
                {
                    var typeThatNeedsMoreSpaceSplit = type.ToString().Split('.');
                    var typeThatNeedsMoreSpace = typeThatNeedsMoreSpaceSplit[typeThatNeedsMoreSpaceSplit.Length - 1];//remove the namespace part of the string
                    var typeFound = constantValue.type;
                    if (typeThatNeedsMoreSpace == typeFound)
                    {
                        position = new Rect(position.x, position.y, position.width, position.height + extraSpaceForRangeProps);
                        break;
                    }
                }
            }

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

            useConstant.boolValue = result == 0;

            EditorGUI.PropertyField(position,
                propToDraw,
                GUIContent.none,
                true);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
            SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");

            if (useConstant.boolValue)
            {
                foreach (System.Type type in RangeTypes)
                {
                    var typeThatNeedsMoreSpaceSplit = type.ToString().Split('.');
                    var typeThatNeedsMoreSpace = typeThatNeedsMoreSpaceSplit[typeThatNeedsMoreSpaceSplit.Length - 1];//remove the namespace part of the string
                    var typeFound = constantValue.type;
                    if (typeThatNeedsMoreSpace == typeFound)
                    {
                        return base.GetPropertyHeight(property, label) + extraSpaceForRangeProps;
                    }
                }
            }
            return base.GetPropertyHeight(property, label);
        }
    }
}

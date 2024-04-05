using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using _Scripts.CoreGame.InteractionSystems.Attributes;

namespace _Scripts.CoreGame.Editor
{
    [CustomPropertyDrawer(typeof(DanmakuRoleClassAttribute))]

    public class DanmakuRoleDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Get a list of all class types that are marked with the RoleClass attribute
            var roleTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute<DanmakuRoleClassAttribute>() != null)
                .ToArray();

            // Create an array of strings for the dropdown options
            var options = roleTypes.Select(t => t.Name).ToArray();

            // Get the index of the currently selected option
            var currentIndex = Array.IndexOf(options, property.stringValue);

            // Draw the dropdown
            var selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, options);

            // Save the selected option
            if (selectedIndex >= 0)
            {
                property.stringValue = options[selectedIndex];
            }
        }
    }
}
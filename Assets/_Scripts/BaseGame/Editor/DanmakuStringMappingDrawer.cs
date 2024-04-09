using System;
using System.Linq;
using System.Reflection;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEditor;
using UnityEngine;

namespace _Scripts.CoreGame.Editor
{
    public class DanmakuStringMappingDrawer<TFindingType, TWithClassAttribute> : PropertyDrawer where TWithClassAttribute : Attribute
    {
        private string[] _options;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            // Update the options when the dropdown is opened
            var roleTypes = Assembly.GetAssembly(typeof(TFindingType)).GetTypes()
                .Where(t => t.GetCustomAttribute<TWithClassAttribute>() != null)
                .ToArray();

            _options = roleTypes.Select(t => t.Name).ToArray();
            
            _options ??= Array.Empty<string>();
            
            // Get the index of the currently selected option
            var currentIndex = Array.IndexOf(_options, property.stringValue);

            // Draw the dropdown
            var selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, _options);

            // Save the selected option
            if (selectedIndex >= 0)
            {
                property.stringValue = _options[selectedIndex];
            }
            
        }
    }
}
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Roles;

namespace _Scripts.CoreGame.Editor
{
    [CustomPropertyDrawer(typeof(DanmakuRolePropertyAttribute))]
    public class DanmakuRoleDrawer : PropertyDrawer
    {
        private string[] _options;
        private bool _isDropdownOpened = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Check if the dropdown button is clicked
            if (Event.current.type == EventType.MouseDown && position.Contains(Event.current.mousePosition))
            {
                // Update the options when the dropdown is opened
                var roleTypes = Assembly.GetAssembly(typeof(IDanmakuRole)).GetTypes()
                    .Where(t => t.GetCustomAttribute<DanmakuRoleClassAttribute>() != null)
                    .ToArray();

                _options = roleTypes.Select(t => t.Name).ToArray();
                _isDropdownOpened = true;
            }
            
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

            // Reset the flag when the dropdown is closed
            if (_isDropdownOpened && Event.current.type == EventType.Layout)
            {
                _isDropdownOpened = false;
            }
        }
    }
}
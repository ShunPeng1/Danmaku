using UnityEditor;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Roles;

namespace _Scripts.CoreGame.Editor
{
    [CustomPropertyDrawer(typeof(DanmakuRolePropertyAttribute))]
    public class DanmakuRoleDrawer : DanmakuStringMappingDrawer<IDanmakuRole, DanmakuRoleClassAttribute>
    {
        
    }
}
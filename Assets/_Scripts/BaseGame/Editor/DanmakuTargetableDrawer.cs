using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using UnityEditor;

namespace _Scripts.CoreGame.Editor
{
    
    [CustomPropertyDrawer(typeof(DanmakuTargetablePropertyAttribute))]
    public class DanmakuTargetableDrawer : DanmakuStringMappingDrawer<IDanmakuTargetable, DanmakuTargetableClassAttribute>
    {
        
    }
    
    
}
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems
{
    
    public class DanmakuCardExecutionParameter
    {
        public readonly IDanmakuCard Card;
        public readonly IDanmakuCardRule DanmakuCardRule;
        public readonly IDanmakuActivator Activator;
        public readonly List<IDanmakuTargetable> Targetable;
            
        public DanmakuCardExecutionParameter(IDanmakuCard card, IDanmakuCardRule danmakuCardRule, IDanmakuActivator activator)
        {
            Card = card;
            DanmakuCardRule = danmakuCardRule;
            Activator = activator;
            Targetable = new ();
        }
        
        
        public void AddTarget(IDanmakuTargetable target)
        {
            Targetable.Add(target);
        }
        
        public void AddTarget(DanmakuSessionChoice sessionChoice)
        {
            Targetable.Add(sessionChoice.SelectedTarget);
        }
            
        public void AddTargets(List<IDanmakuTargetable> targets)
        {
            Targetable.AddRange(targets);
        }
        
        public void ClearTargets()
        {
            Targetable.Clear();
        }
        
    } 
}
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems
{
    
    public class DanmakuCardExecutionParameter
    {
        public readonly IDanmakuCard Card;
        public readonly IDanmakuCardRule CardRule;
        public readonly IDanmakuActivator Activator;
        public readonly List<IDanmakuTargetable> Targetables;
            
        public DanmakuCardExecutionParameter(IDanmakuCard card, IDanmakuCardRule cardRule, IDanmakuActivator activator)
        {
            Card = card;
            CardRule = cardRule;
            Activator = activator;
            Targetables = new ();
        }
        
        
        public void AddTarget(IDanmakuTargetable target)
        {
            Targetables.Add(target);
        }
        
        public void AddTarget(DanmakuSessionChoice sessionChoice)
        {
            Targetables.Add(sessionChoice.SelectedTarget);
        }
            
        public void AddTargets(List<IDanmakuTargetable> targets)
        {
            Targetables.AddRange(targets);
        }
        
        public void ClearTargets()
        {
            Targetables.Clear();
        }
        
    } 
}
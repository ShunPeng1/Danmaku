using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Abstracts;

namespace _Scripts.CoreGame.InteractionSystems
{
    public enum ChoiceActionEnum
    {
        Select,
        Discard
    }
    
    public class DanmakuSessionChoice
    {
        public DanmakuSessionMenu Menu { get; private set; }
        public List<IDanmakuTargetable> Targetables { get; private set; }
        public Func<IDanmakuTargetable, bool> CardFilter { get; private set; }
        public IDanmakuTargetable SelectedTarget { get; private set; }
        public ChoiceActionEnum ChoiceAction { get; private set; }
        public Type TargetType => Targetables[0].GetType(); 
        
        public DanmakuSessionChoice(DanmakuSessionMenu menu, List<IDanmakuTargetable> targetables, ChoiceActionEnum choiceAction, Func<IDanmakuTargetable, bool> cardFilter = null)
        {
            if (targetables.Count == 0)
            {
                throw new ArgumentException("Targetables list cannot be empty");
            }
         
            Targetables = targetables;
            ChoiceAction = choiceAction;
            Menu = menu;
            CardFilter = cardFilter;
            
            CardFilter += targetables.Contains; // Ensure that the target is in the list of targetables
        }
        
        public void SelectTarget(IDanmakuTargetable target)
        {
            if (IsSelectionValid(target))
            {
                SelectedTarget = target;
            }
        }
        
        public void DeselectTarget()
        {
            SelectedTarget = null;
        }
        
        public bool IsSelectionValid(IDanmakuTargetable targetable)
        {
            return CardFilter(targetable) && Targetables.Contains(targetable);
        }
        
        public bool IsChosen()
        {
            return SelectedTarget != null;
        }
        
        
    }
}
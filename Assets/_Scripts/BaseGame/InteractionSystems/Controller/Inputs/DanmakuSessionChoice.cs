using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Abstracts;

namespace _Scripts.CoreGame.InteractionSystems
{
    
    public class DanmakuSessionChoice
    {
        public DanmakuSessionMenu Menu { get; private set; }
        public List<IDanmakuTargetable> Targetables { get; private set; }
        public Func<IDanmakuTargetable, bool> CardFilter { get; private set; }
        public IDanmakuTargetable SelectedTarget { get; private set; }
        public Type TargetType => Targetables[0].GetType(); 
        public Action<IDanmakuTargetable> OnTargetSelected { get; set; } = delegate{};
        public Action<IDanmakuTargetable> OnTargetDeselected { get; set; } = delegate{}; 
        
        public DanmakuSessionChoice(DanmakuSessionMenu menu, List<IDanmakuTargetable> targetables, Func<IDanmakuTargetable, bool> cardFilter = null)
        {
            if (targetables.Count == 0)
            {
                throw new ArgumentException("Targetables list cannot be empty");
            }
         
            Targetables = targetables;
            Menu = menu;
            
            if (cardFilter == null)
            {
                CardFilter = (targetable) => true;
            }
            else
            {
                CardFilter = cardFilter;
            }
            
        }
        
        public DanmakuSessionChoice(DanmakuSessionMenu menu, IDanmakuTargetable targetable)
        {
            Targetables = new List<IDanmakuTargetable>(){targetable};
            Menu = menu;
            
            SelectedTarget = targetable;
        }
        
        public void SelectTarget(IDanmakuTargetable target)
        {
            if (IsSelectionValid(target))
            {
                SelectedTarget = target;
                OnTargetSelected?.Invoke(target);
                
               
                if (Menu.ChoiceAction == ChoiceActionEnum.AutoCheck)
                {
                    Menu.TryEndSession();
                }
            }
        }
        
        public void DeselectTarget()
        {
            OnTargetDeselected?.Invoke(SelectedTarget);
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
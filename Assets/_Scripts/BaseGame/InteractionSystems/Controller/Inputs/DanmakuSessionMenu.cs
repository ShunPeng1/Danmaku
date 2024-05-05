using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Enum;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuSessionMenu
    {
        public DanmakuSession Session { get; private set; }
        public IDanmakuActivator Activator { get; private set; }
        public List<DanmakuSessionChoice> SessionChoices { get; private set; }
        
        public DanmakuSessionMenu(DanmakuSession session, IDanmakuActivator activator, List<DanmakuSessionChoice> sessionChoices)
        {
            Session = session;
            Activator = activator;
            SessionChoices = sessionChoices;
        }
        
        public void AddSessionChoice(DanmakuSessionChoice sessionChoice)
        {
            SessionChoices.Add(sessionChoice);
        }
        
        public void RemoveSessionChoice(DanmakuSessionChoice sessionChoice)
        {
            SessionChoices.Remove(sessionChoice);
        }

        public void ClearSessionChoices()
        {
            SessionChoices.Clear();
        }

        public bool IsAllChosen()
        {
            return SessionChoices.TrueForAll(choice => choice.IsChosen());
        }
        

    }
}
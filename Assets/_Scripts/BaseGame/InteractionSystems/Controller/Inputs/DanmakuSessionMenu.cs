using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Enum;

namespace _Scripts.CoreGame.InteractionSystems
{
    
    public enum ChoiceActionEnum
    {
        Confirm,
        AutoCheck,
    }
    
    public enum ChoiceEndCheckEnum
    {
        AllPlayed, // All players need to play a card to end the session
        AnyPlayed,  // Only one player needs to play a card to end the session
        NonePlayed, // Player does not need to play any card to end the session
    }
    
    public class DanmakuSessionMenu
    {
        public DanmakuSession Session { get; private set; }
        public IDanmakuActivator Activator { get; private set; }
        public List<DanmakuSessionChoice> SessionChoices { get; private set; }
        
        public ChoiceActionEnum ChoiceAction { get; private set; }
        public ChoiceEndCheckEnum ChoiceEndCheck { get; private set; }
        public DanmakuSessionMenu(DanmakuSession session, IDanmakuActivator activator, List<DanmakuSessionChoice> sessionChoices, ChoiceActionEnum choiceAction, ChoiceEndCheckEnum choiceEndCheck = ChoiceEndCheckEnum.AllPlayed)
        {
            Session = session;
            Activator = activator;
            SessionChoices = sessionChoices;
            ChoiceAction = choiceAction;
            ChoiceEndCheck = choiceEndCheck;
            
        }
        
        public bool CheckEndSession()
        {
            switch (ChoiceEndCheck)
            {
                case ChoiceEndCheckEnum.AllPlayed:
                    return SessionChoices.TrueForAll(choice => choice.IsChosen());;
                
                case ChoiceEndCheckEnum.AnyPlayed:
                    return SessionChoices.Exists(choice => choice.IsChosen());
                
                case ChoiceEndCheckEnum.NonePlayed:
                    return true;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
        
        public bool TryEndSession()
        {
            if (!CheckEndSession())
            {
                return false;
            }
            
            return Session.TryEndSession();
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
        

    }
}
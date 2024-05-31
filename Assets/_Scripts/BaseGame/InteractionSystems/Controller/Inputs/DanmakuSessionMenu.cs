using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Enum;
using Shun_Utilities;

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
    
    public enum MenuOutcomeEnum
    {
        Good,
        Bad,
        Neutral,
    }

    public class DanmakuSessionMenuDetail
    {
        public string Title { get; private set; }
        public MenuOutcomeEnum MenuOutcome { get; private set; }
        
        public string Keyword { get; private set; }
        public DanmakuSessionMenuDetail(string title = "", MenuOutcomeEnum menuOutcome = MenuOutcomeEnum.Neutral, string keyword = "")
        {
            Title = title;
            MenuOutcome = menuOutcome;
            Keyword = keyword;
        }
        
    }
    
    public class DanmakuSessionMenu
    {
        
        public DanmakuSession Session { get; private set; }
        public IDanmakuActivator Activator { get; private set; }
        public List<DanmakuSessionChoice> SessionChoices { get; private set; }
        
        public ChoiceActionEnum ChoiceAction { get; private set; }
        public ChoiceEndCheckEnum ChoiceEndCheck { get; private set; }
        
        
        public ObservableData<DanmakuSessionMenuDetail> Detail { get; set; }
        public DanmakuSessionMenu(DanmakuSession session, IDanmakuActivator activator, List<DanmakuSessionChoice> sessionChoices, ChoiceActionEnum choiceAction, ChoiceEndCheckEnum choiceEndCheck = ChoiceEndCheckEnum.AllPlayed, DanmakuSessionMenuDetail detail = null)
        {
            Session = session;
            Activator = activator;
            SessionChoices = sessionChoices;
            ChoiceAction = choiceAction;
            ChoiceEndCheck = choiceEndCheck;
            
            Detail = detail != null ?  new ObservableData<DanmakuSessionMenuDetail>(detail) : new ObservableData<DanmakuSessionMenuDetail>(new DanmakuSessionMenuDetail());
            
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
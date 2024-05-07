﻿using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Abstracts;
using BNG;
using Shun_State_Machine;
using Shun_Utilities;

namespace _Scripts.CoreGame.InteractionSystems
{
    public enum EndSessionKindEnum
    {
        AllPlayed, // All players need to play a card to end the session
        AnyPlayed,  // Only one player needs to play a card to end the session
        NonePlayed, // Player does not need to play any card to end the session
    }
    
    public class DanmakuSession
    {
        public DanmakuInteractionController DanmakuInteractionController { get; private set; }
        private readonly EndSessionKindEnum _sessionKindEnum;
        public List<IDanmakuActivator> PlayingPlayerModel { get; private set; }
        public ObservableList<DanmakuSessionMenu> PlayingSessionMenus { get; private set; }
        public Countdown Countdown { get; private set; }
        public DanmakuSessionEvent OnSessionStartEvent { get; set; }
        public DanmakuSessionEvent OnSessionEndEvent { get; set; }
        public DanmakuSessionEvent OnForceEndSessionEvent { get; set; }
        public bool CanEndSession => UpdateEndSession();
       
        private DanmakuSession(DanmakuInteractionController danmakuInteractionController,
            List<IDanmakuActivator> playingPlayerModel,
            ObservableList<DanmakuSessionMenu> playingSessionMenus, 
            EndSessionKindEnum sessionKindEnum,
            Countdown countdown,
            DanmakuSessionEvent onSessionStartEvent,
            DanmakuSessionEvent onSessionEndEvent,
            DanmakuSessionEvent onForceEndSessionEvent)  
        {
            DanmakuInteractionController = danmakuInteractionController;
            _sessionKindEnum = sessionKindEnum;
            PlayingPlayerModel = playingPlayerModel;
            Countdown = countdown;
            PlayingSessionMenus = playingSessionMenus;
            OnSessionStartEvent = onSessionStartEvent;
            OnSessionEndEvent = onSessionEndEvent;
            OnForceEndSessionEvent = onForceEndSessionEvent;
            
            UpdateEndSession();
        }
        
        public class Builder
        {
            private EndSessionKindEnum _endSessionKindEnum = EndSessionKindEnum.AnyPlayed; // Default is AnyPlayed
            private List<IDanmakuActivator> _playingPlayerModel = new List<IDanmakuActivator>(); // Default is empty
            private ObservableList<DanmakuSessionMenu> _playingSessionMenus = new (); // Default is empty
            private Countdown _countdown = new Countdown(false, float.PositiveInfinity); // Default is infinite
            private DanmakuSessionEvent _onSessionStartEvent = new DanmakuSessionEvent();
            private DanmakuSessionEvent _onSessionEndEvent = new DanmakuSessionEvent();
            private DanmakuSessionEvent _onForceEndSessionEvent = new DanmakuSessionEvent();
            
            public Builder WithPlayerSessionKindEnum(EndSessionKindEnum endSessionKindEnum)
            {
                _endSessionKindEnum = endSessionKindEnum;
                return this;
            }

            public Builder WithPlayingPlayerModel(List<IDanmakuActivator> playingPlayerModel)
            {
                _playingPlayerModel = playingPlayerModel;
                return this;
            }

            public Builder WithPlayingSessionMenus(ObservableList<DanmakuSessionMenu> playingSessionMenus)
            {
                _playingSessionMenus = playingSessionMenus;
                return this;
            }

            public Builder WithCountDownTime(float countDownTime)
            {
                _countdown = new Countdown(false, countDownTime);
                return this;
            }
            
            public Builder WithOnSessionStart(Action onSessionStart)
            {
                _onSessionStartEvent.Subscribe(onSessionStart);
                return this;
            }
            
            public Builder WithOnSessionEnd(Action onSessionEnd, bool isAlsoSubcribeToForceEnd = false)
            {
                _onSessionEndEvent.Subscribe(onSessionEnd);
                if (isAlsoSubcribeToForceEnd)
                {
                    _onForceEndSessionEvent.Subscribe(onSessionEnd);
                }
                return this;
            }
            
            public Builder WithOnSessionEnd(Action<List<DanmakuSessionMenu>> onSessionEnd, bool isAlsoSubcribeToForceEnd = false)
            {
                _onSessionEndEvent.Subscribe(onSessionEnd);
                if (isAlsoSubcribeToForceEnd)
                {
                    _onForceEndSessionEvent.Subscribe(onSessionEnd);
                }
                return this;
            }
            
            public Builder WithOnForceEndSession(Action onForceEndSession)
            {
                _onForceEndSessionEvent.Subscribe(onForceEndSession);
                
                return this;
            }
            
            
            public Builder WithOnForceEndSession(Action<List<DanmakuSessionMenu>> onForceEndSession)
            {
                _onForceEndSessionEvent.Subscribe(onForceEndSession);
                return this;
            }
           
            public DanmakuSession Build(DanmakuInteractionController danmakuInteractionController)
            {
                return new DanmakuSession(
                    danmakuInteractionController,
                    _playingPlayerModel,
                    _playingSessionMenus,
                    _endSessionKindEnum,
                    _countdown,
                    _onSessionStartEvent,
                    _onSessionEndEvent,
                    _onForceEndSessionEvent
                );
            }
        }
        
        public void StartSession()
        {
            OnSessionStartEvent.Invoke(PlayingSessionMenus.List.ToList());
            Countdown.Reset();
        }
        
        public void UpdateSession(float deltaTime)
        {
            bool isEnded = Countdown.Progress(deltaTime);
            if (isEnded)
            {
                OnForceEndSessionEvent.Invoke(PlayingSessionMenus.List.ToList());
            }
        }

        private void EndSession()
        {
            OnSessionEndEvent.Invoke(PlayingSessionMenus.List.ToList());
        }
        
        public bool TryEndSession()
        {
            if (!CanEndSession) return false;
            
            EndSession();
            return true;

        }

        private bool UpdateEndSession()
        {
            return _sessionKindEnum switch
            {
                EndSessionKindEnum.AllPlayed => PlayingSessionMenus.List.ToList().TrueForAll(menu => menu.IsAllChosen()),
                EndSessionKindEnum.AnyPlayed => PlayingSessionMenus.Any(menu => menu.IsAllChosen()),
                EndSessionKindEnum.NonePlayed => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        
        public void AddSessionMenu(DanmakuSessionMenu sessionMenu)
        {
            if (PlayingPlayerModel.Contains(sessionMenu.Activator) && PlayingSessionMenus.All(menu => menu.Activator != sessionMenu.Activator))
            {
                PlayingSessionMenus.Add(sessionMenu);   
            }
        }
        
        public void RemoveSessionMenu(DanmakuSessionMenu sessionMenu)
        {
            PlayingSessionMenus.Remove(sessionMenu);
        }
        
        public void ClearSessionMenus()
        {
            PlayingSessionMenus.Clear();
        }
        
        public List<DanmakuSessionMenu> GetPlayerSessionMenus(IDanmakuActivator activator)
        {
            return PlayingSessionMenus.Where(menu => menu.Activator == activator).ToList();
        }
        
        
        
        public void SubscribeOnSessionEnd(Action onSessionEnd, bool isAlsoSubcribeToForceEnd = false)
        {
            OnSessionEndEvent.Subscribe(onSessionEnd);
            if (isAlsoSubcribeToForceEnd)
            {
                OnForceEndSessionEvent.Subscribe(onSessionEnd);
            }
        }
        
        public void SubscribeOnSessionEnd(Action<List<DanmakuSessionMenu>> onSessionEnd, bool isAlsoSubcribeToForceEnd = false)
        {
            OnSessionEndEvent.Subscribe(onSessionEnd);
            if (isAlsoSubcribeToForceEnd)
            {
                OnForceEndSessionEvent.Subscribe(onSessionEnd);
            }
        }


        public void SubscribeOnSessionEnd(Action<DanmakuSession> removeSessionFromPlayer, bool isAlsoSubcribeToForceEnd = false)
        {
            OnSessionEndEvent.Subscribe(removeSessionFromPlayer);
            if (isAlsoSubcribeToForceEnd)
            {
                OnForceEndSessionEvent.Subscribe(removeSessionFromPlayer);
            }
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Abstracts;
using BNG;
using Shun_State_Machine;
using Shun_Utilities;

namespace _Scripts.CoreGame.InteractionSystems
{
    
    public enum MenuEndCheckEnum
    {
        AllPlayed,
        AnyPlayed,
        NonePlayed
    }
    
    public class DanmakuSession
    {
        public DanmakuInteractionController DanmakuInteractionController { get; private set; }
        private readonly MenuEndCheckEnum _menuEndCheckEnum;
        public List<IDanmakuActivator> PlayingPlayerModel { get; private set; }
        public List<DanmakuSessionMenu> PlayingSessionMenus { get; private set;}
        public Countdown Countdown { get; private set; }
        public DanmakuSessionEvent OnSessionStartEvent { get; set; }
        public DanmakuSessionEvent OnSessionEndEvent { get; set; }
        public DanmakuSessionEvent OnForceEndSessionEvent { get; set; }
        public DanmakuSessionEvent OnFinallyEndSessionEvent { get; set; }
        public DanmakuSessionMenuEvent OnMenuAdded { get; set; }
        public DanmakuSessionMenuEvent OnMenuRemoved { get; set; }
        public bool CanEndSession => UpdateEndSession();
        public bool IsEnded = false;
       
        private DanmakuSession(DanmakuInteractionController danmakuInteractionController,
            List<IDanmakuActivator> playingPlayerModel,
            List<DanmakuSessionMenu> playingSessionMenus, 
            MenuEndCheckEnum menuEndCheckEnum,
            Countdown countdown)  
        {
            DanmakuInteractionController = danmakuInteractionController;
            _menuEndCheckEnum = menuEndCheckEnum;
            PlayingPlayerModel = playingPlayerModel;
            Countdown = countdown;
            PlayingSessionMenus = playingSessionMenus;
            
            UpdateEndSession();
        }

        private void UpdateEvent(DanmakuSessionEvent onSessionStartEvent,
            DanmakuSessionEvent onSessionEndEvent,
            DanmakuSessionEvent onForceEndSessionEvent,
            DanmakuSessionEvent onFinallyEndSessionEvent,
            DanmakuSessionMenuEvent onMenuAdded,
            DanmakuSessionMenuEvent onMenuRemoved)
        {
            OnSessionStartEvent = onSessionStartEvent;
            OnSessionEndEvent = onSessionEndEvent;
            OnForceEndSessionEvent = onForceEndSessionEvent;
            OnFinallyEndSessionEvent = onFinallyEndSessionEvent;
            
            OnMenuAdded = onMenuAdded;
            OnMenuRemoved = onMenuRemoved;
            
        }
        
        public class Builder
        {
            private MenuEndCheckEnum _menuEndCheckEnum = MenuEndCheckEnum.AnyPlayed; // Default is AnyPlayed
            private List<IDanmakuActivator> _playingPlayerModel = new List<IDanmakuActivator>(); // Default is empty
            private List<DanmakuSessionMenu> _playingSessionMenus = new (); // Default is empty
            private Countdown _countdown = new Countdown(false, float.PositiveInfinity); // Default is infinite
            
            public Builder WithPlayerSessionKindEnum(MenuEndCheckEnum menuEndCheckEnum)
            {
                _menuEndCheckEnum = menuEndCheckEnum;
                return this;
            }

            public Builder WithPlayingPlayerModel(List<IDanmakuActivator> playingPlayerModel)
            {
                _playingPlayerModel = playingPlayerModel;
                return this;
            }

            public Builder WithPlayingSessionMenus(List<DanmakuSessionMenu> playingSessionMenus)
            {
                _playingSessionMenus = playingSessionMenus;
                return this;
            }

            public Builder WithCountDownTime(float countDownTime)
            {
                _countdown = new Countdown(false, countDownTime);
                return this;
            }
            
           
            
            
            public DanmakuSession Build(DanmakuInteractionController danmakuInteractionController)
            {
                var session = new DanmakuSession(
                    danmakuInteractionController,
                    _playingPlayerModel,
                    _playingSessionMenus,
                    _menuEndCheckEnum,
                    _countdown
                );
                
                session.UpdateEvent(
                    new DanmakuSessionEvent(session), 
                    new DanmakuSessionEvent(session), 
                    new DanmakuSessionEvent(session),
                    new DanmakuSessionEvent(session),
                    new DanmakuSessionMenuEvent(),
                    new DanmakuSessionMenuEvent());
                
                return session;
            }
        }
        
        public void StartSession()
        {
            OnSessionStartEvent.Invoke();
            Countdown.Reset();
        }
        
        public void UpdateSession(float deltaTime)
        {
            bool isEnded = Countdown.Progress(deltaTime);
            if (isEnded)
            {
                ForceEndSession();
            }
        }

        private void EndSession()
        {
            OnSessionEndEvent.Invoke();
            ClearSessionMenus();
            IsEnded = true;
            OnFinallyEndSessionEvent.Invoke();
        }

        private void ForceEndSession()
        {
            OnForceEndSessionEvent.Invoke();
            ClearSessionMenus();
            IsEnded = true;
            OnFinallyEndSessionEvent.Invoke();
            
        }
        
        public bool TryEndSession()
        {
            if (!CanEndSession) return false;
            
            EndSession();
            return true;

        }

        private bool UpdateEndSession()
        {
            return _menuEndCheckEnum switch
            {
                MenuEndCheckEnum.AllPlayed => PlayingSessionMenus.TrueForAll(menu => menu.CheckEndSession()),
                MenuEndCheckEnum.AnyPlayed => PlayingSessionMenus.Any(menu => menu.CheckEndSession()),
                MenuEndCheckEnum.NonePlayed => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        
        public void AddSessionMenu(DanmakuSessionMenu sessionMenu)
        {
            //if (PlayingPlayerModel.Contains(sessionMenu.Activator) && PlayingSessionMenus.All(menu => menu.Activator != sessionMenu.Activator))
            if (PlayingPlayerModel.Contains(sessionMenu.Activator))
            {
                PlayingSessionMenus.Add(sessionMenu);   
                OnMenuAdded.Invoke(sessionMenu);
            }
        }
        
        public void RemoveSessionMenu(DanmakuSessionMenu sessionMenu)
        {
            if (!PlayingSessionMenus.Contains(sessionMenu))
            {
                return;
            }
            
            PlayingSessionMenus.Remove(sessionMenu);
            OnMenuRemoved.Invoke(sessionMenu);
        }
        
        public List<DanmakuSessionMenu> GetSessionMenus()
        {
            return PlayingSessionMenus;
        }
        public List<DanmakuSessionMenu> GetPlayerSessionMenus(DanmakuPlayerModel activator)
        {
            return PlayingSessionMenus.Where(menu => menu.Activator == activator).ToList();
        }
        
        public void ClearSessionMenus()
        {
            foreach (var sessionMenu in PlayingSessionMenus)
            {
                OnMenuRemoved.Invoke(sessionMenu);
            }
            
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
        
        public void SubscribeOnSessionEnd(Action<DanmakuSession> removeSessionFromPlayer, bool isAlsoSubcribeToForceEnd = false)
        {
            OnSessionEndEvent.Subscribe(removeSessionFromPlayer);
            if (isAlsoSubcribeToForceEnd)
            {
                OnForceEndSessionEvent.Subscribe(removeSessionFromPlayer);
            }
        }

        public void SubscribeOnMenuAdded(Action<DanmakuSessionMenu> onMenuAdded)
        {
            OnMenuAdded.Subscribe(onMenuAdded);
        }
        
        public void SubscribeOnMenuRemoved(Action<DanmakuSessionMenu> onMenuRemoved)
        {
            OnMenuRemoved.Subscribe(onMenuRemoved);
        }
        
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuSessionEvent
    {
        private DanmakuSession _session;
        public Action NoParamEvent { get; private set; }
        public Action<List<DanmakuSessionMenu>> MenusEvent { get; private set; }
        public Action<DanmakuSession> SessionEvent { get; private set; }
        
        public DanmakuSessionEvent(DanmakuSession session)
        {
            _session = session;
        }
        
        public void Subscribe(Action noParamEvent, int priority = 0)
        {
            NoParamEvent += noParamEvent;
        }
        
        public void Subscribe(Action<List<DanmakuSessionMenu>> menusEvent, int priority = 0)
        {
            MenusEvent += menusEvent;
        }
        
        public void Subscribe(Action<DanmakuSession> sessionEvent, int priority = 0)
        {
            SessionEvent += sessionEvent;
        }
        
        public void Unsubscribe(Action @event)
        {
            NoParamEvent -= @event;
        }
        
        public void Unsubscribe(Action<List<DanmakuSessionMenu>> menusEvent)
        {
            MenusEvent -= menusEvent;
        }
        
        public void Unsubscribe(Action<DanmakuSession> sessionEvent)
        {
            SessionEvent -= sessionEvent;
        }
        
        public void UnsubscribeAll()
        {
            NoParamEvent = null;
            MenusEvent = null;
        }
        
        public void Invoke()
        {
            NoParamEvent?.Invoke();
            MenusEvent?.Invoke(_session.PlayingSessionMenus.List.ToList());
            SessionEvent?.Invoke(_session); 
        }
        
    }
}
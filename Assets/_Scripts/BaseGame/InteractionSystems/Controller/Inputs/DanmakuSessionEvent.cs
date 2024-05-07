using System;
using System.Collections.Generic;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuSessionEvent
    {
        public Action NoParamEvent { get; private set; }
        public Action<List<DanmakuSessionMenu>> MenusEvent { get; private set; }
        public Action<DanmakuSession> SessionEvent { get; private set; }
        
        public DanmakuSessionEvent(Action noParamEvent = null, Action<List<DanmakuSessionMenu>> menusEvent = null, Action<DanmakuSession> sessionEvent = null)
        {
            NoParamEvent = noParamEvent;
            MenusEvent = menusEvent;
            SessionEvent = sessionEvent;
        }
        
        public DanmakuSessionEvent(Action<List<DanmakuSessionMenu>> menusEvent)
        {
            MenusEvent = menusEvent;
        }
        
        public DanmakuSessionEvent(Action @event)
        {
            NoParamEvent = @event;
        }
        
        public void Subscribe(Action @event)
        {
            NoParamEvent += @event;
        }
        
        public void Subscribe(Action<List<DanmakuSessionMenu>> menusEvent)
        {
            MenusEvent += menusEvent;
        }
        
        public void Subscribe(Action<DanmakuSession> sessionEvent)
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
        
        public void Invoke(List<DanmakuSessionMenu> menus)
        {
            NoParamEvent?.Invoke();
            MenusEvent?.Invoke(menus);
        }
        
    }
}
using System;
using System.Collections.Generic;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuSessionEvent
    {
        public Action NoParamEvent { get; private set; }
        public Action<List<DanmakuSessionMenu>> MenusEvent { get; private set; }
        
        public DanmakuSessionEvent(Action @event = null, Action<List<DanmakuSessionMenu>> menusEvent = null)
        {
            NoParamEvent = @event;
            MenusEvent = menusEvent;
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
        
        public void Unsubscribe(Action @event)
        {
            NoParamEvent -= @event;
        }
        
        public void Unsubscribe(Action<List<DanmakuSessionMenu>> menusEvent)
        {
            MenusEvent -= menusEvent;
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
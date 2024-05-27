using System;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuSessionMenuEvent
    {
        public Action NoParamEvent { get; private set; }
        public Action<DanmakuSessionMenu> MenuEvent { get; private set; }
        
        public void Subscribe(Action noParamEvent, int priority = 0)
        {
            NoParamEvent += noParamEvent;
        }
        
        public void Subscribe(Action<DanmakuSessionMenu> sessionEvent, int priority = 0)
        {
            MenuEvent += sessionEvent;
        }
        
        public void Unsubscribe(Action noParamEvent)
        {
            NoParamEvent -= noParamEvent;
        }
        
        public void Unsubscribe(Action<DanmakuSessionMenu> sessionEvent)
        {
            MenuEvent -= sessionEvent;
        }
        
        public void UnsubscribeAll()
        {
            NoParamEvent = null;
            MenuEvent = null;
        }
        
        public void Invoke(DanmakuSessionMenu session)
        {
            NoParamEvent?.Invoke();
            MenuEvent?.Invoke(session); 
        }
    }
}

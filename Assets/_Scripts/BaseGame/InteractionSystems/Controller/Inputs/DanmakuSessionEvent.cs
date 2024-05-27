using System;
using System.Collections.Generic;
using System.Linq;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuSessionEvent
    {
        private DanmakuSession _session;
        public Action NoParamEvent { get; private set; }
        public Action<DanmakuSession> SessionEvent { get; private set; }
        
        public DanmakuSessionEvent(DanmakuSession session)
        {
            _session = session;
        }
        
        public void Subscribe(Action noParamEvent, int priority = 0)
        {
            NoParamEvent += noParamEvent;
        }
        
        public void Subscribe(Action<DanmakuSession> sessionEvent, int priority = 0)
        {
            SessionEvent += sessionEvent;
        }
        
        public void Unsubscribe(Action noParamEvent)
        {
            NoParamEvent -= noParamEvent;
        }
        
        public void Unsubscribe(Action<DanmakuSession> sessionEvent)
        {
            SessionEvent -= sessionEvent;
        }
        
        public void UnsubscribeAll()
        {
            NoParamEvent = null;
            SessionEvent = null;
        }
        
        public void Invoke()
        {
            NoParamEvent?.Invoke();
            SessionEvent?.Invoke(_session); 
        }
        
    }
}
using System.Collections.Generic;
using Shun_State_Machine;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuSessionController
    {
        private readonly BaseStateMachine _sessionStateMachine;
        
        public DanmakuSessionController()
        {
            _sessionStateMachine = new BaseStateMachine.Builder().Build();
        }
        
        public void Update()
        {
            _sessionStateMachine.Update();
        }
        
        public void FixedUpdate()
        {
            _sessionStateMachine.FixedUpdate();
        }
        
    }
}
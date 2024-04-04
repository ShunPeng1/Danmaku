using System.Collections.Generic;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayerSubsystem
    {
        private int _playerCount;
        
        public List<DanmakuPlayer> Players { get; set; }
        
        
        public DanmakuPlayerSubsystem(int playerCount)
        {
            _playerCount = playerCount;
        }
        
        
        
        

    }
}
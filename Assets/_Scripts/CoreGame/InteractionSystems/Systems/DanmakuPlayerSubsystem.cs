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
        
        void DistributeRoles()
        {
            
            switch (_playerCount)
            {   
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                
            }
            
        }
        
        

    }
}
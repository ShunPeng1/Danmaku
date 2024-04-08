using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using _Scripts.CoreGame.InteractionSystems.Stats;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayerModel
    {
        public IDanmakuRole Role { get; private set;}
        public IDanmakuCharacter DanmakuCharacter { get; private set;}
        public bool IsAlive { get; private set; } = true;
        
        public PlayerStat Life { get; private set; }
        public PlayerStat HandSize { get; private set; }

        public PlayerStat Distance { get; private set; }
        public PlayerStat Range { get; private set; }
        
        public PlayerStat Power { get; private set; }

        public DanmakuPlayerModel()
        {
            
        }

        public void InitializeRole(IDanmakuRole role)
        {
            Role = role;
        }
        
    }
}
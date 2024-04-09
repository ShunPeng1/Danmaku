using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using _Scripts.CoreGame.InteractionSystems.Stats;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayerModel
    {
        public int PlayerId { get; private set; }
        public IDanmakuRole Role { get; private set;}
        public IDanmakuCharacter DanmakuCharacter { get; private set;}
        public bool IsAlive { get; private set; } = true;
        
        public PlayerStat Life { get; private set; }
        public PlayerStat HandSize { get; private set; }

        public PlayerStat Distance { get; private set; }
        public PlayerStat Range { get; private set; }
        
        public PlayerStat Power { get; private set; }
        
        public DanmakuCardHandModel DanmakuCardHandModel { get; private set; }
        

        public DanmakuPlayerModel(int playerId)
        {
            PlayerId = playerId;
            Life = new PlayerStat(1);
            HandSize = new PlayerStat(1);
            Distance = new PlayerStat(1);
            Range = new PlayerStat(1);
            Power = new PlayerStat(1);
        
            DanmakuCardHandModel = new DanmakuCardHandModel();
        }

        public void InitializeRole(IDanmakuRole role)
        {
            Role = role;
        }
                
        public void InitializeStats(PlayerStat life, PlayerStat handSize, PlayerStat distance, PlayerStat range, PlayerStat power)
        {
            Life = life;
            HandSize = handSize;
            Distance = distance;
            Range = range;
            Power = power;
        }

    }
}
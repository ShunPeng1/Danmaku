using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using _Scripts.CoreGame.InteractionSystems.Stats;

namespace _Scripts.CoreGame.InteractionSystems
{
    [DanmakuTargetableClass]
    public class DanmakuPlayerModel : IDanmakuActivator, IDanmakuTargetable
    {
        public int PlayerId { get; private set; }
        public IDanmakuRole Role { get; private set;}
        public IDanmakuCharacter DanmakuCharacter { get; private set;}
        public bool IsAlive { get; private set; } = true;
        
        // Stats
        public PlayerStat Life { get; private set; }
        public PlayerStat HandSize { get; private set; }

        public PlayerStat Distance { get; private set; }
        public PlayerStat Range { get; private set; }
        
        public PlayerStat Power { get; private set; }
        
        // Card Hand and Items Board
        public DanmakuCardHandModel CardHandModel { get; private set; }
        
        // Turn Stats
        public PlayerStat CardDrawIncomeCount { get; private set; }
        public PlayerStat DanmakuCardPlayedCount { get; private set; }
        public PlayerStat SpellCardPlayedCount { get; private set; }

        public DanmakuPlayerModel(int playerId)
        {
            PlayerId = playerId;
            Life = new PlayerStat(1);
            HandSize = new PlayerStat(1);
            Distance = new PlayerStat(1);
            Range = new PlayerStat(1);
            Power = new PlayerStat(1);
        
            CardHandModel = new DanmakuCardHandModel(this);
            
        }

        public void InitializeRole(IDanmakuRole role)
        {
            Role = role;
        }
        
        public void InitializeCharacter(IDanmakuCharacter character)
        {
            DanmakuCharacter = character;
        }
                
        public void InitializeStats(
            PlayerStat life, 
            PlayerStat handSize, 
            PlayerStat distance, 
            PlayerStat range, 
            PlayerStat power,
            PlayerStat cardDrawIncomeCount,
            PlayerStat danmakuCardPlayedCount,
            PlayerStat spellCardPlayedCount)
        {
            Life = life;
            HandSize = handSize;
            Distance = distance;
            Range = range;
            Power = power;
            
            
            CardDrawIncomeCount = cardDrawIncomeCount;
            DanmakuCardPlayedCount = danmakuCardPlayedCount;
            SpellCardPlayedCount = spellCardPlayedCount;
            
        }

    }
}
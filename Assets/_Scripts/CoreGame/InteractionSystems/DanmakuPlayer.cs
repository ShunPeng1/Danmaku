using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using _Scripts.CoreGame.InteractionSystems.Stats;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayer
    {
        public IDanmakuRole Role { get; private set;}
        public ICharacter Character { get; private set;}
        public bool IsAlive { get; private set; } = true;
        
        public PlayerStat Life { get; private set; }
        public PlayerStat HandSize { get; private set; }

        public PlayerStat Distance { get; private set; }
        public PlayerStat Range { get; private set; }
        
        public PlayerStat Power { get; private set; }

        private DanmakuPlayer()
        {
            
        }

        public class DanmakuPlayerBuilder
        {
            private IDanmakuRole _role;

            public DanmakuPlayerBuilder WithDanmakuRole(IDanmakuRole role)
            {
                _role = role;
                return this;
            }

            public DanmakuPlayer Build()
            {
                return new DanmakuPlayer
                {
                    Role = _role
                };
            }
        }
        
    }
}
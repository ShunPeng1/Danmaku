using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayer
    {
        public IDanmakuRole Role { get; private set;}
        public ICharacter Character { get; private set;}
        public bool IsAlive { get; private set; } = true;
        
        public DanmakuPlayer(IDanmakuRole role, ICharacter character)
        {
            Role = role;
            Character = character;
        }
    }
}
using System.Collections.Generic;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using _Scripts.CoreGame.InteractionSystems.Setups;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayerGroupModel
    {
        public readonly int PlayerCount;
        public List<DanmakuPlayerModel> Players { get; private set; }
        
        public DanmakuPlayerModel CurrentPlayer { get; private set; }
        public PlayStepEnum CurrentPlayStepEnum { get; private set; }
        
        public DanmakuPlayerGroupModel(List<DanmakuPlayerModel> currentPlayer)
        {
            Players = new List<DanmakuPlayerModel>();
            PlayerCount = currentPlayer.Count;
        }
        
        
        
       
        
        

    }
}
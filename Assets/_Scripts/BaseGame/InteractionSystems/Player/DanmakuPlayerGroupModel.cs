using System.Collections.Generic;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using _Scripts.CoreGame.InteractionSystems.Setups;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayerGroupModel
    {
        private ISetupPlayerView _setupPlayerView;
        private readonly int _playerCount;

        public List<DanmakuPlayer> Players { get; private set; }
        
        
        public DanmakuPlayerGroupModel(ISetupPlayerView setupPlayerView, int playerCount)
        {
            _setupPlayerView = setupPlayerView;
            _playerCount = playerCount;
            Players = new List<DanmakuPlayer>();
        }
        
        public void SetupPlayers(RoleSetConfig roleSetConfig)
        {
            List<DanmakuPlayer.DanmakuPlayerBuilder> playerBuilders = new List<DanmakuPlayer.DanmakuPlayerBuilder>();
            
            for (int i = 0; i < _playerCount; i++)
            {
                var playerBuilder = new DanmakuPlayer.DanmakuPlayerBuilder();
                playerBuilders.Add(playerBuilder);   
            }
            
            DanmakuRoleSetupDirector roleSetupDirector = new DanmakuRoleSetupDirector(this, playerBuilders, null);
            
            var playerToRole = roleSetupDirector.SetupRoles();
            _setupPlayerView.SetupPlayerRoleView(playerToRole);
            
            
            
            
        }
        
        

    }
}
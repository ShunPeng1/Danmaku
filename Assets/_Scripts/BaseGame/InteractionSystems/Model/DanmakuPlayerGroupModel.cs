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

        public List<DanmakuPlayerModel> Players { get; private set; }
        
        
        public DanmakuPlayerGroupModel(ISetupPlayerView setupPlayerView, int playerCount)
        {
            _setupPlayerView = setupPlayerView;
            _playerCount = playerCount;
            Players = new List<DanmakuPlayerModel>();
        }
        
        public void SetupPlayers(RoleSetConfig roleSetConfig)
        {
            List<DanmakuPlayerModel.DanmakuPlayerBuilder> playerBuilders = new List<DanmakuPlayerModel.DanmakuPlayerBuilder>();
            
            for (int i = 0; i < _playerCount; i++)
            {
                var playerBuilder = new DanmakuPlayerModel.DanmakuPlayerBuilder();
                playerBuilders.Add(playerBuilder);   
            }
            
            DanmakuRoleSetupDirector roleSetupDirector = new DanmakuRoleSetupDirector(this, playerBuilders, null);
            
            var playerToRole = roleSetupDirector.SetupRoles();
            _setupPlayerView.SetupPlayerRoleView(playerToRole);
            
            
            
            
        }
        
        

    }
}
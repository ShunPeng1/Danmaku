using System.Collections.Generic;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Setups;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuStepController
    {
	    ISetupPlayerView _setupPlayerView;
        
        DanmakuPlayerGroupModel _playerGroupModel;
            

        private DanmakuStepController(DanmakuPlayerGroupModel playerGroupModel, ISetupPlayerView setupPlayerView)
        {
            _playerGroupModel = playerGroupModel;
            _setupPlayerView = setupPlayerView;
        }
        
        public class Builder
        {
            private DanmakuPlayerGroupModel _playerGroupModel;
            private ISetupPlayerView _setupPlayerView;
            
            public Builder WithPlayerGroupModel(DanmakuPlayerGroupModel playerGroupModel)
            {
                _playerGroupModel = playerGroupModel;
                return this;
            }
            
            public DanmakuStepController Build(ISetupPlayerView setupPlayerView)
            {
                return new DanmakuStepController(_playerGroupModel, setupPlayerView);
            }
            
            public void WithPlayerGroup(int playerCount, RoleSetConfig roleSetConfig)
            {
                List<DanmakuPlayerModel> players = new List<DanmakuPlayerModel>();
                
                for (int i = 0; i < playerCount; i++)
                {
                    var player = new DanmakuPlayerModel();
                    players.Add(player);   
                }
                
                _playerGroupModel = new DanmakuPlayerGroupModel(players);
                
                DanmakuRoleSetupDirector roleSetupDirector = new DanmakuRoleSetupDirector(_playerGroupModel, players, roleSetConfig);
                
                var playerToRole = roleSetupDirector.SetupRoles();
                _setupPlayerView.SetupPlayerRoleView(playerToRole);
                
                
            }
            
        }
    }
}
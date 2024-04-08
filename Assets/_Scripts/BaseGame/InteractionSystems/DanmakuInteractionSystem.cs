using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuInteractionSystem : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private ISetupPlayerView _setupPlayerView;
        
        [Header("Configurations")]
        [SerializeField] private int _playerCount;
        [SerializeField] private RoleSetConfig _roleSetConfig;
        
        private void Start()
        {
            DanmakuPlayerGroupModel danmakuPlayerGroupModel = new DanmakuPlayerGroupModel(_setupPlayerView, 1);
            danmakuPlayerGroupModel.SetupPlayers(_roleSetConfig);
        }
        
    }
}
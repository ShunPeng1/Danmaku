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
            DanmakuPlayerController danmakuPlayerController = new DanmakuPlayerController(_setupPlayerView, 1);
            danmakuPlayerController.SetupPlayers(_roleSetConfig);
        }
        
    }
}
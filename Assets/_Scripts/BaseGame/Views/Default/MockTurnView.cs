using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockTurnView : DanmakuTurnBaseView
    {
        public override void SetPlayerCurrentTurn(DanmakuPlayerModel playerModel)
        {
            Debug.Log("Current Player Turn "+ playerModel.PlayerId);
        }
    }
}
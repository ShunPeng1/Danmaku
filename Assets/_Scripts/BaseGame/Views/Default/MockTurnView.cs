using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockTurnView : DanmakuTurnBaseView
    {
        public override void SetPlayerCurrentTurn(DanmakuPlayerModel playerModel)
        {
            Debug.Log("Current Player Turn "+ playerModel.PlayerId);
        }

        public override void EndPlayerStep(DanmakuPlayerModel danmakuPlayerModel, PlayStepEnum playStepEnum)
        {
            Debug.Log("End Player "+ danmakuPlayerModel.PlayerId + " Step "+ playStepEnum.ToString());
        }

        public override void StartPlayerStep(DanmakuPlayerModel danmakuPlayerModel, PlayStepEnum playStepEnum)
        {
            Debug.Log("Start Player "+ danmakuPlayerModel.PlayerId + " Step "+ playStepEnum.ToString());
        }
    }
}
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuTurnBaseView : MonoBehaviour
    {
        
        public abstract void SetPlayerCurrentTurn(DanmakuPlayerModel playerModel);

        public abstract void EndPlayerStep(DanmakuPlayerModel danmakuPlayerModel, PlayStepEnum playStepEnum);
        public abstract void StartPlayerStep(DanmakuPlayerModel danmakuPlayerModel, PlayStepEnum playStepEnum);
    }
}
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuTurnBaseView : MonoBehaviour
    {
        
        public abstract void SetPlayerCurrentTurn(DanmakuPlayerModel playerModel);
        
    }
}
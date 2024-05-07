using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuCharacterBaseView : MonoBehaviour
    {
        public abstract void SetupCharacter(DanmakuCharacterCardModel chosenCard);
    }
}
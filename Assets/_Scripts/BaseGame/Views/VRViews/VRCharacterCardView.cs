using _Scripts.CoreGame.InteractionSystems;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCharacterCardView : DanmakuCharacterCardBaseView
    {
        private DanmakuCharacterCardModel _characterCardModel;
        public override void SetCardModel(DanmakuCharacterCardModel characterCard)
        {
            _characterCardModel = characterCard;
        }
    }
}
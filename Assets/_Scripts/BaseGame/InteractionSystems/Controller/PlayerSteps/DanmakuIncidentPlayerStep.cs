﻿using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuIncidentPlayerStep : IDanmakuPlayerStep
    {
        public bool CanEndStep(DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            return true;
        }

        public void Execute(DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            
        }
    }
}
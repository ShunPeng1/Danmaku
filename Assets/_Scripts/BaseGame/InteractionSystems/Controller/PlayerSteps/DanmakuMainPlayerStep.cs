using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuMainPlayerStep : IDanmakuPlayerStep
    {
        public bool CanEndStep(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            return true;
        }

        public void Execute(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView, Action finishExecuteCallback = null)
        {
            Debug.Log(playerModel.PlayerId + " Main Step Executed!");
        
            var session = new DanmakuSession.Builder()
                .WithPlayingPlayerModel(new List<IDanmakuActivator>(){playerModel})
                .WithPlayerSessionKindEnum(EndSessionKindEnum.NonePlayed)
                .WithCountDownTime(1000f)
                .Build(interactionController);

            session.OnSessionStartEvent.Subscribe(()=>playerView.SessionHandler.SetCurrentSession(session));
            session.SubscribeOnSessionEnd(finishExecuteCallback, true);
            
            session.StartSession();
            
        }
    }
}
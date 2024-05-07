using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views;
using Shun_Utilities;
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
            
            var menus = new ObservableList<DanmakuSessionMenu>();
            
            var session = new DanmakuSession.Builder()
                .WithPlayingPlayerModel(new List<IDanmakuActivator>(){playerModel})
                .WithPlayingSessionMenus(menus)
                .WithPlayerSessionKindEnum(EndSessionKindEnum.NonePlayed)
                .WithCountDownTime(1000f)
                .Build(interactionController);
            var choices = new List<DanmakuSessionChoice>();
            var menu = new DanmakuSessionMenu(session, playerModel, choices);
            menus.Add(menu);

            choices.Add(new DanmakuSessionChoice(
                menu, 
                new List<IDanmakuTargetable>(playerModel.CardHandModel.Cards.Items.ToList()),
                ChoiceActionEnum.Confirm
                ));

            session.OnSessionStartEvent.Subscribe(()=>playerView.SessionHandler.SetCurrentSession(session));
            session.SubscribeOnSessionEnd(finishExecuteCallback, true);
            
            session.StartSession();
            
        }
    }
}
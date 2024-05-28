using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuDiscardPlayerStep : IDanmakuPlayerStep
    {
        private DanmakuInteractionController _interactionController;
        Action _finishExecuteCallback;

        private DanmakuPlayerModel _playerModel;
        private DanmakuPlayerBaseView _playerView;
        
        private DanmakuSession _session;
        private DanmakuSessionMenu _discardMenu;


        
        public bool CanEndStep(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            return playerModel.DeckCardHandModel.Cards.Count <= playerModel.HandSize.Get();
        }

        public void Execute(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel,
            DanmakuPlayerBaseView playerView, Action finishExecuteCallback = null)
        {
            _interactionController = interactionController;
            _finishExecuteCallback = finishExecuteCallback;
            _playerModel = playerModel;
            _playerView = playerView;


            if (_playerModel.DeckCardHandModel.Cards.Count - _playerModel.HandSize.Get() > 0)
            {
                CreateDiscardStepSession();
            }
            else
            {
                _finishExecuteCallback?.Invoke();
            }
        }
        
        private void CreateDiscardStepSession()
        {
           
            var menus = new List<DanmakuSessionMenu>();
            
            _session = new DanmakuSession.Builder()
                .WithPlayingPlayerModel(new List<IDanmakuActivator>(){_playerModel})
                .WithPlayingSessionMenus(menus)
                .WithPlayerSessionKindEnum(MenuEndCheckEnum.AnyPlayed)
                .WithCountDownTime(1000f)
                .Build(_interactionController);
            var choices = new List<DanmakuSessionChoice>();
            var menu = new DanmakuSessionMenu(_session, _playerModel, choices,ChoiceActionEnum.Confirm, ChoiceEndCheckEnum.AllPlayed);
            
            _discardMenu = menu;
            
            menus.Add(menu);

            for (int i = 0; i < _playerModel.DeckCardHandModel.Cards.Count - _playerModel.HandSize.Get(); i++)
            {
                var discardCardChoice = new DanmakuSessionChoice(
                    menu,
                    _playerModel.DeckCardHandModel.GetCards<IDanmakuTargetable>(),
                    IsCardPlayable);
            
                choices.Add(discardCardChoice);
            }
            
            // session events
            
            _session.OnSessionStartEvent.Subscribe(()=>_playerView.SessionHandler.SetCurrentSession(_session));
            _session.SubscribeOnSessionEnd(()=>_playerView.SessionHandler.UnsetCurrentSession(), true);
            
            _session.SubscribeOnSessionEnd(FinishDiscardStepSession, true);
            
            
            _session.StartSession();
            
        }

        private void FinishDiscardStepSession(DanmakuSession obj)
        {
            foreach (var choice in _discardMenu.SessionChoices)
            {
                if (choice.SelectedTarget != null)
                {
                    if (choice.SelectedTarget is DanmakuMainDeckCardModel mainDeckCardModel)
                    {
                        var discardPlayerModel = _discardMenu.Activator as DanmakuPlayerModel;
                        _interactionController.BoardController.DiscardCard(discardPlayerModel,mainDeckCardModel);
                        
                    }
                }
                else
                {
                    Debug.LogError("No card selected");
                }
            }
            
            _finishExecuteCallback?.Invoke();
        }

        private bool IsCardPlayable(IDanmakuTargetable target)
        {
            return target is DanmakuMainDeckCardModel mainDeckCard && mainDeckCard.IsPlayable();
        }

    }
}
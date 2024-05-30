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
        private DanmakuInteractionController _interactionController;
        Action _finishExecuteCallback;

        private DanmakuPlayerModel _playerModel;
        private DanmakuPlayerBaseView _playerView;
        
        private DanmakuSession _session;

        private DanmakuSessionChoice _playCardChoice;
        private DanmakuSessionChoice _cardRuleChoice;
        private DanmakuSessionMenu _cardChoiceMenu;

        public bool CanEndStep(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            return true;
        }

        public void Execute(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel,
            DanmakuPlayerBaseView playerView, Action finishExecuteCallback = null)
        {
            _interactionController = interactionController;
            _finishExecuteCallback = finishExecuteCallback;
            _playerModel = playerModel;
            _playerView = playerView;
            
            _session = CreateMainStepSession();
            _interactionController.CombatController.EnqueueCombatSession(_session, true);
            
        }
        
        private DanmakuSession CreateMainStepSession()
        {

            var menus = new List<DanmakuSessionMenu>();
            
            var session = new DanmakuSession.Builder()
                .WithPlayingPlayerModel(new List<IDanmakuActivator>(){_playerModel})
                .WithPlayingSessionMenus(menus)
                .WithPlayerSessionKindEnum(MenuEndCheckEnum.AnyPlayed)
                .WithCountDownTime(1000f)
                .Build(_interactionController);
            var choices = new List<DanmakuSessionChoice>();
            var menu = new DanmakuSessionMenu(session, _playerModel, choices,ChoiceActionEnum.Confirm, ChoiceEndCheckEnum.NonePlayed);
            
            menus.Add(menu);

            _playCardChoice = new DanmakuSessionChoice(
                menu,
                _playerModel.DeckCardHandModel.GetCards<IDanmakuTargetable>(),
                IsCardPlayable);
            
            choices.Add(_playCardChoice);

            // session events
            
            session.OnSessionStartEvent.Subscribe(()=>_playerView.SessionHandler.SetCurrentSession(session));
            session.SubscribeOnSessionEnd(()=>_playerView.SessionHandler.UnsetCurrentSession(), true);
            session.SubscribeOnSessionEnd(FinishMainStepSession, true);
            
            // Add Card will make a play card session menu for card choices 
            _playCardChoice.OnTargetSelected += AddCardChoiceMenu;
            _playCardChoice.OnTargetDeselected += RemoveCardChoiceMenu;

            return session;
        }
        
        private bool IsCardPlayable(IDanmakuTargetable target)
        {
            if (target is DanmakuMainDeckCardModel mainDeckCard)
            {
                var isPlayable = mainDeckCard.IsPlayable();
                return isPlayable;
            }
            return false;
        }

        private void AddCardChoiceMenu(IDanmakuTargetable obj)
        {
                DanmakuMainDeckCardModel card = obj as DanmakuMainDeckCardModel;
            
            if (card == null || card.IsPlayable() == false)
            {
                return;
            }

            var ruleTargetablesQueryResults = card.GetPlayableRules();

            List<DanmakuSessionChoice> choices = new List<DanmakuSessionChoice>();

            if (ruleTargetablesQueryResults.Count > 1)
            {
                List<IDanmakuTargetable> ruleTargetables = new List<IDanmakuTargetable>(ruleTargetablesQueryResults.Select(ruleTargetables => ruleTargetables.CardRule).ToList());
                _cardRuleChoice = new DanmakuSessionChoice(
                    _cardChoiceMenu,
                    ruleTargetables,
                    (target) => ruleTargetables.Contains(target)
                );
                
                choices.Add(_cardRuleChoice);
            }
            else if (ruleTargetablesQueryResults.Count == 1)
            {
                _cardRuleChoice = new DanmakuSessionChoice(_cardChoiceMenu, ruleTargetablesQueryResults[0].CardRule);
                
                // Not Add to menu
                //choices.Add(_cardRuleChoice);
            }
            else if (ruleTargetablesQueryResults.Count == 0)
            {
                Debug.Log("No rules found for card: " + card);
                _cardChoiceMenu = null;
                return;
            }
            
            
            _cardChoiceMenu = new DanmakuSessionMenu(_session, _playCardChoice.Menu.Activator, choices, ChoiceActionEnum.Confirm);

            foreach (var ruleTargetables in ruleTargetablesQueryResults)
            {
                foreach (var targetableQueryResult in ruleTargetables.Targetables)
                {
                    choices.Add(new DanmakuSessionChoice(
                        _cardChoiceMenu,
                        targetableQueryResult.Targetables,
                        (target) => targetableQueryResult.Targetables.Contains(target)
                    ));
                }
            }
            
            _session.AddSessionMenu(_cardChoiceMenu);
            
        }
        
        private void RemoveCardChoiceMenu(IDanmakuTargetable obj)
        {
            if (_cardChoiceMenu == null)
            {
                return;
            }
            _session.RemoveSessionMenu(_cardChoiceMenu);
        }

        private void FinishMainStepSession(DanmakuSession session)
        {
            if (_playCardChoice.SelectedTarget != null)
            {
                
                DanmakuMainDeckCardModel card = _playCardChoice.SelectedTarget as DanmakuMainDeckCardModel;
                
                IDanmakuCardRule cardRule = _cardRuleChoice.SelectedTarget as IDanmakuCardRule;
                
                IDanmakuActivator activator = _cardChoiceMenu.Activator;

                var cardExecutionParameter = new DanmakuCardExecutionParameter(card, cardRule, activator);

                foreach (var choice in _cardChoiceMenu.SessionChoices)
                {
                    if (choice == _playCardChoice ) continue;
                    
                    if (choice.SelectedTarget == null)
                    {
                        Debug.LogError("Choice has no selected target");
                    }
                    
                    cardExecutionParameter.AddTarget(choice.SelectedTarget);
                }
                
                _interactionController.CombatController.AddCardExecution(cardExecutionParameter);
                
                var nextSession = CreateMainStepSession();
                _session.OnFinallyEndSessionEvent.Subscribe((danmakuSession) => // Do not remove parameter
                {
                    _interactionController.CombatController.EnqueueCombatSession(nextSession, true);
                });
                
                _session = nextSession;
            }
            else
            {
                _finishExecuteCallback?.Invoke();
            }
            
        }
        
    }
}
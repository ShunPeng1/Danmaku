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
        private DanmakuSession _session;
        private DanmakuInteractionController _interactionController;
        private DanmakuSessionChoice _playCardChoice;
        private DanmakuSessionMenu _cardChoiceMenu;
        Action _finishExecuteCallback;

        public bool CanEndStep(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            return true;
        }

        public void Execute(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView, Action finishExecuteCallback = null)
        {
            _interactionController = interactionController;
            _finishExecuteCallback = finishExecuteCallback;
            
            var menus = new List<DanmakuSessionMenu>();
            
            _session = new DanmakuSession.Builder()
                .WithPlayingPlayerModel(new List<IDanmakuActivator>(){playerModel})
                .WithPlayingSessionMenus(menus)
                .WithPlayerSessionKindEnum(EndSessionKindEnum.NonePlayed)
                .WithCountDownTime(1000f)
                .Build(interactionController);
            var choices = new List<DanmakuSessionChoice>();
            var menu = new DanmakuSessionMenu(_session, playerModel, choices,ChoiceActionEnum.Confirm);
            
            menus.Add(menu);

            _playCardChoice = new DanmakuSessionChoice(
                menu,
                new List<IDanmakuTargetable>(playerModel.CardHandModel.Cards.Items.ToList()),
                IsCardPlayable);
            choices.Add(_playCardChoice);

            // session events
            _session.OnSessionStartEvent.Subscribe(()=>playerView.SessionHandler.SetCurrentSession(_session));
            
            _session.SubscribeOnSessionEnd(FinishMainStepSession, true);
            
            // Add Card will make a play card session menu for card choices 
            _playCardChoice.OnTargetSelected += AddCardChoiceMenu;
            _playCardChoice.OnTargetDeselected += RemoveCardChoiceMenu;
            
            _session.StartSession();
            
        }
        
        private bool IsCardPlayable(IDanmakuTargetable target)
        {
            return target is DanmakuMainDeckCardModel mainDeckCard && mainDeckCard.IsPlayable();
        }

        private void AddCardChoiceMenu(IDanmakuTargetable obj)
        {
            DanmakuMainDeckCardModel card = obj as DanmakuMainDeckCardModel;
            List<DanmakuSessionChoice> choices = new List<DanmakuSessionChoice>();
            
            if (card == null)
            {
                return;
            }
            
            var ruleTargetablesQueryResults = card.GetPlayableRules();

            /*
            if (ruleTargetablesQueryResults.Count > 1)
            {
                List<IDanmakuTargetable> ruleTargetables = new List<IDanmakuTargetable>(ruleTargetablesQueryResults.Select(ruleTargetables => ruleTargetables.CardRule).ToList());
                choices.Add(new DanmakuSessionChoice(
                    _cardChoiceMenu,
                    ruleTargetables,
                    ChoiceActionEnum.Confirm,
                    (target) => ruleTargetables.Contains(target)
                ));
            }
            */
            
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
            
            _cardChoiceMenu = new DanmakuSessionMenu(_session, _playCardChoice.Menu.Activator, choices, ChoiceActionEnum.Confirm);
            
            _session.AddSessionMenu(_cardChoiceMenu);
            
        }
        
        private void RemoveCardChoiceMenu(IDanmakuTargetable obj)
        {
            _session.RemoveSessionMenu(_cardChoiceMenu);
            
        }

        private void FinishMainStepSession(DanmakuSession session)
        {
            if (_playCardChoice.SelectedTarget != null)
            {
                DanmakuMainDeckCardModel card = _playCardChoice.SelectedTarget as DanmakuMainDeckCardModel;
                Debug.Log("Selected Card: " + _playCardChoice.SelectedTarget);        
                
                //_interactionController.CombatController.AddCardExecution(new DanmakuCardExecutionParameter(
                //    card,
                //    card.Rule
                //    ));
            }
            else
            {
                _finishExecuteCallback?.Invoke();
            }
        }
        
    }
}
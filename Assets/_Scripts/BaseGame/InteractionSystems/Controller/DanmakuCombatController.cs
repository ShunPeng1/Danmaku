using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Stats;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuCombatController
    {
        private readonly DanmakuInteractionController _danmakuInteractionController;
        private DanmakuBoardModel BoardModel => _danmakuInteractionController.BoardModel;
        
        private readonly Stack<DanmakuCardExecutionParameter> _playedCards = new (); 
        
        private List<DanmakuSession> _combatSessionQueue = new ();
        private DanmakuSession _currentCombatSession;
        
        
        
        public DanmakuCombatController(DanmakuInteractionController danmakuInteractionController)
        {
            _danmakuInteractionController = danmakuInteractionController;
        }
        

        public void AddCardExecution(DanmakuCardExecutionParameter danmakuCardExecutionParameter)
        {
            // Remove from hand if is player
            if (danmakuCardExecutionParameter.Activator is DanmakuPlayerModel danmakuPlayerModel)
            {
                danmakuPlayerModel.DeckCardHandModel.RemoveCard(danmakuCardExecutionParameter.Card);
            }
           
            
            // Add to played cards
            _playedCards.Push(danmakuCardExecutionParameter);
            
            danmakuCardExecutionParameter.Card.RevealCard();

            if (_combatSessionQueue.Count == 0 && _currentCombatSession == null)
            {
                ResolveCombat();
            }
        }
        
        
        
        public void ResolveCombat()
        {
            while (_playedCards.TryPop(out var cardExecution))
            {
                if (cardExecution.CardRule.CanExecuteRule(cardExecution.Activator, cardExecution.Targetables) == false)
                {
                    Debug.Log("Cannot execute rule. Skipping card." + cardExecution.Card);
                    continue;
                }
                
                cardExecution.Card.ExecuteCard(cardExecution.CardRule, cardExecution.Activator, cardExecution.Targetables);
                
                BoardModel.DiscardDeckModel.AddCard(cardExecution.Card);
            }
        }
        
        
        public void EnqueueCombatSession(DanmakuSession danmakuSession, bool willStartSession)
        {
            _combatSessionQueue.Add(danmakuSession);
            
            if (willStartSession)
            {
                StartCombatSession();
            }
        }

        public void InsertCombatSession(DanmakuSession danmakuSession, int index, bool willStartSession)
        {
            _combatSessionQueue.Insert(index, danmakuSession);
            
            if (willStartSession)
            {
                StartCombatSession();
            }
        }
        
        public void StartCombatSession()
        {
            if (_currentCombatSession is { IsEnded: false } || _combatSessionQueue.Count == 0)
            {
                return;
            }
            
            _currentCombatSession = _combatSessionQueue[0];
            _combatSessionQueue.RemoveAt(0);
            
            _currentCombatSession.OnFinallyEndSessionEvent.Subscribe(EndCombatSession);
            
            _currentCombatSession.StartSession();
            
        }

        private void EndCombatSession(DanmakuSession danmakuSession)
        {
            danmakuSession.OnFinallyEndSessionEvent.Unsubscribe(EndCombatSession);
            
            if (_currentCombatSession == danmakuSession)
            {
                _currentCombatSession = null;
            }
            
            if (_combatSessionQueue.Count > 0)
            {
                StartCombatSession();
            }
            else
            {
                ResolveCombat();
            }
            
        }        

    }
}
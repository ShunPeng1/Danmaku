using System;
using System.Collections.Generic;
using BNG;
using Shun_State_Machine;
using Shun_Utilities;

namespace _Scripts.CoreGame.InteractionSystems
{
    public enum PlayerSessionKindEnum
    {
        AllPlayed,
        AnyPlayed,
        
    }
    
    public class DanmakuSession
    {
        public DanmakuInteractionController DanmakuInteractionController { get; private set; }
        public PlayerSessionKindEnum PlayerSessionKindEnum { get; private set; }
        public List<DanmakuPlayerModel> PlayingPlayerModel { get; private set; }
        
        public Countdown Countdown { get; private set; }
        
        public Action ForceEndFunction { get; private set; }
        public Func<Grabbable, bool> CardFilter { get; private set; }

        protected DanmakuSession(DanmakuInteractionController danmakuInteractionController,
            PlayerSessionKindEnum playerSessionKindEnum, List<DanmakuPlayerModel> playingPlayerModel,
            Countdown countdown, Action forceEndFunction, Func<Grabbable, bool> cardFilter)
        {
            DanmakuInteractionController = danmakuInteractionController;
            PlayerSessionKindEnum = playerSessionKindEnum;
            PlayingPlayerModel = playingPlayerModel;
            Countdown = countdown;
            ForceEndFunction = forceEndFunction;
        }
        
        public class Builder
        {
            private PlayerSessionKindEnum _playerSessionKindEnum = PlayerSessionKindEnum.AnyPlayed; // Default is AnyPlayed
            private List<DanmakuPlayerModel> _playingPlayerModel = new(); // Default is empty
            private float _countDownTime = float.PositiveInfinity; // Default is infinite
            private Action _forceEndFunction = delegate {  }; // Do nothing
            private bool _isLoop = false;
            private Action _setPlayingCardFunction = delegate {  }; // Do nothing

            private Func<Grabbable, bool> _cardFilter = delegate { return true;}; // Allow all cards by default
            
            private Countdown _countdown;
            
            
            public Builder WithPlayerSessionKindEnum(PlayerSessionKindEnum playerSessionKindEnum)
            {
                _playerSessionKindEnum = playerSessionKindEnum;
                return this;
            }

            public Builder WithPlayingPlayerModel(List<DanmakuPlayerModel> playingPlayerModel)
            {
                _playingPlayerModel = playingPlayerModel;
                return this;
            }

            public Builder WithCountDownTime(float countDownTime)
            {
                _countDownTime = countDownTime;
                return this;
            }
            
            public Builder WithIsLoop(bool isLoop)
            {
                _isLoop = isLoop;
                return this;
            }
            
            public Builder WithForceEndFunction(Action forceEnd)
            {
                _forceEndFunction = forceEnd;
                return this;
            }
            
            public Builder WithCardFilter(Func<Grabbable, bool> cardFilter)
            {
                _cardFilter = cardFilter;
                return this;
            }
            
            public DanmakuSession Build(DanmakuInteractionController danmakuInteractionController)
            {
                _countdown = new Countdown(_isLoop, _countDownTime);
                
                return new DanmakuSession(danmakuInteractionController, _playerSessionKindEnum, _playingPlayerModel, _countdown, _forceEndFunction, _cardFilter);
            }
        }


    }
}
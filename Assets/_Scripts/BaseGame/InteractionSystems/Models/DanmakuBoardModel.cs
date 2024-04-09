using System.Collections.Generic;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuBoardModel
    {
        public DanmakuCardDeckModel MainDeckModel { get; protected set;}
        public DanmakuCardDeckModel DiscardDeckModel { get; protected set;}
        public DanmakuCardDeckModel IncidentDeckModel { get; protected set;}
        public DanmakuCardDeckModel DiscardIncidentDeckModel { get; protected set;}
        
        
        public DanmakuBoardModel()
        {
            MainDeckModel = new DanmakuCardDeckModel();
            
            DiscardDeckModel = new DanmakuCardDeckModel();
            IncidentDeckModel = new DanmakuCardDeckModel();
        }
    }
}
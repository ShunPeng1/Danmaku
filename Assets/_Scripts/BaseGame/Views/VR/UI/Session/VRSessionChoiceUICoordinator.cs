using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRSessionChoiceUICoordinator : MonoBehaviour
    {
        [SerializeField] private List<ChoiceHandlerRepository> _choiceHandlerRepositories;
        [SerializeField] private List<PlaceKindData> _placeTypeData;

        [ShowInInspector, ReadOnly] private Dictionary<DanmakuSessionChoice, ChoiceHandlerData> _choiceHandlers = new();

        public enum PlaceKindEnum
        {
            World,
            Canvas
        }

        private class ChoiceHandlerData
        {
            public DanmakuSessionChoiceBaseHandler Handler;
            public PlaceKindData PlaceKindData;
            
        }
        
        [System.Serializable]
        public class PlaceKindData
        {
            public PlaceKindEnum PlaceKindEnum;
            public Transform StartingTransform;
            public Vector3 Offset;
            [ReadOnly] public int HandlerCount;
        }
        
        [System.Serializable]
        public class ChoiceHandlerRepository
        {
            public DanmakuSessionChoiceBaseHandler HandlerPrefab;
            [DanmakuTargetableProperty]
            public string TargetableType;
            public PlaceKindEnum PlaceKindEnum = PlaceKindEnum.World;
        }
        
        public void CreateView(DanmakuSessionChoice sessionChoice)
        {
            var choiceHandlerRepository = _choiceHandlerRepositories.FirstOrDefault(handler => handler.TargetableType == sessionChoice.TargetType.Name);
            
            if (choiceHandlerRepository == null)
            {
                Debug.LogError($"No handler found for {sessionChoice.TargetType.Name}");
                return;
            }

            switch (choiceHandlerRepository.PlaceKindEnum)
            {
                case PlaceKindEnum.World:
                    var worldHandler = CreateWorldHandler(sessionChoice,choiceHandlerRepository.HandlerPrefab);
                    worldHandler.SetSessionChoice(sessionChoice);

                    break;
                case PlaceKindEnum.Canvas:
                    var canvasHandler = CreateCanvasHandler(sessionChoice,choiceHandlerRepository.HandlerPrefab);
                    canvasHandler.SetSessionChoice(sessionChoice);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            
        }


        private DanmakuSessionChoiceBaseHandler CreateWorldHandler(DanmakuSessionChoice sessionChoice, DanmakuSessionChoiceBaseHandler handlerPrefab)
        {
            PlaceKindData worldPlaceKindData = _placeTypeData.FirstOrDefault(data => data.PlaceKindEnum == PlaceKindEnum.World);
            
            if (worldPlaceKindData == null)
            {
                Debug.LogError("No world place type data found");
                return null;
            }
            
            var cardHandlerView = Instantiate(handlerPrefab, worldPlaceKindData.StartingTransform);

            var cardTransform = cardHandlerView.transform;
            cardTransform.position = worldPlaceKindData.StartingTransform.position;
            cardTransform.localPosition += worldPlaceKindData.Offset * worldPlaceKindData.HandlerCount;
            cardTransform.rotation = worldPlaceKindData.StartingTransform.rotation;
            
            _choiceHandlers.Add(sessionChoice,new ChoiceHandlerData
            {
                Handler = cardHandlerView,
                PlaceKindData = worldPlaceKindData
            });
            return cardHandlerView;
        }

        private DanmakuSessionChoiceBaseHandler CreateCanvasHandler(DanmakuSessionChoice sessionChoice, DanmakuSessionChoiceBaseHandler handlerPrefab)
        {
            PlaceKindData canvasPlaceKindData = _placeTypeData.FirstOrDefault(data => data.PlaceKindEnum == PlaceKindEnum.Canvas);
            
            if (canvasPlaceKindData == null)
            {
                Debug.LogError("No canvas place type data found");
                return null;
            }
            
            var cardHandlerView = Instantiate(handlerPrefab, canvasPlaceKindData.StartingTransform);

            var cardTransform = cardHandlerView.transform;
            cardTransform.position = canvasPlaceKindData.StartingTransform.position;
            cardTransform.localPosition += canvasPlaceKindData.Offset * canvasPlaceKindData.HandlerCount;
            cardTransform.rotation = canvasPlaceKindData.StartingTransform.rotation;
            
            _choiceHandlers.Add(sessionChoice,new ChoiceHandlerData
            {
                Handler = cardHandlerView,
                PlaceKindData = canvasPlaceKindData
            });
            return cardHandlerView;
            
        }        
        public void RemoveView(DanmakuSessionChoice sessionChoice)
        {
            var isExist = _choiceHandlers.TryGetValue(sessionChoice, out var handler);
            if (!isExist) return;
            
            _choiceHandlers.Remove(sessionChoice);
            Destroy(handler.Handler.gameObject);
            handler.PlaceKindData.HandlerCount--;

        }

    
    }
}
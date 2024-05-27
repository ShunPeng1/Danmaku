using System.Collections.Generic;
using _Scripts.BaseGame.Views.Basics;
using _Scripts.CoreGame.InteractionSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuSessionMenuBaseHandler : MonoBehaviour
    {
        
        protected readonly List<DanmakuSessionMenu> SessionMenus = new();
        
        public abstract void AddSessionMenu(DanmakuSessionMenu sessionMenu);
        public abstract void RemoveSessionMenu(DanmakuSessionMenu sessionMenu);
        
    }
}
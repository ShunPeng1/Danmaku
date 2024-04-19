using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuRoleBaseView : MonoBehaviour
    {
        public abstract void RevealRole(IDanmakuRole role);
    }
}
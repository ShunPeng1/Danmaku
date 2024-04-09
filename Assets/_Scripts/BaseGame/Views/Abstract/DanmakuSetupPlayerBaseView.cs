using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuSetupPlayerBaseView : MonoBehaviour
    {
        public abstract void SetupPlayerRoleView(Dictionary<DanmakuPlayerModel, IDanmakuRole> playerToRole);
    }
}
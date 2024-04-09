using System.Collections;
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuSetupPlayerBaseView : MonoBehaviour
    {
        public abstract IEnumerator SetupPlayerRoleView(Dictionary<DanmakuPlayerModel, IDanmakuRole> playerToRole);
        public abstract IEnumerator SetupCardDeckRoleView(DanmakuCardDeckModel cardDeckModel);
        public abstract IEnumerator SetupPlayerHandView(DanmakuPlayerModel playerModel, DanmakuCardHandModel cardHandModel);
        
        
    }
}
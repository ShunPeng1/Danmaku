using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.BaseGame.ScriptableData
{
    [CreateAssetMenu(fileName = "RoleScriptableData", menuName = "ScriptableData/RoleScriptableData")]
    public class RoleScriptableData : ScriptableObject
    {
        [DanmakuRoleProperty]
        public string RoleName;
        public int MinPlayerCountPrerequisite;
        public DanmakuRoleEnum InitialRoleEnum;

        public void OnValidate()
        {
            Debug.Log(RoleName);
        }
    }
}
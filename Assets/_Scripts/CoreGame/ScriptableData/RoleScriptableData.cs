using System;
using System.Linq;
using System.Reflection;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.CoreGame.Configurations
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
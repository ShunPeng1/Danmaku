using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.CoreGame.Configurations
{
    [CreateAssetMenu(fileName = "RoleConfig", menuName = "Configurations/RoleConfig")]
    public class RoleConfig : ScriptableObject
    {
        public IDanmakuRole Role;
        public int MinPlayerCountPrerequisite;
        public DanmakuRoleEnum InitialRoleEnum;
        
    }
}
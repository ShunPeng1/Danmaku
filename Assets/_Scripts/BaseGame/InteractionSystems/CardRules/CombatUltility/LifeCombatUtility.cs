namespace _Scripts.CoreGame.InteractionSystems.CardRules.CombatUltility
{
    public static class LifeCombatUtility
    {
        public static bool CanHeal(DanmakuPlayerModel playerModel)
        {
            if (playerModel.IsAlive && !playerModel.Life.IsGreaterOrEqualToMax())
            {
                return true;
            }
            
            return false;
        }
        
    }
}
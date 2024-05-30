using System.Collections.Generic;

namespace _Scripts.CoreGame.InteractionSystems.CardRules.CombatUltility
{
    public static class AttackCombatUtility
    {
        
        public static bool CanAttackInRange(DanmakuPlayerGroupModel groupModel, DanmakuPlayerModel attackerPlayer, DanmakuPlayerModel targetedPlayer)
        {
            if (!attackerPlayer.IsAlive || !targetedPlayer.IsAlive)
            {
                return false;
            }
            
            int attackerIndex = groupModel.Players.FindIndex(playerModel => playerModel == attackerPlayer);
            int targetIndex = groupModel.Players.FindIndex(playerModel => playerModel == targetedPlayer);

            int rightDistance = 1;
            for (int i =(attackerIndex+1)%groupModel.PlayerCount; i != targetIndex && i != attackerIndex; i = (i+1)%groupModel.PlayerCount)
            {
                if (groupModel.Players[i].IsAlive)
                {
                    rightDistance++;
                }
            }
            
            int leftDistance = 1;
            for (int i = (attackerIndex-1+groupModel.PlayerCount)%groupModel.PlayerCount; i != targetIndex && i != attackerIndex; i = (i-1+groupModel.PlayerCount)%groupModel.PlayerCount)
            {
                if (groupModel.Players[i].IsAlive)
                {
                    leftDistance++;
                }
            }
            
            int distance = rightDistance < leftDistance ? rightDistance : leftDistance;
            
            return distance + targetedPlayer.Distance.Get() <= attackerPlayer.Range.Get();
            
        }


        public static void AttackPlayer(DanmakuPlayerModel attackerPlayerModel, DanmakuPlayerModel targetedPlayerModel)
        {
            targetedPlayerModel.Life.Decrease(attackerPlayerModel.Power.Get());
            
        }
        
        public static void UseDanmakuCardPlayer(DanmakuPlayerModel attackerPlayerModel)
        {
            attackerPlayerModel.DanmakuCardPlayedCount.Increase(1);
        }
        
        public static void UseSpellCardPlayer(DanmakuPlayerModel attackerPlayerModel)
        {
            attackerPlayerModel.SpellCardPlayedCount.Increase(1);
        }
        
        public static bool CanUseDanmakuCardPlayer(DanmakuPlayerModel attackerPlayerModel)
        {
            return attackerPlayerModel.DanmakuCardPlayedCount.Get() < attackerPlayerModel.DanmakuCardPlayedCount.GetMaxValue();
        }
        
        public static bool CanUseSpellCardPlayer(DanmakuPlayerModel attackerPlayerModel)
        {
            return attackerPlayerModel.SpellCardPlayedCount.Get() < attackerPlayerModel.SpellCardPlayedCount.GetMaxValue();
        }
        
    }
}
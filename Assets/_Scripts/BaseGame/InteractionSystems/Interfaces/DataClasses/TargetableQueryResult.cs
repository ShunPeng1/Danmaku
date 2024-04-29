using System.Collections.Generic;

namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public enum TargetableTypeEnum
    {
        Player,
        Card,
    }

    
    public class TargetableQueryResult
    {
        public readonly TargetableTypeEnum TargetableType;
        public readonly List<IDanmakuTargetable> Targetables;
        
        public TargetableQueryResult(TargetableTypeEnum targetableType, List<IDanmakuTargetable> targetables)
        {
            TargetableType = targetableType;
            Targetables = targetables;
        }

    }
}
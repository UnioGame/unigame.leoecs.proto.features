namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;
    using Game.Code.Animations.EffectMilestones;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public struct AbilityEffectMilestonesComponent : IProtoAutoReset<AbilityEffectMilestonesComponent>
    {
        public EffectMilestone[] Milestones;
        
        public void SetHandlers(IProtoPool<AbilityEffectMilestonesComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref AbilityEffectMilestonesComponent c)
        {
            c.Milestones = Array.Empty<EffectMilestone>();
        }
    }
}
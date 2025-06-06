namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Components
{
	using System;
	using System.Collections.Generic;
	using ActionTools;
	using Data;
	using LeoEcs.Proto;
	using Leopotam.EcsProto;

	/// <summary>
	/// ADD DESCRIPTION HERE
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct RestrictUITapAreaActionComponent : IProtoAutoReset<RestrictUITapAreaActionComponent>
	{
		public List<RestrictTapArea> Areas;
		public ActionId ActionId;
		
		public void SetHandlers(IProtoPool<RestrictUITapAreaActionComponent> pool) => pool.SetResetHandler(AutoReset);
		
		public static void AutoReset(ref RestrictUITapAreaActionComponent c)
		{
			c.Areas ??= new List<RestrictTapArea>();
			c.Areas.Clear();
		}
	}
}
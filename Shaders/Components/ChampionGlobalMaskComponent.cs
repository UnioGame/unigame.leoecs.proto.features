namespace UniGame.Ecs.Proto.Shaders.Components
{
	using System.Collections.Generic;
	using LeoEcs.Proto;
	using Leopotam.EcsProto;
	using UnityEngine;

	/// <summary>
	/// Store global mask variables and materials
	/// </summary>
	public struct ChampionGlobalMaskComponent : IProtoAutoReset<ChampionGlobalMaskComponent>
	{
		public List<string> Variables;
		public List<Material> Materials;
		
		public void SetHandlers(IProtoPool<ChampionGlobalMaskComponent> pool) => pool.SetResetHandler(AutoReset);
		
		public static void AutoReset(ref ChampionGlobalMaskComponent c)
		{
			c.Variables ??= new List<string>();
			c.Materials ??= new List<Material>();
			
			c.Materials.Clear();
			c.Variables.Clear();
		}
	}
}
namespace UniGame.Ecs.Proto.AbilityInventory.Converters
{
	/// <summary>
	/// Convert ability meta data to entity
	/// </summary>
	using Aspects;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;
	[ECSDI]
	public class AbilityInventoryTool : IEcsSystem, IProtoInitSystem
	{
		private ProtoWorld _world;
		
		private AbilityInventoryAspect _inventoryAspect;

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
		}
		
		


	}
}
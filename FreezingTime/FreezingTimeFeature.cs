namespace UniGame.Ecs.Proto.Gameplay.FreezingTime
{
	using Components;
	using Cysharp.Threading.Tasks;
	 
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using Systems;
	using UniGame.LeoEcs.Bootstrap.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Freezing Time Feature", fileName = "Freezing Time Feature")]
	public class FreezingTimeFeature : BaseLeoEcsFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			ecsSystems.DelHere<FreezingTimeCompletedEvent>();
			
			// Responsible for freezing time. Wait for the request FreezingTimeRequest
			ecsSystems.Add(new FreezingTimeSystem());
			
			ecsSystems.DelHere<FreezingTimeRequest>();
		}
	}
}
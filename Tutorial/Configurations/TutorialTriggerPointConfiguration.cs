namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Configurations
{
	using System.Collections.Generic;
	using Abstracts;
	using Components;
	using Game.Ecs.Core.Components;
	using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
	using Leopotam.EcsProto;

	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
    
#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
#if ODIN_INSPECTOR
	[InlineProperty]
#endif
	[CreateAssetMenu(menuName = "Game/Tutorial/Tutorial Trigger Point Configuration", fileName = "Tutorial Trigger Point Configuration")]
	public class TutorialTriggerPointConfiguration : ScriptableObject
	{
		#region Inspector
        
#if ODIN_INSPECTOR
		[BoxGroup("Start")]
#endif
		[SerializeReference]
		public ITutorialTrigger StartTrigger;
		
#if ODIN_INSPECTOR
		[BoxGroup("Start")]
#endif
		[SerializeReference]
		public List<ITutorialAction> StartTriggerActions;
		
#if ODIN_INSPECTOR
		[BoxGroup("Final")]
#endif
		[SerializeReference]
		public ITutorialTrigger FinalTrigger;
		
#if ODIN_INSPECTOR
		[BoxGroup("Final")]
#endif
		[SerializeReference]
		public List<ITutorialAction> FinalTriggerActions;

		#endregion
		
		public bool Apply(ProtoWorld world, ProtoEntity entity)
		{
			var startTriggerEntity = world.NewEntity();
			var finalTriggerEntity = world.NewEntity();

			entity.AddChild(startTriggerEntity, world);
			ref var startActionsComponent = ref world.AddComponent<TutorialActionsComponent>(startTriggerEntity);
			
			entity.AddChild(finalTriggerEntity, world);
			ref var finalActionsComponent = ref world.AddComponent<TutorialActionsComponent>(finalTriggerEntity);

			StartTrigger ??= new EmptyTrigger();
			StartTrigger.ComposeEntity(world, startTriggerEntity);
			startActionsComponent.Actions.AddRange(StartTriggerActions);
            
			FinalTrigger ??= new EmptyTrigger();
			FinalTrigger.ComposeEntity(world, finalTriggerEntity);
            finalActionsComponent.Actions.AddRange(FinalTriggerActions);
			
			return true;
		}
	}
}
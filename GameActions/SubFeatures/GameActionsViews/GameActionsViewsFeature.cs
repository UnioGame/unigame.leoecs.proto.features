namespace Vampire.Game.ECS.UI.GameActionsViews
{
    using Cysharp.Threading.Tasks;
    using Data;
    using global::Game.Ecs.ButtonAction;
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// handle game actions to show views
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class GameActionsViewsFeature : EcsFeature,IGameActionsSubFeature
    {
        [InlineProperty]
        [HideLabel]
        public GameActionViewsData gameActionViewSettings = new(); 
        
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            world.SetGlobal(gameActionViewSettings);
            ecsSystems.AddService(gameActionViewSettings);
            
            ecsSystems.AddSystem(new OpenViewByGameActionSystem());
            return UniTask.CompletedTask;
        }
    }
}
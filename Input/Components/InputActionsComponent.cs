namespace Game.Ecs.Input.Components
{
    using System;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Proto;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Component representing an input actions.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct InputActionsComponent : IProtoAutoReset<InputActionsComponent>
    {
        public DefaultInputActions InputActions;
        
        public void SetHandlers(IProtoPool<InputActionsComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref InputActionsComponent c)
        {
            c.InputActions = new DefaultInputActions();
        }
    }
}
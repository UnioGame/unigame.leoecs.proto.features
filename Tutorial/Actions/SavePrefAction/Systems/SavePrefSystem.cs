namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.SavePrefAction.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
    [ECSDI]
    public class SavePrefSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private SavePrefAspect _aspect;

        private ProtoItExc _filter = It
            .Chain<SavePrefComponent>()
            .Exc<CompletedSavePrefComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var prefComponent = ref _aspect.SavePref.Get(entity);
                if (!PlayerPrefs.HasKey(prefComponent.Value))
                {
                    PlayerPrefs.SetString(prefComponent.Value, prefComponent.Value);
                    PlayerPrefs.Save();
                }

                _aspect.CompletedSavePref.Add(entity);
            }
        }
    }
}
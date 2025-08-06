namespace UniGame.Ecs.Proto.Characteristics.Base
{
    using System.Runtime.CompilerServices;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using RealizationSystems;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public static class CharacteristicsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IProtoSystems AddCharacteristic<TCharacteristic>(this IProtoSystems ecsSystems)
            where TCharacteristic : struct
        {
            //remove changed marker
            ecsSystems.DelHere<CharacteristicChangedComponent<TCharacteristic>>();
            //update characteristic value by source event
            ecsSystems.DelHere<CharacteristicValueChangedEvent<TCharacteristic>>();
            ecsSystems.DelHere<OwnerCharacteristicChangedSelfEvent<TCharacteristic>>();

            ecsSystems.AddSystem(new CreateCharacteristicModificationSystem<TCharacteristic>());
            ecsSystems.AddSystem(new DetectCharacteristicChangedSystem<TCharacteristic>());
            //add owner characteristic changed event
            ecsSystems.AddSystem(new MarkOwnerCharacteristicChangedSystem<TCharacteristic>());

            //create health characteristic
            ecsSystems.AddSystem(new ChangeTargetCharacteristicMaxLimitationSystem<TCharacteristic>());
            ecsSystems.AddSystem(new ChangeTargetCharacteristicMinLimitationSystem<TCharacteristic>());
            ecsSystems.AddSystem(new ChangeTargetCharacteristicValueSystem<TCharacteristic>());
            ecsSystems.AddSystem(new ChangeTargetCharacteristicBaseSystem<TCharacteristic>());
            
            ecsSystems.AddSystem(new CreateCharacteristicValueSystem<TCharacteristic>());
            
            ecsSystems.AddSystem(new AddCharacteristicModificationSystem<TCharacteristic>());
            ecsSystems.DelHere<CreateModificationRequest<TCharacteristic>>();
            
            ecsSystems.AddSystem(new ResetTargetCharacteristicSystem<TCharacteristic>());
            ecsSystems.AddSystem(new ResetTargetCharacteristicMaxLimitSystem<TCharacteristic>());
            ecsSystems.AddSystem(new ResetTargetCharacteristicModificationsSystem<TCharacteristic>());
            
            ecsSystems.AddSystem(new RemoveCharacteristicModificationSystem<TCharacteristic>());
            ecsSystems.DelHere<RemoveCharacteristicModificationRequest<TCharacteristic>>();
            //convert recalculate health request to recalculate characteristic request
            ecsSystems.AddSystem(new RecalculateCharacteristicSystem<TCharacteristic>());
            
            ecsSystems.DelHere<CreateModificationRequest<TCharacteristic>>();
            ecsSystems.DelHere<CreateCharacteristicRequest<TCharacteristic>>();
            ecsSystems.DelHere<ResetCharacteristicMaxLimitSelfRequest<TCharacteristic>>();
            ecsSystems.DelHere<ChangeMinLimitSelfRequest<TCharacteristic>>();
            ecsSystems.DelHere<ChangeCharacteristicValueRequest<TCharacteristic>>();
            ecsSystems.DelHere<ChangeMaxLimitSelfRequest<TCharacteristic>>();
            ecsSystems.DelHere<ChangeCharacteristicBaseRequest<TCharacteristic>>();
            ecsSystems.DelHere<ResetCharacteristicSelfRequest<TCharacteristic>>();
            ecsSystems.DelHere<ResetCharacteristicModificationsSelfRequest<TCharacteristic>>();
            
            return ecsSystems;
        }
    }
}
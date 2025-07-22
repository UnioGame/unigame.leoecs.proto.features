# Ecs Proto Features

- [Game Actions](#game-actions)


# Game Actions

If you know a concept of abstract user input actions, when you already know what is a game actions. 
That's a simple way to define custom important game action and react to it with ECS systems.

As an example:

- **SelectGameMode**.
- **ConfirmGameStart**.
- **ShowHeroesCollection**.

Of course, you can define any game action that you need and add support of any input system that you want.

## Configuration

![game actions config](https://github.com/UnioGame/UniGame.LeoEcs.Proto.Features/blob/main/GitAssets/gameactions_config.png)


# Characteristics Feature

The Characteristics feature is a simple way to define characteristics of entities and use them in the game logic.

by default, it has the following characteristics:

- Ability Power
- Armor
- Attack Speed
- Attack Range
- Critical Chance
- Critical Multiplier
- Health
- Health Regeneration
- Mana
- Mana Regeneration
- Speed
- Shield
- Dodge Chance
- Cooldown
- Block
- Attack Damage
- Splash Damage

All characteristics inherits

```csharp
    /// <summary>
    /// provides a feature to increase the damage of abilities,
    /// allows you to change the strength of abilities by AbilityPowerComponent
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/SomeNewCharacteristic Feature")]
    public sealed class SomeNewCharacteristicAsset : CharacteristicFeature<SomeNewCharacteristic>
    {
    }
    
    [Serializable]
    public sealed class SomeNewCharacteristic : CharacteristicEcsFeature
    {
        //allo to defin custom characteristic components and registation logic
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            //refister new characteristic and create all base logic
            ecsSystems.AddCharacteristic<SomeNewCharacteristicComponent>();

            return UniTask.CompletedTask;
        }
    }
```

When you characteristic is changed, you will receive `CharacteristicValueChangedEvent<TCharacteristic>` event

```csharp
//All request components to characteristic
CreateModificationRequest<TCharacteristic>>();
CreateCharacteristicRequest<TCharacteristic>>();
ResetCharacteristicMaxLimitSelfRequest<TCharacteristic>>();
ChangeMinLimitSelfRequest<TCharacteristic>>();
ChangeCharacteristicValueRequest<TCharacteristic>>();
ChangeMaxLimitSelfRequest<TCharacteristic>>();
ChangeCharacteristicBaseRequest<TCharacteristic>>();
ResetCharacteristicSelfRequest<TCharacteristic>>();
ResetCharacteristicModificationsSelfRequest<TCharacteristic>>();
```

Demo filter to get notification of health characteristic changes

```csharp
private ProtoIt _filter = It
    .Chain<CharacteristicChangedComponent<HealthComponent>>()
    .Inc<CharacteristicComponent<HealthComponent>>()
    .Inc<HealthComponent>()
    .End();
```


# Death Feature





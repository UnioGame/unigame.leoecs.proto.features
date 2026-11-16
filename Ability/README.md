# Ability

Способности на LeoEcsProto. Способность — это сущность с набором шагов на таймлайне: «через 0.3 с нанести урон», «запустить кулдаун», «проиграть анимацию», «выпустить снаряд». Данные описываются в `AbilityConfiguration`. Позже эти ассеты будет собирать нодовый редактор.

## Из чего состоит

| Что | Где | Зачем |
|---|---|---|
| `AbilityConfiguration` | `AbilityService` | ScriptableObject, список шагов (`IAbilityBehaviour`) |
| `IAbilityBehaviour` | `AbilityService/Description` | Шаг, который при сборке вешает на способность свои компоненты |
| `TimelineAbilityBehaviour` | `AbilityService/Description` | Шаг на таймлайне с задержкой `delay` от момента применения |
| `AbilityConfigurationConverter` | `AbilityBase/Converters` | Выдаёт юниту способности из конфигов: слот и основная ли |
| `AbilityAspect`, `AbilityOwnerAspect` | `AbilityBase/Aspects` | Пулы и методы: `ActivateAbility`, `SetTarget`, `TryGetInHandAbility`… |
| `AbilityFeature` | `AbilityBase` | Регистрирует системы и вложенные фичи (`AbilitySubFeature`) |
| `TimelineFeature` | `FakeTimeline` | Проигрывает шаги таймлайна по времени |
| Подфичи | `SubFeatures` | Cooldown, Effects, Projectiles, Animations, Visuals: каждая обрабатывает свои шаги |

Готовые шаги:
- `AbilityTargetingBehaviour` — радиус, отношение (враг/союзник) и категория целей. Это не шаг таймлайна, просто данные.
- `ApplyEffectsBehaviour` — наложить эффекты на цели.
- `CooldownBehaviour` — кулдаун и момент его запуска.
- `SelfDestructBehaviour` — владелец способности умирает, к примеру дла абилки камикадзе. Срабатывает после эффектов того же момента.
- `ProjectileBehaviour`, `AnimationBehaviour`, `VisualsBehaviour`.

## Жизненный цикл

```
Конвертер юнита (AbilityConfigurationConverter)
  └─ EquipAbilityConfigurationSelfRequest
       └─ EquipAbilityConfigurationSystem: собирает шаги, кладёт в слот,
          ставит ActiveAbilityComponent; основную сразу берёт в руку

Кто-то решил ударить (ИИ, ввод, игровая система)
  └─ AbilityAspect.ActivateAbility(owner, ability, target)
       = SetTarget + SetInHandAbilitySelfRequest + ApplyAbilitySelfRequest
  └─ ApplyAbilityRequestSystem: способность чья и в руке? → AbilityValidationSelfRequest
  └─ ApplyAbilitySystem (только Active и не AbilityUsing):
       создаёт таймлайн с контекстом (владелец, способность, цели),
       снимает Active, перезапускает кулдаун, ставит AbilityUsing + AbilityStartUsingSelfEvent
  └─ TimelineSystem: пришло время шага → ExecuteTimelinePlayableRequest
       └─ система подфичи делает работу шага (урон, снаряд, анимация…)
  └─ последний шаг → TimelineEndEvent, таймлайн удаляется
  └─ CompleteAbilityOnTimelineEndSystem → CompleteAbilitySelfRequest
  └─ CompleteAbilitySystem: снимает AbilityUsing, кидает AbilityCompleteSelfEvent
  └─ кулдаун прошёл (CooldownFinishedSelfEvent) → AbilityCompleteCooldownSystem
       возвращает ActiveAbilityComponent — можно применять снова
```

Ключевые метки на сущности способности:
- `ActiveAbilityComponent` — готова к применению.
- `AbilityUsingComponent` — сейчас играет, повторно не применяется.
- `DefaultAbilityComponent` — основная, например обычная атака.

## Порядок фич (обязателен)

В наборе фич боя (`Combat Features`) порядок такой:

1. **Target Selection раньше Ai.** Размеры буферов поиска целей задаёт фича Target Selection, а ИИ создаёт буферы при сборке систем.
2. **Timeline раньше Ability.** `TimelineEndEvent` должен дожить до `CompleteAbilityOnTimelineEndSystem`.
3. **Effects с `Damage Effect Feature`** в списке эффектов, иначе шаг «наложить эффекты» ничего не сделает.

## Как применить способность из кода

```csharp
private AbilityAspect _abilityAspect;

// способность в руке у юнита
if (_abilityAspect.TryGetInHandAbility(unit, out var ability)
    && _abilityAspect.Active.Has(ability)
    && _abilityAspect.IsAbilityCooldownPassed(ability))
{
    _abilityAspect.ActivateAbility(unit, ability, targetPackedEntity);
}
```

ИИ делает то же самое в `ActivateAbilityActionSystem`.

## Как добавить новый шаг

1. **Компонент шага** — данные, которые нужны в момент срабатывания. Один компонент на файл, по шаблону пакета.
2. **Behaviour** — наследник `TimelineAbilityBehaviour`. В `ComposeBehaviour` положи компонент на `playableEntity`:
   ```csharp
   [Serializable]
   public sealed class PushBehaviour : TimelineAbilityBehaviour
   {
       public float force;

       public override void ComposeBehaviour(ProtoWorld world, ProtoEntity abilityEntity, ProtoEntity playableEntity)
       {
           ref var push = ref world.AddComponent<PushPlayableComponent>(playableEntity);
           push.Force = force;
       }
   }
   ```
3. **Система** с фильтром `Chain<PushPlayableComponent>().Inc<ExecuteTimelinePlayableRequest>()`. Контекст применения (владелец, цели) берётся так:
   ```csharp
   ref var execute = ref _timelineAspect.TimelineExecute.Get(playableEntity);
   if (!execute.TimelineContextEntity.Unpack(_world, out var contextEntity)) continue;
   ref var context = ref _abilityAspect.AbilityContext.Get(contextEntity);
   ```
   `ExecuteTimelinePlayableRequest` удаляется в `TimelineFeature`, руками удалять не нужно.
4. **Подфича** — наследник `AbilitySubFeature`. Систему добавь в подходящий этап, для эффектов обычно `OnApplyEffectsSystems`. Затем нажми `Fill` в ассете `Ability Feature`, чтобы подфича попала в список.

Источник эффектов шага — **владелец** способности (`AbilityContextComponent.abilityOwner`), а не сама способность. Поэтому эффекты вроде «урон от атаки» берут характеристики юнита.

## Подводные камни

- Способность без шагов на таймлайне сразу получает `TimelineEndEvent` и завершается в том же цикле.
- `IsAbilityCooldownPassed` смотрит на время последнего применения. Кулдаун перезапускается в момент применения (`ApplyAbilitySystem`), а не в конце таймлайна.
- Id способностей из конфига отрицательные (-1, -2…) и с каталожными не пересекаются.
- Смена способности в руке отменяется, пока идёт другая, не основная способность (`DiscardSetInHandWhileExecutingAbilitySystem`).

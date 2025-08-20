### ECS States — a state system for ECS

The `game.ecs.state` feature provides a simple, extensible entity state machine. 
A state is represented by a component; state changes are initiated via requests, 
and the state lifecycle is communicated through events.

### Architecture (key building blocks)

- `StateComponent` — the active state of an entity (integer identifier).
- `SetStateSelfRequest` — a request to change the entity's state.
- `StopStateSelfRequest` — a request to stop the current state (reset to 0).
- `StateChangedSelfEvent` — an event that the state changed; contains `FromStateId` and `NewId`.
- `IStateComponent` — marker interface for a typed state (e.g., `MenuStateComponent`).
- `BaseStateFeature<TState, TFinished, TStarted>` — base feature that registers systems for a specific state.
- `StatesMapAsset` — ScriptableObject mapping name/type to an integer id. Updated in the editor.

### Installation and integration

1) Create a `StatesFeature` via the editor menu: “ECS Proto/Features/States/States Feature”.

2) Add the created `StatesFeature` to your ECS configuration (your features assembly).

3) Populate the states map. In the `StatesFeature` inspector there is a “Fill States” command that finds all `IStateFeature` implementations and adds them, and also calls `statesMap.UpdateStates()` to build the id map.

4) (Optional if you don’t have Odin Inspector) Open the context menu for the `StatesMapAsset` asset and click “Update States” to rebuild the list after adding new `IStateComponent` types.

### Defining a state and automatic registration

Create a state as a `struct` with the `IStateComponent` marker interface and then a small feature based on `BaseStateFeature<TState, TFinished, TStarted>`.

```csharp
// New state component
public struct MenuStateComponent : IStateComponent {}

// Feature that registers all systems for this state:
[Serializable]
public class MenuStateFeature : BaseStateFeature<MenuStateComponent,
    MenuStateFinishedSelfEvent, MenuStateStartedSelfEvent> {}
```

The base registers the typical systems (adding/removing the typed component on change, start/finish events)

### Switching state at runtime

At minimum, add a `SetStateSelfRequest` with the desired `Id` to the entity. `SetStateSystem` will set `StateComponent.Value` and emit `StateChangedSelfEvent`.

To get the id by state type inside systems, use the states aspect:

```cssharp StatesAspect.cs
public int GetStateId(Type stateType)
{
    TypeStates.TryGetValue(stateType, out var stateData);
    return stateData?.id ?? 0;
}
```

You can also stop the state using`StopStateSelfRequest`

### Using on prefabs (converter)

To set the initial state directly from the inspector, use `StatesConverter` on the converter object in the entity’s prefab. It will add `SetStateSelfRequest` during conversion.

In the editor (when Odin is present), the list of available states comes from `StatesMapAsset`.

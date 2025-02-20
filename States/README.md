# ECS States

This folder contains the state machine system for the ECS. It's a way to manage the state of an entity. 

## How to use

Add define #GAME_ECS_STATES to your project to enable feature

1. Create a new feature StateFeature with "ECS Proto/Features/States/States Feature" menu.
2. Add it to the ECS Feature Configuration
3. StateDataAsset will created automatically in the same folder as the feature.


4. Define All States in the StateDataAsset

- image

5. Regenerate States Ids for external usage

It's will generate a new file with all states ids in "Assets/UniGame.Generated/Ecs/StateId.cs"

6. Create State Entity prefab with StatesConverter converter and setup entity states

7. Setup States behaviours on every state


## ECS States

- StateComponent - component with current state id 
- ChangeStateSelfRequest - request to change state for entity if this action is allowed when StateChangedSelfEvent is fired on the entity
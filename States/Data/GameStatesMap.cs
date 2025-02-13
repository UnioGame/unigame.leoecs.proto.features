namespace Game.Ecs.State.Data
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class GameStatesMap
    {
        public Dictionary<int, State> States = new(32);
        public Dictionary<string, State> StatesNames = new(32);
    }
}
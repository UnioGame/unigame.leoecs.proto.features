namespace Game.Ecs.State.Data
{
    using System;

    [Serializable]
    public struct StateResult
    {
        public static readonly StateResult Default = new StateResult
        {
            Completed = false,
            Failed = false,
            NextState = 0,
            Message = string.Empty,
            Error = string.Empty
        };
        
        public static readonly StateResult CompletedResult = new StateResult
        {
            Completed = true,
            Failed = false,
            NextState = 0,
            Message = string.Empty,
            Error = string.Empty
        };
        
        public bool Completed;
        public bool Failed;
        public int NextState;
        public string Message;
        public string Error;
    }
}
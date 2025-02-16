namespace Game.Modules.SequenceActions.Components
{
    using System;
    using Leopotam.EcsLite;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SequenceActionProgressComponent : IEcsAutoReset<SequenceActionProgressComponent>
    {
        public bool IsSuccess;
        public bool IsFinished;
        public float Progress;
        public string ActionName;
        public string Message;
        public string Error;
        
        public void AutoReset(ref SequenceActionProgressComponent c)
        {
            c.Progress = 0;
            c.ActionName = string.Empty;
            c.IsFinished = false;
            c.Message = string.Empty;
        }
    }
}
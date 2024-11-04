namespace Game.Ecs.SpineAnimation.Data.AnimationType
{
    public partial struct AnimationTypeId
    {
        public static AnimationTypeId Walk = new AnimationTypeId { value = 0 };
        public static AnimationTypeId Run = new AnimationTypeId { value = 1 };
        public static AnimationTypeId Idle = new AnimationTypeId { value = 2 };
        public static AnimationTypeId Atack = new AnimationTypeId { value = 3 };
    }
}

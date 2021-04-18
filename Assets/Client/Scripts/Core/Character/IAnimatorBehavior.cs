
namespace GameCore
{
    public interface IAnimatorBehavior
    {
        void SetAnimationTrigger(int triggerID);
        void SetAnimatorParamBool(int animID, bool state);
        void SetAnimatorParamFloat(int animID, float value);
    }
}

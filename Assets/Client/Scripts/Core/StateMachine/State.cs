

namespace GameCore.StateMachines
{
    public abstract class State
    {
        protected IMovementBehavior m_MovementBehevior;
        protected IAnimatorBehavior m_AnimatorBehevior;

        protected Character m_Character;
        protected StateMachine m_StateMachine;

        protected State(IMovementBehavior movementBehavior, IAnimatorBehavior animatorBehavior, Character character, StateMachine stateMachine)
        {
            m_MovementBehevior = movementBehavior;
            m_AnimatorBehevior = animatorBehavior;
            m_Character = character;

            m_StateMachine = stateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }

}

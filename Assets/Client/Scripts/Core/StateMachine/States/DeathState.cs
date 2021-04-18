using UnityEngine;

namespace GameCore.StateMachines
{

    public class DeathState : State
    {
        private LayerMask m_CheckLayer;

        public DeathState(IMovementBehavior movement,
                            IAnimatorBehavior animator, Character character,
                                StateMachine stateMachine)
                                    :base(movement, animator, character, stateMachine) {}


        public override void Enter()
        {
            base.Enter();

            m_CheckLayer = 1 << 6;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if(m_MovementBehevior.GetSqrMagnitudeVelocity() < 2.0f)
            {
                var startRay = m_Character.m_Transform.position - (m_Character.m_Transform.forward * 1);
                startRay += m_Character.m_Transform.up;
                
                if(Physics.Raycast(startRay, m_Character.m_Transform.forward, 2.0f, m_CheckLayer))
                {
                    Debug.Log("Death");
                }
            }
        }
    }

}

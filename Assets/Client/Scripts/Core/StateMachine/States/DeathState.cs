using UnityEngine;

namespace GameCore.StateMachines
{

    public class DeathState : State
    {
        private GameMode m_GameMode;
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

            var startRay = m_Character.m_Transform.position + m_Character.m_Transform.up;
            
            if(RayCheckDirection(startRay, m_Character.m_Transform.forward))
                m_Character.KillCharacter();

            if(RayCheckDirection(startRay, -m_Character.m_Transform.forward))
                m_Character.KillCharacter();
        }

        private bool RayCheckDirection(Vector3 startPoint, Vector3 direction)
        {
            return Physics.Raycast(startPoint, direction, 0.8f, m_CheckLayer);
        }
    }

}

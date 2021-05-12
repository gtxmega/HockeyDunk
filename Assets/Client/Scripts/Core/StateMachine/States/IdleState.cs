using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.StateMachines
{
    public class IdleState : State
    {
        public IdleState(IMovementBehavior movement,
                                IAnimatorBehavior animator, Character character,
                                        StateMachine stateMachine)
                                            :base(movement, animator, character, stateMachine) {}


        public override void Enter()
        {
            base.Enter();

            m_Character.ResetAllTriggers();
            m_Character.SetAnimationTrigger(Animator.StringToHash("Idle"));
        }

    }
}

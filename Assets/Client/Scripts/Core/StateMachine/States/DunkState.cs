using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameCore.Triggers;

namespace GameCore.StateMachines
{
    public class DunkState : DeathState
    {

        public DunkState(IMovementBehavior movement,
                                IAnimatorBehavior animator, Character character,
                                        StateMachine stateMachine)
                                            :base(movement, animator, character, stateMachine) {}


        public override void Enter()
        {
            base.Enter();

            m_AnimatorBehevior.SetAnimationTrigger(Animator.StringToHash("Dunk"));
        }

    }
}

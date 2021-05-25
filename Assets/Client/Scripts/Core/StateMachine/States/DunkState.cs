using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            m_AnimatorBehevior.SetAnimatorParamFloat(Animator.StringToHash("DunkPos"), Random.Range(0, 2));
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Actors.Player;



namespace Actors.Player.Animation
{
    public class PlayerAnimations : MonoBehaviour
    {

        Animator _animator;

        public void BeginSelf()
        {
            _animator = GetComponent<Animator>();

            SetupDelegates();    
        }

        void OnDisable()
        {
            RemoveDelegates();
        }


        void SetupDelegates()
        {
            PlayerAnimationDelegates.tEMP_ON_PLAYER_MOVEMENT += TEMP_SET_MOVING_TRIGGER;
        }

        void RemoveDelegates()
        {
            PlayerAnimationDelegates.tEMP_ON_PLAYER_MOVEMENT -= TEMP_SET_MOVING_TRIGGER;
        }


        void TEMP_SET_MOVING_TRIGGER(bool moving)
        {
            if(moving)
            {
                _animator.SetBool("TEMP_TRIGGER_MOVING", true);
            }

            else
            {
                _animator.SetBool("TEMP_TRIGGER_MOVING", false);
            }
        }
    }
}
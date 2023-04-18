using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.Player;
using Delegates.Actors.Player;


namespace Actors.Player
{
    [RequireComponent (typeof(SpriteRenderer))]
    public class PlayerSprite : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer _sprite;

        public void BeginSelf()
        {
            _sprite.flipX = false;

            SetupDelegates();
        }

        void OnDisable()
        {
            RemoveDelegates();
        }

        void SetupDelegates()
        {
            PlayerStateDelegates.onPlayerTurn += FlipSprite;
        }

        void RemoveDelegates()
        {
            PlayerStateDelegates.onPlayerTurn -= FlipSprite;
        }

        void FlipSprite(PlayerFacingDirectionState pState)
        {
            if(pState == PlayerFacingDirectionState.Right) _sprite.flipX = false;
            else _sprite.flipX = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.Player.Collisions;
using Actors.EnemyNS;

using Controllers.Utility.Statistics;

using Delegates.Actors.Player;
using Delegates.Actors.EnemyNS;
using Delegates.Utility;



namespace Actors.Player.Actions
{
    public class PlayerAttacking : MonoBehaviour
    {
        [SerializeField]
        PlayerAttackCollisions _playerAttackingCollisions;

        [SerializeField]
        GameObject _attackObjectChild;
        PlayerAttackCollisions _playerChildAttackingCollisions;

        public void BeginSelf()
        {
            _playerAttackingCollisions.BeginSelf();

            _playerChildAttackingCollisions = _attackObjectChild.GetComponent<PlayerAttackCollisions>();
            _playerChildAttackingCollisions.BeginSelf();

            SetupDelegates();
        }

        void OnDisable()
        {
            RemoveDelegates();
        }

        void SetupDelegates()
        {
            PlayerActionsDelegates.onPlayerAttack += Attack;
        }

        void RemoveDelegates()
        {
            PlayerActionsDelegates.onPlayerAttack -= Attack;
        }

        void Attack()
        {
            foreach(Enemy e in _playerAttackingCollisions.GetEnemiesList())
            {
                if(EnemyStatsDelegates.onEnemyHit != null)
                {
                    EnemyStatsDelegates.onEnemyHit(e);
                }
            }

            if (EnemyStatsDelegates.onEnemyDeathCheck != null) EnemyStatsDelegates.onEnemyDeathCheck(_playerAttackingCollisions.GetEnemiesList());

            _playerAttackingCollisions.ClearInRange();

            //Need to check child object (attack object) too
            foreach (Enemy e in _playerChildAttackingCollisions.GetEnemiesList())
            {
                if (EnemyStatsDelegates.onEnemyHit != null)
                {
                    EnemyStatsDelegates.onEnemyHit(e);
                }
            }

            if (EnemyStatsDelegates.onEnemyDeathCheck != null) EnemyStatsDelegates.onEnemyDeathCheck(_playerChildAttackingCollisions.GetEnemiesList());

            _playerChildAttackingCollisions.ClearInRange();


            if (StatisticsTrackingDelegates.onActionTracking != null) StatisticsTrackingDelegates.onActionTracking(ActionType.Attack);
        }
    }
}
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


        [SerializeField]
        SpriteRenderer _attackSprite;
        float _displayTime;


        [SerializeField] AudioSource attackSound;
        [SerializeField] AudioClip attack, breakVase, enemyDeath;

        public void BeginSelf()
        {
            _playerAttackingCollisions.BeginSelf();

            _playerChildAttackingCollisions = _attackObjectChild.GetComponent<PlayerAttackCollisions>();
            _playerChildAttackingCollisions.BeginSelf();


            _attackSprite.enabled = false;
            _displayTime = 0.25f;

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
            attackSound.clip = attack;
            attackSound.Play();

            StartCoroutine(ShowSprite());
            StartCoroutine(Attacking());

            if (StatisticsTrackingDelegates.onActionTracking != null) StatisticsTrackingDelegates.onActionTracking(ActionType.Attack);

            /*foreach(Enemy e in _playerAttackingCollisions.GetEnemiesList())
            {
                if(EnemyStatsDelegates.onEnemyHit != null)
                {
                    EnemyStatsDelegates.onEnemyHit(e);
                }
            }

            if (EnemyStatsDelegates.onEnemyDeathCheck != null) EnemyStatsDelegates.onEnemyDeathCheck(_playerAttackingCollisions.GetEnemiesList());

            _playerAttackingCollisions.ClearInRange();*/
        }

        void DestroyVase(ItemVaseController vase)
        {
            attackSound.clip = breakVase;
            attackSound.Play();

            Destroy(vase.gameObject);

            if (PlayerActionsDelegates.onVaseDestroyed != null) PlayerActionsDelegates.onVaseDestroyed();
        }

        IEnumerator ShowSprite()
        {
            _attackSprite.enabled = true;
            yield return new WaitForSeconds(_displayTime);
            _attackSprite.enabled = false;
        }

        IEnumerator Attacking()
        {
            float attackTime = 0.24f;
            float timeGone = 0f;

            while(timeGone < attackTime)
            {
                //Need to check child object (attack object) too
                foreach (Enemy e in _playerChildAttackingCollisions.GetEnemiesList())
                {
                    if (EnemyStatsDelegates.onEnemyHit != null)
                    {
                        attackSound.clip = enemyDeath;
                        attackSound.Play();
                        EnemyStatsDelegates.onEnemyHit(e);
                    }
                }

                if (EnemyStatsDelegates.onEnemyDeathCheck != null) EnemyStatsDelegates.onEnemyDeathCheck(_playerChildAttackingCollisions.GetEnemiesList());

                _playerChildAttackingCollisions.ClearInRange();

                for(int i = 0; i < _playerChildAttackingCollisions.GetVasesList().Count; i++)
                {
                    DestroyVase(_playerChildAttackingCollisions.GetVasesList()[i]);
                    i++;
                }

                yield return null;
                timeGone += Time.deltaTime;
            }
        }
    }
}
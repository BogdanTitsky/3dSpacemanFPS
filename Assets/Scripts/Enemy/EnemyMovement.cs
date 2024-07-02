using System;
using Audio;
using Damage;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform player;
        [SerializeField] private LayerMask layerPlayer;
        [SerializeField] private float attackCD;
        [SerializeField] private float sightRange, attackRange;
        [SerializeField] private bool playerInSightRange, playerInAttackRange;
        [SerializeField] private Animator enemyAnimator;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] Health health;
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private ZombieAudioConfigScriptableObject zombieAudio;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Enemy enemy;
        

        private Vector3 spawnPosition;
        private int walkPoint;
        private bool alreadyAttacked;
        private bool isAlive = true;

        public event Action OnAttackPlayer;

        private void Awake()
        {
            spawnPosition = transform.position;
        }

        private void OnEnable()
        {
            health.OnDeath += Death;
            enemy.OnRespawn += Respawn;
        }

        private void OnDisable()
        {
            health.OnDeath -= Death;
            enemy.OnRespawn -= Respawn;
        }

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, layerPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, layerPlayer);

            if (isAlive){
                if (!playerInSightRange && !playerInAttackRange)
                    Patrolling();
                if (playerInSightRange && !playerInAttackRange)
                    ChasePlayer();
                if (playerInSightRange && playerInAttackRange)
                    AttackPlayer();
            }
        }

        private void Patrolling()
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                walkPoint = (walkPoint + 1) % patrolPoints.Length;
                agent.destination = patrolPoints[walkPoint].position;
            }
        }

        private void ChasePlayer()
        {
            agent.speed = speed;
            agent.destination = player.position;
        }

        private void AttackPlayer()
        {
            agent.isStopped = true;
            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                agent.speed = 0;
                enemyAnimator.SetTrigger("Attack");
                alreadyAttacked = true;
                OnAttackPlayer?.Invoke();
                zombieAudio.PlayBiteClip(audioSource);
                Invoke(nameof(ResetAttack), attackCD);
            }
        }

        private void ResetAttack()
        {
            agent.speed = speed;
            alreadyAttacked = false;
            agent.isStopped = false;
        }
        
        private void Death(Vector3 position)
        {
            isAlive = false;
            zombieAudio.PlayDeathClip(audioSource);
            agent.speed = 0;
            agent.isStopped = true;
            rigidbody.isKinematic = true;
        }
        
        private void Respawn()
        {
            isAlive = true;
            agent.speed = speed;
            agent.isStopped = false;
            rigidbody.isKinematic = false;
            transform.position = spawnPosition;
            health.ResetHealth();
            walkPoint = 1;
            enemyAnimator.SetTrigger("Reset");
        }
      
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }

    }
}

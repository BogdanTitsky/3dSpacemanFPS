using System;
using System.Collections;
using Game;
using UnityEngine;

namespace Enemy
{
    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {        
        [SerializeField] private Animator animator;
        [SerializeField] private float respawnDelay = 5f;
        
        public Health health;
        public EnemyPainResponse PainResponse;
        public event Action OnRespawn;


        private void OnEnable()
        {
            health.OnDeath += Die;
        }

        private void OnDisable()
        {
            health.OnDeath -= Die;
        }

        private void Die(Vector3 position)
        {
            animator.SetTrigger("Dead");
            StartCoroutine(RespawnAfterDelay(respawnDelay));
        }

        private IEnumerator RespawnAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Debug.Log("RespawnAfterDelay");
            OnRespawn?.Invoke();
        }
    }
}
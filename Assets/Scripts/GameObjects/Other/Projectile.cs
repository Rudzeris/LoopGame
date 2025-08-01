using Assets.Scripts.GameObjects.Entities;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Other
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile Settings")]
        [SerializeField] private float speed = 10f;
        public int Damage { get; set; }
        public Fraction Fraction { get; set; }
        [SerializeField] private float lifeTime = 5f;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            // Делаем коллайдер триггером
            var col = GetComponent<Collider>();
            col.isTrigger = true;

            // Запускаем движение вперёд по локальной оси Z
            _rb.linearVelocity = transform.forward * speed;

            StartCoroutine(SelfDestruct());
        }

        private void OnTriggerEnter(Collider other)
        {
            var entity = other.GetComponent<IBasicEntity>();
            if (entity != null && entity.Fraction != this.Fraction)
            {
                entity.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }

        private IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}
using Assets.Scripts.GameObjects.Entities;
using Assets.Scripts.GameObjects.Other;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Attacks
{

    public abstract class Shooter : MonoBehaviour
    {
        [Header("Fire Settings")]
        [SerializeField] protected Transform firePoint;
        [SerializeField] protected GameObject projectilePrefab;
        [SerializeField] protected float shootInterval = 1f;
        [SerializeField] private Fraction fraction;
        [SerializeField] private int damage = 1;

        protected float _lastShootTime;

        protected virtual void Shoot()
        {
            GameObject ball = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projectile = ball?.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Fraction = fraction;
                projectile.Damage = damage;
            }
        }
    }
}

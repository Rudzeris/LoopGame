using UnityEngine;

namespace Assets.Scripts.GameObjects.Attacks
{
    public class EnemyShooter : Shooter
    {
        [Header("Enemy Settings")]
        [SerializeField] private float detectionRange = 5f;
        [SerializeField] private float rotationSpeed = 180f; // градусы в секунду

        private Transform _player;

        private void Start()
        {
            var playerGO = GameObject.FindWithTag("Player");
            if (playerGO != null)
                _player = playerGO.transform;
        }

        private void Update()
        {
            if (_player == null)
                return;

            RotateTowardsPlayer();
            TryShoot();
        }

        private void RotateTowardsPlayer()
        {
            // вектор на цель, обнуляем Y, чтобы не наклоняться
            Vector3 dir = _player.position - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude < 0.01f)
                return;

            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }

        private void TryShoot()
        {
            float dist = Vector3.Distance(transform.position, _player.position);
            if (dist > detectionRange)
                return;

            if (Time.time - _lastShootTime < shootInterval)
                return;

            Shoot();
            _lastShootTime = Time.time;
        }
    }
}
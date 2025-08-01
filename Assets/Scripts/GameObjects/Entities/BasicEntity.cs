using System;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Entities
{
    [RequireComponent(typeof(Collider))]
    public class BasicEntity : MonoBehaviour, IBasicEntity
    {
        public event Action<IBasicEntity, int> OnTakenDamage;
        public event Action<IBasicEntity> OnDestroyed;
        [SerializeField] private int _hp = 5;
        [SerializeField] private int _maxHp = 10;
        [SerializeField] private Fraction _fraction;
        public bool IsDestroyed { get; private set; }
        public int HP => _hp;

        public int MaxHP => _maxHp;

        public Fraction Fraction => _fraction;

        public void TakeDamage(int damage)
        {
            _hp = Math.Clamp(_hp - damage, 0, MaxHP);
            Damaged(damage);
            if (_hp == 0 && !IsDestroyed)
            {
                Destroyed();
            }
        }
        private void Damaged(int damage)
        {
            OnTakenDamage?.Invoke(this, damage);
            try
            {
                Messenger<IBasicEntity, int>.Broadcast(GameEvent.ENTITY_TAKE_DAMAGE, this, damage);
            }
            catch (Exception e)
            {
                Debug.LogError(e?.Message);
            }
        }
        private void Destroyed()
        {
            OnDestroyed?.Invoke(this);
            try
            {
                Messenger<IBasicEntity>.Broadcast(GameEvent.ENTITY_IS_DESTROYED, this);
            }
            catch (Exception e)
            {
                Debug.LogError(e?.Message);
            }
        }
    }
}

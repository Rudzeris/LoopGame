using System;

namespace Assets.Scripts.GameObjects.Entities
{
    public enum Fraction { Neutral, Player, Enemy}
    public interface IBasicEntity
    {
        event Action<IBasicEntity, int> OnTakenDamage;
        event Action<IBasicEntity> OnDestroyed;
        int MaxHP { get; }
        int HP { get; }
        void TakeDamage(int damage);
        Fraction Fraction { get; }
        
    }
    public interface IEntity : IBasicEntity { }
}

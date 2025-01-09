using System;

namespace IdleArcade.Data
{
    [Serializable]
    public class Stats
    {
        public int   CurrentTrunk;
        public float DamageRadius;
        public float CurrentHP;
        public float MaxHP = 250f;

        public Action Changed;

        private float _speed = 10f;
        private int _maxTrunk = 10;
        private float _gun = 110f;
        private float _damage = 110f;

        public float Speed { get => _speed; private set => _speed = value; }
        public int MaxTrunk { get => _maxTrunk; private set => _maxTrunk = value; }
        public float Gun { get => _gun; private set => _gun = value; }
        public float Damage { get => _damage; private set => _damage = value; }

        public void ResetHP()
        {
            CurrentHP = MaxHP;
        }

        public void UpgradeSpeed(float value)
        {
            Speed += value;
            Changed?.Invoke();
        }

        public void UpgradeMaxHP(float value)
        {
            MaxHP += value;
            Changed?.Invoke();
        }

        public void UpgradeMaxTrunk(int value)
        {
            MaxTrunk += value;
            Changed?.Invoke();
        }

        public void UpgradeGun(float value)
        {
            Gun += value;
            Changed?.Invoke();
        }

        public void UpgradeDamage(float value)
        {
            Damage += value;
            Changed?.Invoke();
        }
    }
}

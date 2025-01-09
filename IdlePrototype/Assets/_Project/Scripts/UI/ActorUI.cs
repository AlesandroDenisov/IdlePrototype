using System;
using IdleArcade.Player;
using IdleArcade.Logic;
using UnityEngine;

namespace IdleArcade.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += OnUpdateHpBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
      
            if(health != null)
                Construct(health);
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= OnUpdateHpBar;
        }

        private void OnUpdateHpBar()
        {
            HpBar.SetValue(_health.Current, _health.Max);
        }
    }
}
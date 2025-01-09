using UnityEngine;
using IdleArcade.Enemy;

namespace IdleArcade.Player
{
    //[RequireComponent(typeof(NPCAttack))]
    public class PlayerCheckAttackRange : MonoBehaviour
    {
        public PlayerAttack Attack;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += OnTriggerEnter;
            TriggerObserver.TriggerExit += OnTriggerExit;

            Attack.DisableAttack();
        }

        private void OnDestroy()
        {
            TriggerObserver.TriggerEnter -= OnTriggerEnter;
            TriggerObserver.TriggerExit -= OnTriggerExit;
        }

        private void OnTriggerExit(Collider obj)
        {
            Attack.DisableAttack();
        }

        private void OnTriggerEnter(Collider obj)
        {
            Attack.EnableAttack(obj.transform);
        }
    }
}
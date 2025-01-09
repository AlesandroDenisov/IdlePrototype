using IdleArcade.Logic;
using UnityEngine;

namespace IdleArcade.Enemy
{
    //[RequireComponent(typeof(NPCAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        public Attack Attack;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            Debug.Log("CheckAttackRange - Start()");

            TriggerObserver.TriggerEnter += OnTriggerEnter;
            TriggerObserver.TriggerExit += OnTriggerExit;

            Attack.DisableAttack();
        }

        private void OnDestroy()
        {
            Debug.Log("CheckAttackRange - Start()");

            TriggerObserver.TriggerEnter -= OnTriggerEnter;
            TriggerObserver.TriggerExit -= OnTriggerExit;
        }

        private void OnTriggerExit(Collider obj)
        {
            Debug.Log("CheckAttackRange - OnTriggerExit()");

            Attack.DisableAttack();
        }

        private void OnTriggerEnter(Collider obj)
        {
            Debug.Log("CheckAttackRange - OnTriggerEnter()");

            Attack.EnableAttack();
        }
    }
}
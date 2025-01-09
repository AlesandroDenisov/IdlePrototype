using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace IdleArcade.Enemy
{
    [RequireComponent(typeof(NPCFollowPlayer))]
    public class NPCAggression : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public NPCFollowPlayer Follow;
        public float Cooldown;

        private bool _hasAggressionTarget;
        private Coroutine _aggressionCoroutine;

        private void Start()
        {
            TriggerObserver.TriggerEnter += OnTriggerEnter;
            TriggerObserver.TriggerExit += OnTriggerExit;

            Follow.enabled = false;
        }

        private void OnDestroy()
        {
            TriggerObserver.TriggerEnter -= OnTriggerEnter;
            TriggerObserver.TriggerExit -= OnTriggerExit;
        }

        private void OnTriggerEnter(Collider obj)
        {
            if (_hasAggressionTarget) return;

            StopAggressionCoroutine();
            SwitchFollowOn();
            Debug.Log("Enemy started chasing the player");
        }

        private void OnTriggerExit(Collider obj)
        {
            if (!_hasAggressionTarget) return;

            _aggressionCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
        }

        private void StopAggressionCoroutine()
        {
            if (_aggressionCoroutine == null) return;

            StopCoroutine(_aggressionCoroutine);
            _aggressionCoroutine = null;
            Debug.Log("Enemy stopped chasing the player");
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(Cooldown);
            SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            _hasAggressionTarget = true;
            Follow.enabled = true;
        }

        private void SwitchFollowOff()
        {
            Follow.enabled = false;
            _hasAggressionTarget = false;
        }
    }
}

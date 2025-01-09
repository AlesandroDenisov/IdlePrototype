using IdleArcade.Core.Factory;
using IdleArcade.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IdleArcade.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPCFollowPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 8f;

        public NavMeshAgent Agent;

        private Transform _playerTransform;
        //private IGameFactory _gameFactory;

        public void Construct(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Update()
        {
            if (IsInitialized() && IsPlayerNotReached())
                Agent.destination = _playerTransform.position;
        }

        private bool IsInitialized()
        {
            return _playerTransform != null;
        }

        private bool IsPlayerNotReached()
        {
            return (Agent.transform.position - _playerTransform.position).sqrMagnitude >= MinimalDistance * MinimalDistance;
        }
    }

}


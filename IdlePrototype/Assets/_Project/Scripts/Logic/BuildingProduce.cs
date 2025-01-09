using System;
using System.Collections;
using UnityEngine;

namespace IdleArcade.Logic
{
    public class BuildingProduce : MonoBehaviour, ILootableEvent
    {
        //public Building Building;

        public event Action Happened;
        public float productionTime = 3f; // время производства

        private void Start()
        {
            //Building.ProduceChanged += OnProduceChanged;
            Invoke("Produce", productionTime);
        }

        private void OnDestroy()
        {
            //Building.ProduceChanged -= OnProduceChanged;
        }

        private void Produce()
        {
            Happened?.Invoke();
        }
    }
}
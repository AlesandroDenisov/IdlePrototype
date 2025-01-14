﻿using IdleArcade.Data;
using System;
using TMPro;
using UnityEngine;

namespace IdleArcade.UI.Elements
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI Counter;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.ResourceData.Changed += UpdateCounter;
        }


        private void Start()
        {
            UpdateCounter();
        }
        private void UpdateCounter()
        {
            //Counter.text = $"{_worldData.ResourceData.Collected}";
        }

        private void UpdateCounter(ResourceType type)
        {
            //Counter.text = $"{_worldData.ResourceData.Collected}";
        }
    }
}
using System;
using IdleArcade.Data;
using IdleArcade.Services.PersistentProgress;
using UnityEngine;

namespace IdleArcade.Logic
{
    public class BuildingSpawner : MonoBehaviour, ISavedProgress
    {
        public BuildingType BuildingType;
        public bool Active;
        private string _id;
    
        private void Awake()
        { 
            _id = GetComponent<UniqueId>().Id;
        }


        public void LoadProgress(PlayerProgress progress)
        {
            // if (progress.KillData.ClearedSpawners.Contains(_id))
            // Active = true;
            // else
            Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            //if(Active)
            //progress.KillData.ClearedSpawners.Add(_id);
        }

        private void Spawn()
        {
        }
    }
}
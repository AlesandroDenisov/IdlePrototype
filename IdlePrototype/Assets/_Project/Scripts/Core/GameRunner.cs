using UnityEngine;

namespace IdleArcade.Core
{
    public class GameRunner : MonoBehaviour
    {
        // If there is no GameEntryPointBootstrap on the Unity scene,
        // then it will add the prefab of Bootstrapper to the scene
        public GameEntryPointBootstrap BootstrapPrefab;
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameEntryPointBootstrap>();
      
            if(bootstrapper != null) return;

            Instantiate(BootstrapPrefab);
        }
    }
}
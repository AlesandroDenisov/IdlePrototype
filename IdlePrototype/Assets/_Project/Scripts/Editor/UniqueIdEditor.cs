using System.Linq;
using IdleArcade.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace IdleArcade.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            UniqueId uniqueId = (UniqueId) target;
      
            if(IsPrefab(uniqueId))
                return;
      
            if (string.IsNullOrEmpty(uniqueId.Id))
                Generate(uniqueId);
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();
        
                if(uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                    Generate(uniqueId);
            }
        }

        private bool IsPrefab(UniqueId uniqueId)
        {
            return uniqueId.gameObject.scene.rootCount == 0;
        } 

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.GenerateId();

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}
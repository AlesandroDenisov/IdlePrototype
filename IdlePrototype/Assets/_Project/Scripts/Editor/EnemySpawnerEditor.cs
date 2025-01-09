using IdleArcade.Enemy;
using UnityEditor;
using UnityEngine;

namespace IdleArcade.Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    
            public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(spawner.transform.position, new Vector3(3,4,4));
        }
    }
}
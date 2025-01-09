using IdleArcade.Data.Loot;
using UnityEditor;
using UnityEngine;

namespace IdleArcade.Editor
{
    [CustomEditor(typeof(LootSpawner))]
  public class LootSpawnerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(LootSpawner spawner, GizmoType gizmo)
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawSphere(spawner.transform.position, 1f);
    }
  }
}
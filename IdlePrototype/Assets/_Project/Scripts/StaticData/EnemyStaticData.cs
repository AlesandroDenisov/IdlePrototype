using UnityEngine;

namespace IdleArcade.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Static Data/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;
    
        [Range(1,300)]
        public int Hp = 100;
    
        [Range(1,50)]
        public float Damage = 20;
    
        [Range(.5f,5.0f)]
        public float EffectiveDistance = .5f;
    
        [Range(.5f,1)]
        public float Cleavage = .5f;

        [Range(0,10)]
        public float MoveSpeed = 3;

        public int LootValue = 2;
        public ResourceType LootType = ResourceType.Salvage;

        public GameObject Prefab;
    }
}
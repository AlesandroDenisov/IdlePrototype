using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleArcade.Logic
{
    public class Damage : MonoBehaviour
    {
        public float DamagePower;
        public GameObject BulletImpact;

        [Header("DamageOnExplode")] public bool ExplodeDamageBullet;
        public float DamageRadius;
        public float ExplosionForcePower;
        public LayerMask RayCasterLayer;

        public bool _impact;
        public void ExplodeDamage()
        {
            var hitCollider = Physics.OverlapSphere(transform.position, DamageRadius, RayCasterLayer);
            foreach (var hit in hitCollider)
            {
                var rigidBody = hit.GetComponent<Rigidbody>();

/*                if (hit.GetComponent<IHealth>())
                {
                    hit.GetComponent<IHealth>().TakeDamage(DamagePower);
                }*/

                if (rigidBody)
                {
                    rigidBody.AddExplosionForce(ExplosionForcePower, transform.position, DamageRadius);
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Missile")
                return;
            if (_impact)
                Instantiate(BulletImpact, other.ClosestPointOnBounds(transform.position), BulletImpact.transform.rotation);
            if (ExplodeDamageBullet)
            {
                ExplodeDamage();
            }
            gameObject.SetActive(false);
            Destroy(gameObject, 0.1f);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Missile")
                return;
            if (_impact)
                Instantiate(BulletImpact, other.contacts[0].point, BulletImpact.transform.rotation);
            if (ExplodeDamageBullet)
            {
                ExplodeDamage();
            }
            gameObject.SetActive(false);
            Destroy(gameObject, 0.1f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, DamageRadius);
        }
    }
}

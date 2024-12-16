using Components;
using UnityEngine;

namespace Bullets
{
    internal static class BulletDamageInteractor
    {
        internal static void DealDamage(Bullet bullet, GameObject other)
        {
            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(bullet.Damage);
            }
        }
    }
}
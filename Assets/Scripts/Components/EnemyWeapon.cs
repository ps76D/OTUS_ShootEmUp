using UnityEngine;

namespace Components
{
	public class EnemyWeapon: Weapon
	{
		[SerializeField] private float _velocityMultiplier = 2.0f;
		
		public override Vector2 CalcBulletVelocity()
		{
			Vector2 startPosition = this.GetPosition();
			Vector2 vector = (Vector2) this.GetTarget().transform.position - startPosition;
			Vector2 direction = vector.normalized;
			
			Vector2 velocity = direction * this._velocityMultiplier;
			return velocity;
		}
	}
}

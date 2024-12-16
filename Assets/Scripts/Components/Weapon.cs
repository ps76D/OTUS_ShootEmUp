using System;
using Bullets;
using UnityEngine;

namespace Components
{
	public abstract class Weapon: MonoBehaviour
	{
		[SerializeField] private BulletConfig _bulletConfig;
		
		[SerializeField] private Transform _firePoint;
		
		public event Action<Weapon> OnWeaponFire;
		
		private BulletManager _bulletManager;

		private HitPointsComponent _weaponTarget;

		private void Awake()
		{
			this._bulletManager = FindObjectOfType<BulletManager>();
		}

		protected virtual private void OnEnable()
		{
			OnWeaponFire += this._bulletManager.OnFlyBullet;
		}

		protected virtual private void OnDisable()
		{ 
			OnWeaponFire -= this._bulletManager.OnFlyBullet;
		}
		
		public abstract Vector2 CalcBulletVelocity();

		public virtual void Fire()
		{
			OnWeaponFire?.Invoke(this);
		}

		public void SetTarget(HitPointsComponent target)
		{
			this._weaponTarget = target;
		}

		public BulletConfig GetBulletConfig()
		{
			return this._bulletConfig;
		}

		public Vector2 GetPosition()
		{
			return this._firePoint.position;
		}

		protected private Quaternion GetRotation()
		{
			return this._firePoint.rotation;
		}

		protected private HitPointsComponent GetTarget()
		{
			return this._weaponTarget;
		}
	}
}

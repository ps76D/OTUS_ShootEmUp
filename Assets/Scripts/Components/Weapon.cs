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
			_bulletManager = FindObjectOfType<BulletManager>();
		}

		protected virtual private void OnEnable()
		{
			OnWeaponFire += _bulletManager.OnFlyBullet;
		}

		protected virtual private void OnDisable()
		{ 
			OnWeaponFire -= _bulletManager.OnFlyBullet;
		}
		
		public abstract Vector2 CalcBulletVelocity();

		public virtual void Fire()
		{
			OnWeaponFire?.Invoke(this);
		}

		public void SetTarget(HitPointsComponent target)
		{
			_weaponTarget = target;
		}

		public BulletConfig GetBulletConfig()
		{
			return _bulletConfig;
		}

		public Vector2 GetPosition()
		{
			return _firePoint.position;
		}

		protected private Quaternion GetRotation()
		{
			return _firePoint.rotation;
		}

		protected private HitPointsComponent GetTarget()
		{
			return _weaponTarget;
		}
	}
}

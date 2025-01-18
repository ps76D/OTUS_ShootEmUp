using System;
using Bullets;
using UnityEngine;
using Zenject;

namespace Components
{
	public abstract class Weapon: MonoBehaviour
	{
		[SerializeField] private BulletConfig _bulletConfig;
		
		[SerializeField] private Transform _firePoint;
		
		public event Action<Weapon> OnWeaponFire;
		
		[Inject]
		private BulletManager _bulletManager;

		private Transform _weaponTarget;

		protected virtual private void Start()
		{
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

		public void Fire()
		{
			OnWeaponFire?.Invoke(this);
		}

		public void SetTarget(Transform target)
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

		protected private Transform GetTarget()
		{
			return _weaponTarget;
		}
	}
}

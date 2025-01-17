﻿using AdventureGame.BattleSystem;
using AdventureGame.ObjectPooling;
using AdventureGame.Shared.ExtensionMethods;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.Projectiles
{
    /// <summary>
    /// When EnterTrigger2D remove this object
    /// </summary>
    public class Projectile : HitBox {

        [Title("Projectile")]
        [SerializeField] protected new Rigidbody rigidbody;

        [Header(" - Optional")]
        [SerializeField] protected Transform impactEffect;

        protected virtual void Reset() => TryGetComponent(out rigidbody);

        protected virtual void BeforeShoot() { }

        public void Shoot(Damage damage, float velocity)
        {
            ShootExplicitVelocity(damage, transform.forward * velocity);
        }

        public void Shoot(Damage damage, Vector3 velocity)
        {
            ShootExplicitVelocity(damage, transform.forward.Multiply(velocity));
        }

        public void ShootExplicitVelocity(Damage damage, Vector3 velocity)
        {
            BeforeShoot();

            SetDamage(damage);
            rigidbody.velocity = velocity;
        }

        protected override void AfterHit(TargetHitType targetHit, Vector3 contact)
        {
            // If is Hittable or KilledDamageable continue without Impact
            if (targetHit == TargetHitType.Alive || targetHit == TargetHitType.Unknown)
            {
                Impact();
            }
        }

        /// <summary>
        /// You can force the impact effect by calling this method. It is useful if you want to reproduce the impact effect
        /// </summary>
        public virtual void Impact()
        {
            ImpactVFX();
            this.ReturnToPool();
        }

        protected void ImpactVFX()
        {
            if (impactEffect)
            {
                impactEffect.SpawnPooledObject(transform.position, transform.rotation);
            }
        }
    }
}
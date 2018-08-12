using System.Collections;
using Cattle.Input;
using UnityEngine;

namespace Cattle.Weapons
{
    public class BaseWeapon : BaseComponent
    {
        public BaseProjectile projectilePrefab;
        
        public float shootSpeedIncrement;
        public float reloadPeriod;
        public bool useAutoShootSpeed = false;

        public float shootSpeed;
        public bool canShoot = false;
        
        protected BaseInput input;

        protected virtual void Awake()
        {
            input = transform.parent.GetComponent<BaseInput>();
        }
        
        void Start()
        {
            shootSpeed = 0.0f;
            
            StartCoroutine(Reload());
        }

        protected virtual void Update()
        {
            if (canShoot)
            {
                if (input.GetButtonUp("Fire1") || shootSpeed >= 1.0f)
                {
                    if (shootSpeed > float.Epsilon)
                    {
                        Shoot();
                    }
                }
                else
                {
                    if (input.GetButton("Fire1"))
                    {
                        shootSpeed += shootSpeedIncrement * Time.fixedDeltaTime;
                    }
                    else
                    {
                        shootSpeed = 0.0f;
                    }
                } 
            }
        }

        void Shoot()
        {
            canShoot = false;
            if (useAutoShootSpeed)
            {
                shootSpeed = Random.Range(0.6f, 1.0f);
            }

            shootSpeed = Mathf.Clamp(shootSpeed, 0.5f, 1.0f);
            
            InstantiateProjectile();

            shootSpeed = 0.0f;

            StartCoroutine(Reload());
        }

        protected virtual void InstantiateProjectile()
        {
        }

        IEnumerator Reload()
        {
            yield return new WaitForSeconds(reloadPeriod);
            canShoot = true;
            yield return null;
        }
    }
}
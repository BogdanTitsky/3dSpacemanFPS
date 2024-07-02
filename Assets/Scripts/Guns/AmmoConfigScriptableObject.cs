using System;
using UnityEngine;

namespace Guns
{
    [CreateAssetMenu(fileName = "Ammo Config", menuName = "Guns/Ammo Config", order = 3)]
    public class AmmoConfigScriptableObject : ScriptableObject
    {
        [SerializeField] private GunScriptableObject gun;

        [SerializeField] private int initAmmo = 120;
        [SerializeField] private int initClip = 30;
        
        public int maxAmmo = 120;
        public int clipSize = 30;

        public int currentAmmo = 120;
        public int currentClipAmmo = 30;

        private void OnEnable()
        {
                currentAmmo = initAmmo;
                currentClipAmmo = initClip;

            gun.OnShoot += Shoot;
        }

        private void Shoot()
        {
            currentClipAmmo--;
        }

        public void AddAmmo()
        {
            if (currentAmmo >= maxAmmo) return;
            currentAmmo += 10;
            if (maxAmmo < currentAmmo) currentAmmo = maxAmmo;
        }

        public void Reload()
        {                

            int maxReloadAmount = Mathf.Min(clipSize, currentAmmo);
            int availableBulletsInCurrentClip = clipSize - currentClipAmmo;
            int reloadAmount = Mathf.Min(maxReloadAmount, availableBulletsInCurrentClip);
            currentClipAmmo += reloadAmount;
            currentAmmo -= reloadAmount;
        }
        
        public bool CanReload()
        {
            return currentClipAmmo < clipSize && currentAmmo > 0;
        }
    }
}

using System;
using Animation;
using Guns;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [DisallowMultipleComponent]
    public class PlayerAction : MonoBehaviour
    {
        [SerializeField] private PlayerGunSelector gunSelector;
        [SerializeField] private bool autoReload = true;
        [SerializeField] private WeaponReloadAnimator weaponReloadAnimator;
        private bool _fireBtnIsPressed;
        public event Action OnReload;

        private bool _isReloading;

       
        private void OnEnable()
        {
            
                weaponReloadAnimator.OnReloadComplete += EndReload;
        }

        private void OnDisable()
        {
           
                weaponReloadAnimator.OnReloadComplete -= EndReload;
        }

        private void Update()
        {
            if (_fireBtnIsPressed) HandleFireBtnClick();
        }
        
        private void HandleFireBtnClick()
        {
            if (gunSelector != null && gunSelector.activeGun != null)
            {
                gunSelector.activeGun.TryToShoot(true);
                TryAutoReload();
            }
        }

        public void SetFireBtnIsClicked()
        {
            _fireBtnIsPressed = true;
        }
        public void SetFireBtnIsNotClicked()
        {
            _fireBtnIsPressed = false;
        }

        private void EndReload()
        {            
            gunSelector.activeGun.EndReload();
            _isReloading = false;
        }

        public void ShouldManualReload()
        {
            if (!_isReloading && gunSelector.activeGun.CanReload())
                Reload();
        }

        private void TryAutoReload()
        {
            if (!_isReloading && autoReload && gunSelector.activeGun.ammoConfig.currentClipAmmo == 0  &&
                gunSelector.activeGun.CanReload())
                Reload();
        }

        private void Reload()
        {
            gunSelector.activeGun.StartReloading();
            _isReloading = true;
            OnReload?.Invoke();
        }
    }
}
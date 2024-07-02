using System.Collections;
using System.Collections.Generic;
using Game;
using Guns;
using UnityEngine;

public class BuyAmmoBtn : MonoBehaviour
{
    [SerializeField] private PlayerGunSelector playerGunSelector;
    [SerializeField] private RunMoney runMoney;

    public void BuyAmmo()
    {
        if (runMoney.Money >= 10 && playerGunSelector.activeGun.ammoConfig.currentAmmo < playerGunSelector.activeGun.ammoConfig.maxAmmo)
        {
            playerGunSelector.activeGun.ammoConfig.AddAmmo();
            runMoney.DecrRunMoney(10);
        }
    }
}

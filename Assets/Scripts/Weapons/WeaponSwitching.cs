using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private PlayerThrow[] weapons;

    private void Start()
    {
        SwitchToDefaultWeapon();
    }

    private void SwitchToDefaultWeapon()
    {
        weapons[0].gameObject.SetActive(true);

        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }
}

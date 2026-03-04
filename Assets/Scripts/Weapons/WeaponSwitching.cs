using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private PlayerThrow[] weapons;

    private void Start()
    {
        SwitchToDefaultWeapon();
    }
    private void Update()
    {
        CheckSwitchButton();
    }

    // used when it switches to a default weapon
    private void SwitchToDefaultWeapon()
    {
        weapons[0].gameObject.SetActive(true);

        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }

    // checks if player has pressed button to switch weapons, used with WeaponPosition
    private void CheckSwitchButton()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponPosition(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponPosition(1);
        }
    }

    // if i equals to Alpha Number - 1, sets active the weapon and disables the others
    private void WeaponPosition(int weaponPoisiton)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == weaponPoisiton)
            {
                weapons[weaponPoisiton].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }

        }
    }
}

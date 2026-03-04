using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private PlayerThrow[] weapons;

    private InputAction weapon1Action;
    private InputAction weapon2Action;
    private InputAction weapon3Action;
    private InputAction weapon4Action;

    private void Start()
    {
        weapon1Action = InputSystem.actions.FindAction("weapon1");
        weapon2Action = InputSystem.actions.FindAction("weapon2");
        weapon3Action = InputSystem.actions.FindAction("weapon3");
        weapon4Action = InputSystem.actions.FindAction("weapon4");

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

        if (weapon1Action.WasPressedThisFrame())
        {
            WeaponPosition(0);
        }
        else if (weapon2Action.WasPressedThisFrame())
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

using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private PlayerThrow[] weapons;
    public bool[] unlocks;

    private InputAction[] switchAction;

    private void Start()
    {
        switchAction = new InputAction[weapons.Length];
        for (int i = 0; i < weapons.Length; i++) switchAction[i] = InputSystem.actions.FindAction($"weapon{i + 1}");

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
        for (int i = 0; i < switchAction.Length; i++)
            if (switchAction[i].WasCompletedThisFrame()) 
                WeaponPosition(i);
    }

    // if i equals to Alpha Number - 1, sets active the weapon and disables the others
    private void WeaponPosition(int weaponPosition)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == weaponPosition)
            {
                weapons[weaponPosition].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }

        }
    }
}

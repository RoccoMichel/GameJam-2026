using Unity.VisualScripting;
using UnityEngine;

public class PlayerScreenStatus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EffectZone")
        {
            ScreenEffects.Instance.panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ScreenEffects.Instance.panel.SetActive(false);
    }



}

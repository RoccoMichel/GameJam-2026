using Unity.VisualScripting;
using UnityEngine;

public class PlayerScreenStatus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EffectZone")
        {
            ScreenEffects.Instance.panels[0].SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ScreenEffects.Instance.panels[0].SetActive(false);
    }



}

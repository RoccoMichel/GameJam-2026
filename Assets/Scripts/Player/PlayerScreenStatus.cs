using Unity.VisualScripting;
using UnityEngine;

public class PlayerScreenStatus : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "EffectZone")
        {
            Debug.Log("wsda");
            ScreenEffects.Instance.SelectPanel(0);
            ScreenEffects.Instance.startFadingIn();
        }
    }


}

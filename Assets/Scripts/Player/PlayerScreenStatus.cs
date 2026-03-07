using UnityEngine;

public class PlayerScreenStatus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EffectZone"))
        {
            CanvasController.Instance.sodaPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CanvasController.Instance.sodaPanel.SetActive(false);
    }
}
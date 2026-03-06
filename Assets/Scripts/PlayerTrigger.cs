using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    public bool oneShot;
    public UnityEvent onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        onTrigger.Invoke();

        if (oneShot) Destroy(gameObject);
    }
}

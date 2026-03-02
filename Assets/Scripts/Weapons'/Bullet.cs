
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;


    private void Update()
    {
        rb.linearVelocity = transform.forward * moveSpeed;
    }

}

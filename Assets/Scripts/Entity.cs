using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Entity Attributes")]
    public string identity = "Unnamed Entity";
    public float maxHealth = 10;
    private float _health = 10;
    public float Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(0, maxHealth, value);
            if (_health <= 0 && !immortal) Die();
        }
    }
    public bool immortal;

    protected virtual void OnStart()
    {
        Health = maxHealth;
        if (!immortal && Health <= 0) Die();
    }

    private void Start() => OnStart();

    public virtual void Damage(float amount)
    {
        Health -= amount;
    }

    public virtual void Heal(float amount)
    {
        Health += amount;
    }

    public void SetImmortality(bool newState)
    {
        immortal = newState;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void OnReset()
    {
        gameObject.layer = LayerMask.NameToLayer("Entity");

    }
    private void Reset() => OnReset();
}

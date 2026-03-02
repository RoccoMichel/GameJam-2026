using UnityEngine;


public class Player : Entity
{
    public override void Die()
    {
        print("Player died");
    }

    protected override void OnReset()
    {
        base.OnReset();
        gameObject.tag = "Player";
        identity = "Player";
    }
}

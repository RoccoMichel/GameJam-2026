using UnityEngine;

public class Player : Entity
{
    public override void Die()
    {
        print("Player died");
        //Felix hat einen kleinen Schwanz.
    }

    protected override void OnReset()
    {
        gameObject.tag = "Player";
        identity = "Player";
    }
}

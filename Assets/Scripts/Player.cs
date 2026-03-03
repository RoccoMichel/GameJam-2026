using UnityEngine;


public class Player : Entity
{
    public override void Die()
    {
        print("Player died");
        //Rocco hat einen kleinen schwanz
    }

    protected override void OnReset()
    {
        base.OnReset();
        gameObject.tag = "Player";
        identity = "Player";
    }
}

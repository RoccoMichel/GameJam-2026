using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] public GameObject damagePanel;
 
    public override void Die()
    {
        if(immortal)
        print("Player died");
        else
        GameController.Instance.GameOver();
        //Felix hat einen kleinen Schwanz.
    }

    protected override void OnReset()
    {
        gameObject.tag = "Player";
        identity = "Player";
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
        StartCoroutine(FlashRed());

    }

    public IEnumerator FlashRed()
    {
        damagePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damagePanel.SetActive(false);
    }
}

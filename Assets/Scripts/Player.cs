using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public override void Die()
    {
        if (immortal) print("Player died");
        else GameController.Instance.GameOver();
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
        CanvasController.instance.damagePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        CanvasController.instance.damagePanel.SetActive(false);
    }
}

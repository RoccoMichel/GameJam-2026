using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("Player Healing")]
    public float healRate = 1f;
    public float timeUntilHeal = 6;
    private float timer = 0;
    public override void Die()
    {
        if (immortal) print("Player died");
        else GameController.Instance.GameOver();
    }
    private void Update()
    {
        // Heal Player when they haven't taken damage for some time
        timer = Mathf.Clamp(timer += Time.deltaTime, 0, timeUntilHeal);
        if (timer >= timeUntilHeal) Heal(healRate * Time.deltaTime);
    }
    protected override void OnReset()
    {
        gameObject.tag = "Player";
        identity = "Player";
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
        timer = 0;
        StartCoroutine(FlashRed());
    }

    public IEnumerator FlashRed()
    {
        CanvasController.instance.damagePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        CanvasController.instance.damagePanel.SetActive(false);
    }
}

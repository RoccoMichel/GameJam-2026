using System.Collections;
using UnityEngine;

public class Player : Entity
{
    private float timer = 0;
    private Settings settings;

    protected override void OnStart()
    {
        base.OnStart();
        settings = GameController.Instance.Settings;
    }

    public override void Die()
    {
        // Camera.main.GetComponent<Animator>().Play("Death");
        if (immortal) print("Player died");
        else GameController.Instance.GameOver();
    }
    private void Update()
    {
        // Heal Player when they haven't taken damage for some time
        timer = Mathf.Clamp(timer += Time.deltaTime, 0, settings.timeUntilHeal);
        if (timer >= settings.timeUntilHeal) Heal(settings.playerHealRate * Time.deltaTime);
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
        CanvasController.Instance.damagePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        CanvasController.Instance.damagePanel.SetActive(false);
    }
}

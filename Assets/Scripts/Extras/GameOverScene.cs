using TMPro;
using UnityEngine;

public class GameOverScene : MonoBehaviour
{
    public Settings settings;
    [SerializeField] private TMP_Text waveDisplay;

    private void Start()
    {
        waveDisplay.text = $"WAVE {settings.currentWave}";
    }
}

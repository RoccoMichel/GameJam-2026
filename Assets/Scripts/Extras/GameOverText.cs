using TMPro;
using UnityEngine;

public class GameOverText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverTextUI;
    public string[] gameOverText;

    private void DisplayText()
    {
        gameOverTextUI.text = gameOverText[Random.Range(0, gameOverText.Length)];
    }

    private void Awake()
    {
        DisplayText();
    }
}

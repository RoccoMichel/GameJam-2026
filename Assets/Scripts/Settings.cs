using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    [Header("Game Settings")]
    public int targetFrameRate = 60;
    public int currentWave = 0; // gets read in death scene

    [Header("Player Settings")]
    [Tooltip("If the player can press 'Q' to quick switch to their previous weapon")]
    public bool canQuickSwap = true;
    [Tooltip("How much the player heals per second")]
    public float playerHealRate = 1.5f;
    [Tooltip("Time in seconds before the player start to heal")]
    public float timeUntilHeal = 5.5f;

    [Header("Enemy Settings")]
    [Tooltip("Maximum number of Enemies allowed to exists at once")]
    public int maxEnemyCount = 30; // not implemented yet
}

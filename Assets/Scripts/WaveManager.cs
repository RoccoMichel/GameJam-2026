using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float waveDuration;
    private float timer;
    private bool isTimerOn;
    public int currentWaveIndex;
    private float waveTransitionTimer;
    [SerializeField] private float waveTransitionTime;

    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    [SerializeField] private List<GameObject> spawnPosList = new();
    private List<float> localCounters = new();

     
    private void Start()
    {
        StartNextWave();
    }
    private void Update()
    {
        if (currentWaveIndex >= waves.Length)
        {
            GameController.Instance.GameWon();
        
        }
        if (!isTimerOn) return;
        if (timer < waveDuration)
        {
            MangeCurrentWave();

            string timerString = ((int)(waveDuration - timer)).ToString();
            //waveManagerUI.UpdateTimerText(timerString);
        }
        else
        {
            StartWaveTransition();
        }
    }

    private void StartWave(int waveIndex)
    {
        CanvasController.instance.UpdateWave(currentWaveIndex);
        
        localCounters.Clear();
        foreach (WaveSegment segment in waves[waveIndex].segments)
        {
            localCounters.Add(1);
        }
        timer = 0;
        isTimerOn = true;
        Debug.Log("Starting wave " + currentWaveIndex);
    }
    private void StartNextWave()
    {
        StartWave(currentWaveIndex);
    }

    private void StartWaveTransition()
    {
        isTimerOn = false;

        currentWaveIndex++;

        if (PlayerPrefs.GetInt($"{SceneManager.GetActiveScene().buildIndex}Highscore", 0) < currentWaveIndex + 1)
            PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().buildIndex}Highscore", currentWaveIndex + 1);

        if (currentWaveIndex >= waves.Length)
        {
            GameController.Instance.GameWon();
        }
        else StartCoroutine(startNext());
    }
    public IEnumerator startNext()
    {
        waves[currentWaveIndex - 1].onComplete.Invoke();

        yield return new WaitForSeconds(waveTransitionTime);
        StartNextWave();
    }

    private void MangeCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];

            float tStart = segment.tStartEnd.x / 100 * waveDuration;
            float tEnd = segment.tStartEnd.y / 100 * waveDuration;

            if (timer < tStart || timer > tEnd) continue;

            float timeSinceSegmentStart = timer - tStart;

            if (timeSinceSegmentStart / (1f / segment.spawnFrequency) > localCounters[i])
            {
                Instantiate(segment.prefab, GetSpawnPos().position, Quaternion.identity, transform);
                localCounters[i]++;
            }
        }
        timer += Time.deltaTime;
    }

    private Transform GetSpawnPos()
    {
        GameObject newSpawn = spawnPosList[Random.Range(0, spawnPosList.Count)];
        if (newSpawn.activeSelf == false)
        {
            while(newSpawn.activeSelf == false)
            newSpawn = spawnPosList[Random.Range(0, spawnPosList.Count)];
        }

        return newSpawn.transform;
    }
}

[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
    public UnityEvent onComplete;
}
[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
}
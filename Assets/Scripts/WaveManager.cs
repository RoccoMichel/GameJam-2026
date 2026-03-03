using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
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
    [SerializeField] private List<GameObject> spawnPosList = new List<GameObject>();
    private List<float> localCounters = new List<float>();

     
    private void Start()
    {
        StartNextWave();
    }
    private void Update()
    {
        if (currentWaveIndex >= waves.Length)
        {
          /*  if (transform.GetComponentInChildren<RatBase>() == null)
            {
                Debug.Log("Waves completed");
                PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().buildIndex}BeatGame", 1);

                
            }*/
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
      //  GameUI.Instance.UpdateWavesDisplay(currentWaveIndex + 1);
      // Rocco implement this with your ui if you feel like it
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
        GameObject gameObject = spawnPosList[Random.Range(0, spawnPosList.Count)];
        if (gameObject.activeSelf == false)
        {
            while(gameObject.activeSelf == false)
            gameObject = spawnPosList[Random.Range(0, spawnPosList.Count)];
        }

        return gameObject.transform;
    }
}

[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}
[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
}
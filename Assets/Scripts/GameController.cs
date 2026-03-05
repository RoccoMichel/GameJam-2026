using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public bool tutorial;
    public bool debug;

    [Header("References")]
    private AudioSource audioSource;
    private InputAction debugAction;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        debugAction = InputSystem.actions.FindAction("Debug");

        if (tutorial) StartCoroutine(StartTutorial());
    }

    /// <param name="fileName">From: Resources/Sounds/...</param>
    public void SFX(string fileName)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/" + fileName));
    }
    public void SFX(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    /// <summary>
    /// Unlocks the weapon at the specified index for the player
    /// </summary>
    /// <param name="index">Starts at 0</param>
    public void UnlockWeapon(int index)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitching>().unlocks[index] = true;
    }
    /// <summary>
    /// Locks the weapon at the specified index for the player
    /// </summary>
    /// <param name="index">Starts at 0</param>
    public void LockWeapon(int index)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitching>().unlocks[index] = false;
    }

    private void Update()
    {
        if (debugAction.WasPressedThisFrame()) debug = !debug;
    }

    private void OnGUI()
    {
        if (!debug) return;

        // Text 
        GUI.Label(new Rect(10, 10, 100, 20), $"ms per frame: {System.Decimal.Round((decimal)(Time.deltaTime * 1000), 2)}");

        // Buttons
        if (GUI.Button(new Rect(10, 40, 100, 20), "Reload")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GUI.Button(new Rect(10, 70, 100, 20), "Exit")) Application.Quit(); ;
    }

    private void Reset()
    {
        transform.position = Vector3.zero;
        gameObject.tag = "GameController";
        gameObject.name = "--- GameController ---";
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void GameWon()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void FreezeGame()
    {
        Time.timeScale = 0f;
    }
    public void UnfreezeGame()
    {
        Time.timeScale = 1f;
    }
    private IEnumerator StartTutorial()
    {
        GameObject latest = null;
        int index = 0; // ORDER: Look, Walk, Shoot, Sprint!

        while (index < 4)
        {
            if (latest == null)
            {
                switch (index)
                {
                    case 0:
                        latest = CanvasController.instance.InstantiateTutorial("Look");
                        break;
                    case 1:
                        latest = CanvasController.instance.InstantiateTutorial("Walk");
                        break;
                    case 2:
                        latest = CanvasController.instance.InstantiateTutorial("Shoot");
                        break;
                    case 3:
                        latest = CanvasController.instance.InstantiateTutorial("Sprint");
                        break;
                }

                index++;
            }

            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}

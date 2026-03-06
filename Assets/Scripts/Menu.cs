using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectedOverride;

    private EventSystem eventSystem;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) Cursor.lockState = CursorLockMode.Confined;
        try { eventSystem = FindFirstObjectByType<EventSystem>().GetComponent<EventSystem>(); }
        catch
        {
            eventSystem = gameObject.AddComponent<EventSystem>().GetComponent<EventSystem>();
            gameObject.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
        }

        eventSystem.firstSelectedGameObject = firstSelectedOverride == null ? gameObject : firstSelectedOverride;
    }

    public void LoadSceneByString(string sceneName)
    {
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(sceneName);
    }

    public void OptionMenuButton()
    {
        Instantiate((GameObject)Resources.Load($"UI/{"Options Menu"}"), transform);
    }
    public void LoadSceneByIndex(int index)
    {
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(index);
    }

    public void LoadSceneThis()
    {
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PlaySound(AudioClip clip)
    {
        try { gameObject.GetComponent<AudioSource>().PlayOneShot(clip); }
        catch { gameObject.AddComponent<AudioSource>().PlayOneShot(clip); }
    }
    public void ToggleSelf()
    {
        SetActive(!gameObject.activeInHierarchy);
    }

    public void SetActive(bool b)
    {
        gameObject.SetActive(b);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

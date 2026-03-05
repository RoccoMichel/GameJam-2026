using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;
    [SerializeField] private TMP_Text healthDisplay;
    [SerializeField] private TMP_Text waveDisplay;
    [SerializeField] private Image crosshair;
    [SerializeField] internal Slider staminaBar;

    private InputAction pauseAction;
    private GameObject optionMenu;
    private GameObject tutorialMenu;
    private Player player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Feel free to remove the following!
        if (GetComponent<CanvasScaler>().uiScaleMode == CanvasScaler.ScaleMode.ConstantPixelSize)
            Debug.LogWarning($"{gameObject.name} is currently set to 'Constant Pixel Size', this is usually undesired!");
        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("No Event System in Scene!");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void Update()
    {
        healthDisplay.text = Mathf.Ceil(player.Health) + " HP";
        if (pauseAction.WasPressedThisFrame() && optionMenu == null)
            optionMenu = InstantiateMenu("Options Menu");
    }

    public void UpdateWave(int waveIndex)
    {
        waveDisplay.gameObject.GetComponent<Animator>().Play("Highlight");
        waveDisplay.text = $"WAVE {waveIndex + 1}";
    }

    public void UpdateCrosshair(string crosshairName)
    {
        crosshair.sprite = Resources.Load<Sprite>("UI/Crosshairs/" + crosshairName);
    }

    /// <summary>
    /// Instantiate GameObject onto Canvas from ResourceFolder
    /// </summary>
    /// <param name="resourceName">Prefab path within "Resources/UI/"</param>
    /// <return>The Instantiated GameObject (child of CanvasController)</return>
    public GameObject InstantiateMenu(string resourceName)
    {
        return Instantiate((GameObject)Resources.Load($"UI/{resourceName}"), transform);
    }
    public GameObject InstantiateTutorial(string resourceName)
    {
        if (tutorialMenu != null) Destroy(tutorialMenu);
        tutorialMenu = Instantiate((GameObject)Resources.Load($"UI/Tutorials/{resourceName}"), transform);

        return tutorialMenu;
    }
    public void InstantiateMenuVoid(string resourceName)
    {
        Instantiate((GameObject)Resources.Load($"UI/{resourceName}"), transform);
    }
    public void InstantiateTutorialVoid(string resourceName)
    {
        if (tutorialMenu != null) Destroy(tutorialMenu);
        tutorialMenu = Instantiate((GameObject)Resources.Load($"UI/Tutorials/{resourceName}"), transform);
    }

    private void Reset()
    {
        gameObject.name = "--- Canvas ---";
    }
}

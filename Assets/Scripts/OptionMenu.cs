using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text sensitivityDisplay;
    private PlayerCamera cameraController;

    private void Start()
    {
        try { GameController.Instance.FreezeGame(); } catch { }
        Cursor.lockState = CursorLockMode.Confined;
        try { cameraController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCamera>(); } catch { }
        slider.value = PlayerPrefs.GetFloat("sensitivity", 1);        
        sensitivityDisplay.text = $"SENSITIVITY: {Mathf.Round(slider.value * 100) / 100}";
    }

    public void UpdateSensitivity()
    {
        float newSensitivity = slider.value;
        PlayerPrefs.SetFloat("sensitivity", newSensitivity);
        try
        {
            if (cameraController == null)
                cameraController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCamera>();

            cameraController.mouseSen = newSensitivity;
        }
        catch { }

        sensitivityDisplay.text = $"SENSITIVITY: {Mathf.Round(newSensitivity * 100) / 100}";
    }

    private void OnDestroy()
    {
        try { GameController.Instance.UnfreezeGame(); } catch { }
        if (SceneManager.GetActiveScene().buildIndex != 0) Cursor.lockState = CursorLockMode.Locked; // don't lock on main menu
    }
}

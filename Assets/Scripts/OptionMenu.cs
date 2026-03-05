using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text sensitivityDisplay;
    private PlayerCamera cameraController;

    private void Start()
    {
        GameController.Instance.FreezeGame();
        Cursor.lockState = CursorLockMode.Confined;
        cameraController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCamera>();
        slider.value = PlayerPrefs.GetFloat("sensitivity", 1);        
        sensitivityDisplay.text = $"SENSITIVITY: {Mathf.Round(slider.value * 100) / 100}";
    }

    public void UpdateSensitivity()
    {
        float newSensitivity = slider.value;
        PlayerPrefs.SetFloat("sensitivity", newSensitivity);
        if (cameraController == null) 
            cameraController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCamera>();


        cameraController.mouseSen = newSensitivity;
        sensitivityDisplay.text = $"SENSITIVITY: {Mathf.Round(newSensitivity * 100) / 100}";
    }

    private void OnDestroy()
    {
        GameController.Instance.UnfreezeGame();
        Cursor.lockState = CursorLockMode.Locked;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour
{
    [Header("Behaviour")]
    public bool destroyAfterTime;
    public float destroyAfterSeconds = 5;
    public bool destroyAfterAction;
    [SerializeField] private InputActionReference action;

    [Header("Visuals")]
    public bool cycleInputs = true;
    public float timePerInputCycleSeconds = 0.8f;
    [SerializeField] private Sprite[] inputs;
    [SerializeField] private Image inputDisplay;

    private void Start()
    {
        if (cycleInputs) StartCoroutine(InputTransition(timePerInputCycleSeconds));

        if (destroyAfterTime) Destroy(gameObject, destroyAfterSeconds);
    }

    private void Update()
    {
        if (destroyAfterAction && action.action.WasPressedThisFrame()) Destroy(gameObject);
    }

    private IEnumerator InputTransition(float duration)
    {
        int index = 0;
        while (true)
        {
            inputDisplay.sprite = inputs[index];
            index++;
            if (index >= inputs.Length) index = 0;

            yield return new WaitForSeconds(duration);
        }
    }
}

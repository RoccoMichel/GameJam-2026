using UnityEngine;

public class Door : MonoBehaviour
{
    public enum States { OPEN, CLOSE };
    public States startState;
    public GameObject openState;
    public GameObject closeState;

    private void Start()
    {
        switch (startState)
        {
            case States.OPEN:
                openState.SetActive(true);
                closeState.SetActive(false);
                break;
            case States.CLOSE:
                closeState.SetActive(true);
                openState.SetActive(false);
                break;
        }
    }
    public void Open()
    {
        openState.SetActive(true);
        closeState.SetActive(false);
        GameController.Instance.SFX("door-open");
        CanvasController.instance.InstantiateTutorial("Door Alert");
    }

    public void Close()
    {
        GameController.Instance.SFX("door-close");
        closeState.SetActive(true);
        openState.SetActive(false);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum States { OPEN, CLOSE };
    public States startState;
    public GameObject openState;
    public GameObject closeState;
    public List<GameObject> spawnPosList = new List<GameObject>();
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
        for(int i = 0; i < spawnPosList.Count; i++)
        {
            GameObject spawnPos = spawnPosList[i];
            spawnPos.SetActive(true);
        }
        GameController.Instance.SFX("door-open");
        CanvasController.Instance.InstantiateTutorial("Door Alert");
    }

    public void Close()
    {
        GameController.Instance.SFX("door-close");
        closeState.SetActive(true);
        openState.SetActive(false);
    }


}

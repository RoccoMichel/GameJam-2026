using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour
{
    [Header("Panels and positions")]
    [SerializeField] public GameObject panel;



    public static ScreenEffects Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;   
    }




}

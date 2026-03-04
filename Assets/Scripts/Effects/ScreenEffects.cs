using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour
{
    [Header("Panels and positions")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] private int selectPanelPosiiton = 0;

    [Header("Fade speeds")]
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float fadeOutSpeed;

    [Header("Panel Opacity")]
    [SerializeField] private float paneOpacityFadeIn;
    [SerializeField] private float panelOpacityFadeOut;

    [Header("Other")]
    public bool canPlayAgain = true;

    public static ScreenEffects Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;   
    }

    public IEnumerator FadeOut()
    {
        for (float f = panelOpacityFadeOut; f >= -0.05f; f -= 0.05f)
        {
            Color c = panels[selectPanelPosiiton].GetComponent<Image>().color;
            c.a = f;
            panels[selectPanelPosiiton].GetComponent<Image>().color = c;
            yield return new WaitForSeconds(fadeOutSpeed);
            canPlayAgain = true;
        }

    }

    public IEnumerator FadeIn()
    {
        if (canPlayAgain == true)
        {
            for (float f = 0.05f; f <= paneOpacityFadeIn; f += 0.05f)
            {
                Color c = panels[selectPanelPosiiton].GetComponent<Image>().color;
                c.a = f;
                panels[selectPanelPosiiton].GetComponent<Image>().color = c;
                yield return new WaitForSeconds(fadeInSpeed);
                canPlayAgain = false;
            }
        }

    }

    public void startFadingIn()
    {
        StartCoroutine(FadeIn());
    }

    public void startFadingOut()
    {
        StartCoroutine(FadeOut());
    }

    // must be used to select which panel that is going to be affected
    public void SelectPanel(int selectedPanel)
    {
        selectPanelPosiiton = selectedPanel;
    }



}

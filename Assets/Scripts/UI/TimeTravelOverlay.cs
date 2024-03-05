using UnityEngine;
using UnityEngine.UI;



public class TimeTravelOverlay : MonoBehaviour
{

    private Image overlay;



    private void Awake()
    {
        
        overlay = GetComponent<Image>();
        overlay.enabled = false;

    }



    public void ToggleOverlay(bool isOn)
    {

        overlay.enabled = isOn;

    }


}
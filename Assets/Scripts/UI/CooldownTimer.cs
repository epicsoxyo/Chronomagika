using UnityEngine;
using UnityEngine.UI;



public class CooldownTimer : MonoBehaviour
{

    private Image timer;

    [SerializeField] private float fillValue = 1f;

    private float currentFill = 1f;
    private float timeElapsed = 0f;
    [SerializeField] private float lerpTime;



    private void Awake()
    {
        
        timer = GetComponent<Image>();
        timer.enabled = false;

    }


    public void ToggleVisibility(bool isOn)
    {

        timer.enabled = isOn;

    }



    public void UpdateCooldownTimer(float cooldownTime, float currentTime)
    {

        currentFill = fillValue;

        fillValue = 1 - (currentTime / cooldownTime);

        if(fillValue > 1f) fillValue = 1f;
        else if(fillValue < 0f) fillValue = 0f;

        timeElapsed = 0f;

    }



    private void Update()
    {

        timeElapsed += Time.deltaTime;
        timer.fillAmount = Mathf.Lerp(currentFill, fillValue, timeElapsed / lerpTime);

    }

}

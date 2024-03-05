using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{

    private Image img;

    [SerializeField] private float fillValue = 1f;
    [SerializeField] private float fractionalOffset;

    private float currentFill = 1f;
    private float timeElapsed = 0f;
    [SerializeField] private float lerpTime;



    private void Awake()
    {
        
        img = transform.GetChild(0).GetComponent<Image>();

    }



    public void UpdateManaUI(float mana, float maxMana)
    {

        currentFill = fillValue;

        fillValue = (mana / maxMana) * (1 - fractionalOffset) + fractionalOffset;

        if(fillValue > 1f) fillValue = 1f;
        else if(fillValue < 0f) fillValue = 0f;

        timeElapsed = 0f;

    }



    private void Update()
    {

        timeElapsed += Time.deltaTime;
        img.fillAmount = Mathf.Lerp(currentFill, fillValue, timeElapsed / lerpTime);

    }

}
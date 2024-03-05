using UnityEngine;
using TMPro;

public class SeedUI : MonoBehaviour
{

    private TextMeshProUGUI text;



    private void Awake()
    {
        
        text = GetComponent<TextMeshProUGUI>();

    }



    public void SetSeed(string seed)
    {

        text.SetText("FLOOR SEED: " + seed);

    }

}
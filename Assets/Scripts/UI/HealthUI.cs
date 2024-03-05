using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private List<Image> uiHearts = new List<Image>();



    public void UpdateHealthUI(float health)
    {

        int hearts = Mathf.FloorToInt(health / 10);

        if(hearts > 10) hearts = 10;

        for(int i = 0; i < hearts; i++) uiHearts[i].enabled = true;

        if(hearts < 10)
            for(int i = hearts; i < uiHearts.Count; i++) uiHearts[i].enabled = false;

    }

}
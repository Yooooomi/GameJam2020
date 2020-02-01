using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeUIConnector : MonoBehaviour
{

    public Slider slider;
    private Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
        slider.minValue = 0;
        slider.maxValue = stats.life;
    }

    private void Update()
    {
        slider.value = stats.life;
    }
}

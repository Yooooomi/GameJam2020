using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointPointPoint : MonoBehaviour
{
    public Text text;
    public float interval;

    public int maxPoints = 3;

    private int currentIndex;
    private float currentTime;
    private string initialString;

    private void Start()
    {
        initialString = text.text;
    }

    private void ManagePoint()
    {
        currentIndex = (currentIndex + 1) % (maxPoints + 1);

        text.text = initialString + new string('.', currentIndex);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > interval)
        {
            currentTime = 0;
            ManagePoint();
        }
    }
}

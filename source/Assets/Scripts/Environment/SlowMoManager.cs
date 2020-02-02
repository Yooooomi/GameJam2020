using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoManager : MonoBehaviour
{
    public static SlowMoManager Instance;

    public float speed;

    private float startValue;
    private float target = 1;
    private float currentAdvancement;

    private void Start()
    {
        currentAdvancement = speed;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetSlowMo(float target)
    {
        if (target != 1.0f)
            startValue = 1.0f;
        else
            startValue = Time.timeScale;
        currentAdvancement = 0;
        this.target = target;
    }

    private void Update()
    {
        if (currentAdvancement == speed)
        {
            return;
        }
        currentAdvancement += Time.unscaledDeltaTime;
        float ratio = currentAdvancement / speed;
        Time.timeScale = Mathf.Lerp(startValue, target, ratio);
    }
}

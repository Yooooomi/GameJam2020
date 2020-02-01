using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashText : MonoBehaviour
{
    public Text text;

    public void Init(string text)
    {
        this.text.text = text;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}

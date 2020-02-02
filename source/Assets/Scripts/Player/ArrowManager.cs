using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : MonoBehaviour
{
    public SpriteRenderer arrowContainer;
    public Text text;

    public void SetIdentity(string text, Color c)
    {
        arrowContainer.color = c;
        this.text.text = text;
        this.text.color = c;
    }
}

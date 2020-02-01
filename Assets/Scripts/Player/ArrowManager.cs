using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public SpriteRenderer arrowContainer;

    public void SetColor(Color c)
    {
        arrowContainer.color = c;
    }
}

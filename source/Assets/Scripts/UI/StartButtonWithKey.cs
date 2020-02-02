using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonWithKey : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (Inputs.GetButton("Start", Inputs.ButtonType.DOWN))
        {
            if (button.IsActive())
            {
                button.onClick.Invoke();
            }
        }
    }
}

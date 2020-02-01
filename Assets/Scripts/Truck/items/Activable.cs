using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activable : MonoBehaviour
{
    public UnityEvent onDelete;
    public UnityEvent methodToCall;

    private void Start()
    {
        if (methodToCall == null)
            methodToCall = new UnityEvent();
    }

    public void activate()
    {
        Debug.Log("ACTIVATE");
        methodToCall.Invoke();
    }

    private void OnDestroy()
    {
        Debug.Log("ON DELETE");
        onDelete.Invoke();
    }
}

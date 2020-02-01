using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Joiner : MonoBehaviour
{
    public GameObject prefab;
    private ControllerListener assigner;

    private Dictionary<int, GameObject> indexToPlayer = new Dictionary<int, GameObject>();

    private void Start()
    {
        assigner = GetComponent<ControllerListener>();

        assigner.OnPlayerEvent.AddListener(this.OnPlayer);
    }

    private void OnPlayer(int index, ControllerListener.ConnectionInfos infos, bool connecting)
    {
        if (connecting)
        {
            GameObject newPlayer = Instantiate(prefab);
            newPlayer.GetComponent<Inputs>().Connect(infos.controllerType, infos.controllerIndex);
            indexToPlayer[index] = newPlayer;
        }
        else
        {
            Destroy(indexToPlayer[index]);
            indexToPlayer[index] = null;
        }
    }

    private void Update()
    {
    }
}

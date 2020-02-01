using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerListener : MonoBehaviour
{
    [System.Serializable]
    public class ConnectionInfos
    {
        public KeyCode accessKey;
        public Inputs.ControllerType controllerType;
        public int controllerIndex;

        public ConnectionInfos(KeyCode ak, int ci, Inputs.ControllerType ct)
        {
            accessKey = ak;
            controllerIndex = ci;
            controllerType = ct;
        }
    }

    public class PlayerEvent : UnityEvent<int, ConnectionInfos, bool> { }

    public PlayerEvent OnPlayerEvent = new PlayerEvent();

    public ConnectionInfos[] keysToPress;
    private Dictionary<KeyCode, bool> keysPressed = new Dictionary<KeyCode, bool>();
    public ConnectionInfos[] connectedPlayers { get; private set; } = new ConnectionInfos[4]
    {
        null,
        null,
        null,
        null,
    };

    private void Start()
    {
        foreach (var i in keysToPress)
        {
            keysPressed[i.accessKey] = false;
        }
    }

    public int GetIndexInConnectedFromRank(int rank)
    {
        int current = 0;

        for (int i = 0; i < 4; i++)
        {
            if (connectedPlayers[i] != null)
            {
                if (current == rank) return i;
                current += 1;
            }
        }
        return -1;
    }

    private void AddPlayer(ConnectionInfos infos)
    {
        for (int i = 0; i < connectedPlayers.Length; i++)
        {
            if (connectedPlayers[i] == null)
            {
                keysPressed[infos.accessKey] = true;
                connectedPlayers[i] = infos;
                OnPlayerEvent.Invoke(i, infos, true);
                break;
            }
        }
    }

    private void RemovePlayer(ConnectionInfos infos)
    {
        for (int i = 0; i < connectedPlayers.Length; i++)
        {
            if (connectedPlayers[i] != null && connectedPlayers[i].accessKey == infos.accessKey)
            {
                keysPressed[infos.accessKey] = false;
                connectedPlayers[i] = null;
                OnPlayerEvent.Invoke(i, infos, false);
                break;
            }
        }
    }

    private void Update()
    {
        foreach (var i in keysToPress)
        {
            bool keyDown = Input.GetKeyDown(i.accessKey);
            if (!keysPressed[i.accessKey] && keyDown)
            {
                AddPlayer(i);
            }
            else if (keysPressed[i.accessKey] && keyDown)
            {
                RemovePlayer(i);
            }
        }
    }
}

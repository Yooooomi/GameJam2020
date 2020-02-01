using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Joiner : MonoBehaviour
{
    public GameObject prefab;
    private ControllerListener assigner;
    private int nbPlayers;

    private Dictionary<int, GameObject> indexToPlayer = new Dictionary<int, GameObject>();
    private List<GameObject> players = new List<GameObject>();

    [System.Serializable]
    public class OnPlayerJoins : UnityEvent<bool, int, Inputs.ControllerType> { }

    public OnPlayerJoins onPlayerJoins;

    private bool nextFrameInit;

    private void Start()
    {
        SceneManager.sceneLoaded += InitGame;
        assigner = GetComponent<ControllerListener>();

        assigner.OnPlayerEvent.AddListener(this.OnPlayer);
    }

    public void StartTheGame()
    {
        Debug.Log("Should start " + nbPlayers);
        if (nbPlayers != 2)
        {
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void InitGame(Scene s, LoadSceneMode m)
    {
        if (s.buildIndex != 1) return;
        for (int i = 0; i < 2; i++)
        {
            GameObject newPlayer = Instantiate(prefab);
            int index = assigner.GetIndexInConnectedFromRank(i);
            Debug.Log("Index - " + index);
            newPlayer.GetComponent<Inputs>().Connect(assigner.connectedPlayers[index].controllerType, assigner.connectedPlayers[index].controllerIndex);
            indexToPlayer[i] = newPlayer;
            players.Add(newPlayer);
        }
    }

    private void OnPlayer(int index, ControllerListener.ConnectionInfos infos, bool connecting)
    {
        if (connecting)
        {
            nbPlayers += 1;
            onPlayerJoins.Invoke(connecting, index, infos.controllerType);
        }
        else
        {
            nbPlayers -= 1;
            onPlayerJoins.Invoke(connecting, index, infos.controllerType);
        }
    }
}

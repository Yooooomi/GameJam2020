using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Joiner : MonoBehaviour
{
    public List<Color> colors;

    public int gameScene;
    public GameObject prefab;
    private ControllerListener assigner;

    private int nbPlayers;
    private bool started;

    private Dictionary<int, GameObject> indexToPlayer = new Dictionary<int, GameObject>();
    private List<GameObject> players = new List<GameObject>();

    [System.Serializable]
    public class OnPlayerJoins : UnityEvent<bool, int, Inputs.ControllerType> { }

    public OnPlayerJoins onPlayerJoins;
    public int lastWinner { get; private set; }

    private void Start()
    {
        SceneManager.sceneLoaded += InitGame;
        assigner = GetComponent<ControllerListener>();

        assigner.OnPlayerEvent.AddListener(this.OnPlayer);
    }

    public void StartTheGame()
    {
        if (!started && nbPlayers != 2)
        {
            return;
        }
        started = true;
        assigner.LockState();

        SceneManager.LoadScene(gameScene);
    }

    public void EndTheGame()
    {
        lastWinner = FindObjectOfType<GameLogic>().playerOneOnBike ? 2 : 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void InitGame(Scene s, LoadSceneMode m)
    {
        if (s.buildIndex != gameScene) return;
        for (int i = 0; i < 2; i++)
        {
            GameObject newPlayer = Instantiate(prefab);
            int index = assigner.GetIndexInConnectedFromRank(i);
            newPlayer.GetComponent<Inputs>().Connect(assigner.connectedPlayers[index].controllerType, assigner.connectedPlayers[index].controllerIndex);
            newPlayer.GetComponent<ArrowManager>().SetColor(colors[i]);
            indexToPlayer[i] = newPlayer;
            players.Add(newPlayer);
        }
    }

    private void OnPlayer(int index, ControllerListener.ConnectionInfos infos, bool connecting)
    {
        if (started) return;
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

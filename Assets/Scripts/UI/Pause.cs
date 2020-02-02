using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public GameObject body;
    public Button defaultButton;
    private EventSystem eventSystem;
    bool active = false;
    float oldTimeScale = 1.0f;

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        body.SetActive(false);
    }

    private void setActive(bool state)
    {
        body.SetActive(state);
        if (state == true)
        {
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(defaultButton.gameObject);
        }
        else
        {
            Time.timeScale = oldTimeScale;
        }
        active = state;
    }

    public void resume()
    {
        setActive(false);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void backToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Inputs.GetButton("Start", Inputs.ButtonType.DOWN))
        {
            setActive(!active);
        }
    }
}

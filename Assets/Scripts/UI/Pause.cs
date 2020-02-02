using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject body;
    private Inputs[] inputs;
    bool active = false;
    float oldTimeScale = 1.0f;

    private void Start()
    {
        body.SetActive(false);
        inputs = FindObjectsOfType<Inputs>();
        Debug.Log("Number of inputs: " + inputs.Length);
    }

    private void setActive(bool state)
    {
        body.SetActive(state);
        if (state == true)
        {
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;
        } else
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
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        foreach (Inputs input in inputs)
        {
            if (input.GetButton("Start", Inputs.ButtonType.DOWN, false))
            {
                setActive(!active);
            }
        }
    }
}

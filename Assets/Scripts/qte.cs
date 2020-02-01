using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class qte : MonoBehaviour
{
    public List<GameObject> imgs;

    public List<string> keys;

    public float time = 5.0f;
    public int score = 0;

    private bool isRunning;
    private string toPress;
    private Inputs currentInput;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject img in imgs)
        {
            setImage(false, img);
        }
        next(true);
        End();
    }

    private void setImage(bool active, GameObject obj)
    {
        var sprite = obj.GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        color.a = active ? 1.0f : 0.3f;
        sprite.color = color;
    }

    private void onKeyPressed(string key, GameObject pressed, bool valid)
    {
        score += valid ? 1 : -1;
        pressed.GetComponent<Animator>().SetTrigger("grossi");
    }

    private void next(bool startup = false)
    {
        if (!startup)
        {
            setImage(false, imgs[keys.IndexOf(toPress)]);
        }
        toPress = keys[Random.Range(0, keys.Count)];
        setImage(true, imgs[keys.IndexOf(toPress)]);
    }

    public void StartQTE(Inputs input)
    {
        isRunning = true;
        currentInput = input;
        score = 0;
        foreach(var i in imgs)
        {
            i.SetActive(true);
        }
    }

    public int End()
    {
        isRunning = false;
        foreach (var i in imgs)
        {
            i.SetActive(false);
        }

        int tmpScore = score;
        score = 0;
        return tmpScore;
    }

    void Update()
    {
        if (!isRunning) return;

        foreach (string key in keys)
        {
            if (currentInput.GetButton(key, Inputs.ButtonType.DOWN))
            {
                onKeyPressed(key, imgs[keys.IndexOf(key)], key == toPress);
                next();
            }
        }
    }
}

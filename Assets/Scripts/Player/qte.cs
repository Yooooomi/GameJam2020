using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class qte : MonoBehaviour
{
    public List<GameObject> imgs;
    public List<KeyCode> keys;

    public float time = 5.0f;
    public int score = 0;

    public delegate void EventHandler(int score);
    public event EventHandler OnQteEnd;

    private KeyCode toPress;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject img in imgs)
        {
            setImage(false, img);
        }
        next(true);
    }

    private void setImage(bool active, GameObject obj)
    {
        var sprite = obj.GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        color.a = active ? 1.0f : 0.3f;
        sprite.color = color;
    }

    private void onKeyPressed(KeyCode key, GameObject pressed, bool valid)
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

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            OnQteEnd(score);
            Destroy(gameObject);
        }
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                onKeyPressed(key, imgs[keys.IndexOf(key)], key == toPress);
                next();
            }
        }
    }
}

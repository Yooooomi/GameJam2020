using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public bool checkDeclarations = true;
    private int declaredOnScene;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (!checkDeclarations) return;
        declaredOnScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.activeSceneChanged += this.Check;
    }

    private void Check(Scene a, Scene b)
    {
        if (!checkDeclarations) return;
        if (b.buildIndex == declaredOnScene)
        {
            SceneManager.activeSceneChanged -= this.Check;
            Destroy(this.gameObject);
        }
    }
}

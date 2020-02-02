using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSplashText : MonoBehaviour
{
    public float rotationMax;
    public GameObject prefab;
    public static SpawnSplashText Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Spawn(string text, Vector3 position, bool randomRotation = true)
    {
        float randZ = randomRotation ? Random.Range(-rotationMax, rotationMax) : 0;

        GameObject splash = Instantiate(prefab, position, Quaternion.Euler(0, 0, randZ));
        splash.GetComponent<SplashText>().Init(text);
    }
}

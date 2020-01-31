using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public List<GameObject> borders;
    public Camera cam;

    private void Update()
    {
        float ratio = (float) Screen.width / (float) Screen.height;
        float sizeY = cam.orthographicSize;
        float sizeX = sizeY * ratio;

        borders[0].transform.position = new Vector3(0, sizeY);
        borders[1].transform.position = new Vector3(0, -sizeY);
        borders[2].transform.position = new Vector3(-sizeX, 0);
        borders[3].transform.position = new Vector3(sizeX, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public bool inverse;
    public float zOffset;
    public GameObject roadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    private float currentOffset;
    
    private float roadHeight;
    private int nbToSpawn;
    private int middle;
    private GlobalStats stats;

    public void Start()
    {
        stats = FindObjectOfType<GlobalStats>();
        nbToSpawn = 4; // TODO implement roadHeight
        middle = nbToSpawn - (nbToSpawn / 2);

        for (int i = 0; i < nbToSpawn; i++)
        {
            GameObject obj = Instantiate(roadPrefab);

            if (roadHeight == 0)
            {
                SpriteRenderer render = obj.GetComponent<SpriteRenderer>();
                roadHeight = render.size.y * render.transform.localScale.y;
                Debug.Log(roadHeight);
            }

            obj.transform.position = new Vector3(0, ((float) (i - middle)) * roadHeight, 0);
            roads.Add(obj);
        }
    }

    public void Update()
    {
        float sens = inverse ? -1 : 1;
        currentOffset += stats.mapSpeed * Time.deltaTime * sens;

        if (Mathf.Abs(currentOffset) > roadHeight)
        {
            currentOffset = 0;
        }

        for (int i = 0; i < roads.Count; i++)
        {
            roads[i].transform.position = Vector3.up * currentOffset + Vector3.up * (roadHeight * (i - middle)) + Vector3.forward * zOffset;
        }
    }
}

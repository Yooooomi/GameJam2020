using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [System.Serializable]
    public class TruckItem
    {
        public GameObject obj;
        public float cooldown = 0.5f;
        [HideInInspector] public float currentCooldown = 0;
    }

    public List<KeyCode> keys;
    public List<TruckItem> items;
    public GameObject itemContainer;

    private BoxCollider2D truckCollider;

    void Start()
    {
        truckCollider = GetComponent<BoxCollider2D>();    
    }

    void activeItem(TruckItem item)
    {
        GameObject container = Instantiate(itemContainer);
        container.transform.position = transform.position - new Vector3(0, truckCollider.size.y);
        Instantiate(item.obj, container.transform);
        item.currentCooldown = item.cooldown;
    }

    void Update()
    {
        foreach (TruckItem item in items)
        {
            item.currentCooldown -= Time.deltaTime;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            var key = keys[i];
            if (Input.GetKeyDown(key))
            {
                var item = items[i];
                if (item.currentCooldown <= 0)
                {
                    activeItem(item);
                }
            }
        }
    }
}

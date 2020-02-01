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
        public Animator animationOnTruck = null;
        [HideInInspector] public float currentCooldown = 0;
        [HideInInspector] public Activable activable = null;
    }

    public List<string> keys;
    public List<TruckItem> items;
    public GameObject itemContainer;

    private BoxCollider2D truckCollider;
    private Movement movement;

    void Start()
    {
        if (items.Count != keys.Count)
        {
            Debug.LogWarning("Invalid configuration of truck class");
        }

        movement = GetComponent<Movement>();
        truckCollider = GetComponent<BoxCollider2D>();    
    }

    void activeItem(TruckItem item)
    {
        GameObject container = Instantiate(itemContainer);
        container.transform.position = transform.position - new Vector3(0, truckCollider.size.y / 2) - new Vector3(0, item.obj.GetComponent<BoxCollider2D>().size.y / 2) - new Vector3(0, 0.1f);
        GameObject spawnedObject = Instantiate(item.obj, container.transform);
        item.activable = spawnedObject.GetComponent<Activable>();
        if (item.activable != null)
        {
            item.activable.onDelete.AddListener(() =>
            {
                item.activable = null;
            });
        }
        if (item.animationOnTruck != null)
        {
            item.animationOnTruck.SetTrigger("open");
        }
        item.currentCooldown = item.cooldown;
    }

    void Update()
    {

        foreach (TruckItem item in items)
        {
            item.currentCooldown -= Time.deltaTime;
        }

        if (movement.input == null) return;

        for (int i = 0; i < keys.Count; i++)
        {
            string key = keys[i];
            if (movement.input.GetButton(key, Inputs.ButtonType.DOWN))
            {
                var item = items[i];
                if (item.activable != null)
                {
                    item.activable.activate();
                    continue;
                } else if (item.currentCooldown <= 0)
                {
                    activeItem(item);
                }
            }
        }
    }
}

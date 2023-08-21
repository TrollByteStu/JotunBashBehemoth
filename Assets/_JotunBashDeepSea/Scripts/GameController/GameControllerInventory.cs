using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerInventory : MonoBehaviour
{
    public Dictionary<GameObject,int> inventory = new System.Collections.Generic.Dictionary<GameObject,int>();

    public GameObject[] debugPrefabs;
    public int[] debugAmounts;

    private int reuseIndex;

    public void ItemDecrease(GameObject prefab)
    {
        inventory[prefab]--;
        if (inventory[prefab] < 0) inventory[prefab] = 0;
    }

    public void itemAdd(GameObject prefab, int amount)
    {
        reuseIndex = ItemFindIndexOfPrefab(prefab);
        if (reuseIndex == -1)
        {
            inventory.Add(prefab, amount);
        } else {
            inventory[prefab] += amount;
        }
    }

    public int ItemHowMany(GameObject prefab)
    {
        reuseIndex = ItemFindIndexOfPrefab(prefab);
        if (reuseIndex > -1)
            return inventory[prefab];
        else return 0;
    }

    public int ItemFindIndexOfPrefab(GameObject prefab)
    {
        if (inventory.ContainsKey(prefab))
        {
            List<GameObject> keysList = new List<GameObject>(inventory.Keys);
            int index = keysList.IndexOf(prefab);
            return index;
        }
        else
        {
            return -1; // Prefab not found in the inventory
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.Count > 0)
        {
            debugPrefabs = new GameObject[inventory.Count];
            debugAmounts = new int[inventory.Count];
            int i = 0;
            foreach (KeyValuePair<GameObject, int> item in inventory)
            {
                debugPrefabs[i] = item.Key;
                debugAmounts[i++] = item.Value;
            }  
        }
    }
}

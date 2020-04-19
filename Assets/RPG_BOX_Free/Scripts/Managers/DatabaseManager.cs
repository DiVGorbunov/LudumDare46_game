using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour {

    public static List<FruitItem> Items = new List<FruitItem>(); 

    private void Awake()
    {
        //Items = new List<Item>(Resources.LoadAll<Item>("ItemsBases")); // Load all items
        Items = FruitManager.Instance.GetFruitItems();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour {

    public static List<FruitItem> Items = new List<FruitItem>(); 
    public static List<LootTable> LootTables = new List<LootTable>();
    public static List<Attribute> Attributes = new List<Attribute>();


    private void Awake()
    {
        //Items = new List<Item>(Resources.LoadAll<Item>("ItemsBases")); // Load all items
        Items = FruitManager.Instance.GetFruitItems();
        LootTables=new List<LootTable>(Resources.LoadAll<LootTable>("LootTables"));// Load all LootTables
        Attributes = new List<Attribute>(Resources.LoadAll<Attribute>("Attributes"));// Load all Attributes
    }
}

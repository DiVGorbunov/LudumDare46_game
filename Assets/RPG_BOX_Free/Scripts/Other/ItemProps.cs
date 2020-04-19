﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemProps : MonoBehaviour, IPointerClickHandler
{
    //Script for the item GameObject itself and how it manages all the commands.

    public FruitItem MyItem;
    public int MyPlaceInHome;
    public Image MyImage;
    Text CounterText;
    InventoryManager AccInv;
    FruitBucket bucket;

    public ItemHome MyHome;


    void Start()
    {
        //loads neccessary Managers
        if (GameObject.Find("FruitFactory") != null)
        {
            AccInv = GameObject.Find("FruitFactory").GetComponent<FruitFactory>();
        }
        if (GameObject.Find("Bucket") != null)
        {
            bucket = GameObject.Find("Bucket").GetComponent<FruitBucket>();
        }
    }



    public void TakeInfo(FruitItem TheItem, int ThePlaceInInventroy, ItemHome TheHome)//When the item is created, it takes these paramets to determine its function and to determine where it is placed (inventory or shop, etc)
    {
        MyItem = TheItem;
        MyImage = transform.Find("ItemSpriteIcon").GetComponent<Image>();
        CounterText = transform.Find("ItemCounter").GetComponent<Text>();
        CounterText.text = "";
        MyPlaceInHome = ThePlaceInInventroy;
        MyHome = TheHome;

        MyImage.sprite = TheItem.itemIcon;

    }

    public void ChangeStacks(int NumberOfStacks)
    {
        if (NumberOfStacks > 1)
        {
            CounterText.text = NumberOfStacks.ToString();
        }
        else
        {
            CounterText.text = "";
        }
    }

    public void OnPointerClick(PointerEventData eventData) // when the mouse is clicked
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            MouseClick();
        }

    }

    public void MouseClick()
    {

        if (
            MyHome == ItemHome.Inventory &&
            bucket.getInventoryCount() < bucket.Rows * bucket.Columns
        )
        {
            bucket.AddItemToInventory(MyItem);
            AccInv.RemoveItemFromInventory(MyPlaceInHome);

        }
        else if (MyHome == ItemHome.FruitBucket)
        {
            bucket.RemoveItemFromInventory(MyPlaceInHome);
        }

    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

}

public enum ItemHome
{
    Inventory = 0, PlayerBuyTab = 1, Dropped = 2, Equiped = 3, FruitBucket = 4
}

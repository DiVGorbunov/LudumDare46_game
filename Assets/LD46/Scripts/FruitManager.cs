﻿using System.Collections.Generic;
using UnityEngine;

public class FruitManager
{
    private Fruit[] _fruits;
    private IDictionary<Fruit, Sprite> _spriteDictionary;

    private static FruitManager _instance;
    public static FruitManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FruitManager();
            }
            return _instance;
        }
    }

    private FruitManager()
    {
        _fruits = (Fruit[])System.Enum.GetValues(typeof(Fruit));

        _spriteDictionary = new Dictionary<Fruit, Sprite>();
        var sprites = Resources.LoadAll<Sprite>("LD46/Fruits");
        foreach (var sprite in sprites)
        {
            for (int i = 0; i < _fruits.Length; i++)
            {
                if (string.Compare(sprite.name, _fruits[i].ToString(), true) == 0)
                {
                    _spriteDictionary.Add(_fruits[i], sprite);
                    break;
                }
            }
        }
    }

    public Fruit GetRandomFruit()
    {
        int index = Mathf.FloorToInt(Random.value * _fruits.Length);
        if (index == _fruits.Length)
        {
            index--;
        }

        return _fruits[index];
    }

    public Fruit[] GetRandomFruits(int number)
    {
        Fruit[] fruits = new Fruit[number];
        for (int i = 0; i < number; i++)
        {
            fruits[i] = GetRandomFruit();
        }
        return fruits;
    }

    public Sprite GetFruitSprite(Fruit fruit)
    {
        return _spriteDictionary[fruit];
    }

    public List<FruitItem> GetFruitItems()
    {
        var fruitItems = new List<FruitItem>();

        foreach (var item in _spriteDictionary)
        {
            fruitItems.Add(new FruitItem
            {
                itemIcon = item.Value
            });
        }

        return fruitItems;
    }
}

public class FruitItem
{
    public Sprite itemIcon;
}

public enum Fruit
{
    Banana,
    Orange,
    Avocado,
    Cookie,
    Cake
}
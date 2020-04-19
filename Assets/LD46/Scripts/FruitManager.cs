using System.Collections.Generic;
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

    public Sprite GetFruitSprite(Fruit fruit)
    {
        return _spriteDictionary[fruit];
    }
}

public enum Fruit
{
    Banana,
    Orange,
    Avocado,
    Cookie,
    Cake
}
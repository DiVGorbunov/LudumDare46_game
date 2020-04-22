using System.Collections.Generic;
using UnityEngine;

public class FruitManager
{
    private Fruit[] _fruits;
    private IDictionary<Fruit, Sprite> _spriteDictionary;
    private int _fruitTypes;

    private List<Fruit> _remainingFruits;
    private int[] _remainingFruitsCounts;
    private bool _isRemaningFruitsInitialized;

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

        _fruitTypes = _fruits.Length;
        _remainingFruitsCounts = new int[_fruits.Length];
    }

    public Fruit GetRandomFruit()
    {
        int index = Mathf.FloorToInt(Random.value * _fruitTypes);
        if (index == _fruitTypes)
        {
            index--;
        }

        return _fruits[index];
    }

    public Fruit GetRandomRemainingFruitItem()
    {
        if (!_isRemaningFruitsInitialized)
        {
            _remainingFruits = new List<Fruit>();
            for (int i = 0; i < _remainingFruitsCounts.Length; i++)
            {
                if (_remainingFruitsCounts[i] > 0)
                {
                    _remainingFruits.Add((Fruit)i);
                }
            }
        }

        return _remainingFruits[Random.Range(0, _remainingFruits.Count)];
    }

    public Fruit[] GetRandomFruits(int number)
    {
        Fruit[] fruits = new Fruit[number];
        for (int i = 0; i < number; i++)
        {
            fruits[i] = GetRandomFruit();
            _remainingFruitsCounts[(int)fruits[i]]++;
        }
        return fruits;
    }

    public Sprite GetFruitSprite(Fruit fruit)
    {
        return _spriteDictionary[fruit];
    }

    public FruitItem GetFruitItem(Fruit fruit)
    {
        return new FruitItem
        {
            itemType = fruit,
            itemIcon = _spriteDictionary[fruit]
        };
    }

    public List<FruitItem> GetFruitItems()
    {
        var fruitItems = new List<FruitItem>();

        foreach (var item in _spriteDictionary)
        {
            fruitItems.Add(new FruitItem
            {
                itemType = item.Key,
                itemIcon = item.Value
            });
        }

        return fruitItems;
    }

    public void SetFruitTypes(int fruitTypes)
    {
        if (fruitTypes > 0 && fruitTypes <= _fruits.Length)
        {
            _fruitTypes = fruitTypes;
            for (int i = 0; i < _fruits.Length; i++)
            {
                _remainingFruitsCounts[i] = 0;
            }
            _isRemaningFruitsInitialized = false;
        }
    }

    public void DropRemainingFruits(Fruit[] fruits)
    {
        foreach (var fruit in fruits)
        {
            _remainingFruitsCounts[(int)fruit]--;
            if (_remainingFruitsCounts[(int)fruit] == 0)
            {
                _remainingFruits.Remove(fruit);
            }
        }
    }
}

public class FruitItem
{
    public Sprite itemIcon;
    public Fruit itemType;
}

public enum Fruit
{
    Apple,
    Banana,
    Cake,
    Cherries,
    Drink,
    Orange,
    Pizza,
    Watermelon
}
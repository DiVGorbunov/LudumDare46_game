using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClientController : MonoBehaviour
{
    [Tooltip("Number of seconds to live")]
    public float secondsToLive = 10f;

    [Tooltip("Seconds delta between damage")]
    public float delta = 1f;

    private IEnumerator _coroutine;
    private Health _objectHealth;

    public Fruit[] Fruits { get; private set; }

    public float[] fruitNumberProbabilities = { 0.34f, 0.33f, 033f };
    public UnityAction onSatisfy;
    public UnityAction onUnsatisfy;

    void Start()
    {
        _objectHealth = GetComponent<Health>();
        _objectHealth.onDie += () =>
        {
            if (onUnsatisfy != null)
            {
                onUnsatisfy.Invoke();
            }
            Destroy(gameObject);
        };

        EnsureFruits();

        _coroutine = WaitAndDamage(_objectHealth.currentHealth / secondsToLive);
        StartCoroutine(_coroutine);
    }

    private void EnsureFruits()
    {
        if (Fruits != null)
        {
            return;
        }

        var fruitController = GetComponentInChildren<CanvasFruitController>();
        if (fruitController != null)
        {
            int fruitNumber = GetFruitNumber();
            Fruits = FruitManager.Instance.GetRandomFruits(fruitNumber);
            fruitController.Generate(Fruits);
        }
    }

    private int GetFruitNumber()
    {
        var random = Random.value;
        for (int i = 0; i < fruitNumberProbabilities.Length; i++)
        {
            if (random <= fruitNumberProbabilities[i])
            {
                return i + 1;
            }
            random -= fruitNumberProbabilities[i];
        }
        return fruitNumberProbabilities.Length;
    }

    public void RandomizeHealth()
    {
        SetHealth(Random.value * secondsToLive);
    }

    public void SetHealth(float seconds)
    {
        var controller = GetComponent<ClientController>();
        if (controller != null)
        {
            controller.secondsToLive = seconds;
        }
    }

    public bool CheckRequirement(Fruit[] cartItems)
    {
        if (cartItems.Length == 0) return false;
        List<Fruit> listCheck = new List<Fruit>(Fruits);

        foreach (var cartItem in cartItems)
        {
            listCheck.Remove(cartItem);
        }

        if (listCheck.Count == 0)
        {
            if (onSatisfy != null)
            {
                onSatisfy.Invoke();
            }

            Destroy(this.gameObject);
            return true;
        }

        return false;
    }

    // Update is called once per frame
    IEnumerator WaitAndDamage(float damage)
    {
        while (!_objectHealth.IsDead)
        {
            _objectHealth.TakeDamage(damage, gameObject);
            yield return new WaitForSeconds(delta);
        }

        yield break;
    }
}
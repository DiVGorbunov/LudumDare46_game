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
            int fruitNumber = Mathf.RoundToInt(Random.value * 2f);
            Fruits = FruitManager.Instance.GetRandomFruits(fruitNumber + 1);
            fruitController.Generate(Fruits);
        }
    }

    public void RandomizeHealth()
    {
        var controller = GetComponent<ClientController>();
        if (controller != null)
        {
            controller.secondsToLive += Random.value * secondsToLive;
        }
    }

    public void CheckRequirement(List<Fruit> cartItems)
    {
        if (cartItems.Count == 0) return; 
        List<Fruit> listCheck = new List<Fruit>(Fruits);

        foreach (var cartItem in cartItems)
        {
            listCheck.Remove(cartItem);
        }

        if (listCheck.Count == 0)
        {
            Destroy(this.gameObject);
        }
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
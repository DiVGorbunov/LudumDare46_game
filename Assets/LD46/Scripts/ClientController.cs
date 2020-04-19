using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    [Tooltip("Number of seconds to live")]
    public float secondsToLive = 10f;

    [Tooltip("Seconds delta between damage")]
    public float delta = 1f;

    private IEnumerator _coroutine;
    private Health _objectHealth;

    public List<int> items = null;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<int> { 1, 2, 3, 4 };

        _objectHealth = gameObject.GetComponent<Health>();
        _coroutine = WaitAndDamage(_objectHealth.currentHealth / secondsToLive);
        StartCoroutine(_coroutine);
    }

    public void CheckRequirement(List<int> cartItems)
    {
        if (cartItems.Count == 0) return; 
        List<int> listCheck = new List<int>(items);
        bool supplied = true;
        for (int i = 0; i < cartItems.Count; i++)
        {
            if (listCheck.Contains(cartItems[i]))
            {
                listCheck.Remove(cartItems[i]);
            }
            else
            {
                supplied = false;
                break;
            }
        }

        if (supplied)
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
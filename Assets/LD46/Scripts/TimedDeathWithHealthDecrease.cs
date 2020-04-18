using System.Collections;
using UnityEngine;

public class TimedDeathWithHealthDecrease : MonoBehaviour
{
    [Tooltip("Number of seconds to live")]
    public float secondsToLive = 10f;

    [Tooltip("Seconds delta between damage")]
    public float delta = 1f;

    private IEnumerator _coroutine;
    private Health _objectHealth;

    // Start is called before the first frame update
    void Start()
    {
        _objectHealth = gameObject.GetComponent<Health>();
        _coroutine = WaitAndDamage(_objectHealth.currentHealth / secondsToLive);
        StartCoroutine(_coroutine);
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
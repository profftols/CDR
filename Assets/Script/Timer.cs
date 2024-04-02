using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    private Coroutine _counter;
    private TextMeshProUGUI _textDisplay;

    private float _delay = 0.5f;
    private int number;

    private void Awake()
    {
        _textDisplay = GetComponent<TextMeshProUGUI>();
    }

    public void ManageTimer()
    {
        if (_counter == null)
        {
            _counter = StartCoroutine(TimerCoroutine());
        }
        else
        {
            StopCoroutine(_counter);
            _counter = null;
        }
    }

    private IEnumerator TimerCoroutine()
    {
        var wait = new WaitForSeconds(_delay);

        while (true)
        {
            number++;
            _textDisplay.text = number.ToString();
            yield return wait;
        }
    }
}
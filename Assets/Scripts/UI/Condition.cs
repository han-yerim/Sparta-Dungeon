using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    [Header("Condition Values")]
    public float startValue = 100f;
    public float maxValue = 100f;
    public float curValue;

    [Header("Passive Stats")]
    public float passiveValue;

    [Header("UI")]
    public Image uiBar;

    private void Start()
    {
        curValue = Mathf.Clamp(startValue, 0, maxValue);
        UpdateBar();
    }

    private void Update()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        if (uiBar != null)
        {
            uiBar.fillAmount = curValue / maxValue;
        }
    }

    public void Add(float value)
    {
        curValue += value;
        curValue = Mathf.Clamp(curValue, 0, maxValue);
        UpdateBar();
    }

    public void Subtract(float value)
    {
        curValue -= value;
        curValue = Mathf.Clamp(curValue, 0, maxValue);
        UpdateBar();
    }
}

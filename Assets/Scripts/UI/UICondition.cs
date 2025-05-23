using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;

    void Awake()
    {
        if (health == null)
        {
            health = new Condition();
        }

        health.Set(health.maxValue); // 체력 초기화
    }
}
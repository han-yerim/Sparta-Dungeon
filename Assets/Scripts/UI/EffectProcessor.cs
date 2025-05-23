using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectProcessor : MonoBehaviour
{
    public static void ApplyEffect(GameObject player, ConsumableType type, float value, float duration)
    {
        var controller = player.GetComponent<PlayerController>();
        var condition = player.GetComponent<PlayerCondition>();

        switch (type)
        {
            case ConsumableType.Health:
                condition?.Heal(value);
                break;

            case ConsumableType.Speed:
                controller?.ApplySpeedBuff(value, duration);
                break;

            case ConsumableType.Jump:
                controller?.ApplyJumpBuff(value, duration);
                break;
        }
    }
}

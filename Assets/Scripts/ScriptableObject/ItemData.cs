using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Resource
}

public enum ConsumableType
{
    Health,
    Speed,
    Jump
}

[Serializable]
public class ItemDataConsumbale
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]

public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;


    [Header("Consumable")]
    public ItemDataConsumbale[] consumables;
    public ConsumableType effectType;
    public float effectValue;
    public float duration;
}

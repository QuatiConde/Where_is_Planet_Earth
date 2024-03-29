using UnityEngine;
using UnityEngine.Events;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [Header("Tracking Values")]
    public Resources stacks;

    //Events
    public static UnityEvent<ResourceType, int> OnAddResource = new();
    public static UnityEvent<ResourceType, int> OnRemoveResource = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void AddToStack(ResourceType type, int amount)
    {
        OnAddResource?.Invoke(type, amount);

        switch (type)
        {
            case ResourceType.Water:
                stacks.water += amount;
                break;
            case ResourceType.Uranium:
                stacks.uranium += amount;
                break;
            case ResourceType.Titanium:
                stacks.titanium += amount;
                break;
            case ResourceType.Seed:
                stacks.seed += amount;
                break;
            case ResourceType.Fertilizer:
                stacks.fertilizer += amount;
                break;
        }
    }

    public void RemoveFromStack(ResourceType type, int amount)
    {
        OnRemoveResource?.Invoke(type, amount);

        switch (type)
        {
            case ResourceType.Water:
                stacks.water -= amount;
                break;
            case ResourceType.Uranium:
                stacks.uranium -= amount;
                break;
            case ResourceType.Titanium:
                stacks.titanium -= amount;
                break;
            case ResourceType.Seed:
                stacks.seed -= amount;
                break;
            case ResourceType.Fertilizer:
                stacks.fertilizer -= amount;
                break;
        }
    }
}

[Serializable]
public class Resources
{
    public int water;
    public int uranium;
    public int titanium;
    public int seed;
    public int fertilizer;
}
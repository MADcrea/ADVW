using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager_script : MonoBehaviour
{
    public enum ResourceType
    {
        Money,
        Steel,
        Brick,
        Energy
    }
    private int money, steel, brick, energy;

    private int restMoney, restSteel, restBrick, restEnergy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCost(ResourceType Resource)
    {
        switch(Resource)
        {
            case ResourceType.Energy:
                return energy;
            case ResourceType.Money:
                return money;
            case ResourceType.Brick:
                return brick;
            case ResourceType.Steel:
                return steel;
            default:
                return -1;
        }
    }

    public int getRestCost(ResourceType Resource)
    {
        switch(Resource)
        {
            case ResourceType.Energy:
                return restEnergy;
            case ResourceType.Money:
                return restMoney;
            case ResourceType.Brick:
                return restBrick;
            case ResourceType.Steel:
                return restSteel;
            default:
                return -1;
        }
    }


}

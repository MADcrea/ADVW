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
    private int costMoney, costSteel, costBrick, costEnergy;

    private int restMoney, restSteel, restBrick, restEnergy;

    // Start is called before the first frame update
    void Start()
    {
        restSteel =costSteel;
        restEnergy = costEnergy;
        restBrick = costBrick;
        restMoney = costMoney;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpCost(int CardCostSteel, int CardCostEnergy,int CardCostBrick,int CardCostMoney)
    {
        costSteel=CardCostSteel;
        costEnergy =CardCostEnergy;
        costBrick = CardCostBrick;
        costMoney = CardCostMoney;
    }
    public int getCost(ResourceType Resource)
    {
        switch(Resource)
        {
            case ResourceType.Energy:
                return costEnergy ;
            case ResourceType.Money:
                return costMoney;
            case ResourceType.Brick:
                return costBrick;
            case ResourceType.Steel:
                return costSteel;
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

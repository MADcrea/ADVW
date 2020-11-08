using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour
{
    public float Energy_stock;
    public float Energy_prod;
    public float Steel_stock;
    public float Steel_prod;
    public float Brick_stock;
    public float Brick_prod;
    public float Money_stock;
    public float Money_prod;
    public float PV;

    public List<Card_script> List_cards;
    // Start is called before the first frame update
    void Start()
    {
        //Stock
        Energy_stock=10;
        Steel_stock =10;
        Brick_stock =10;
        Money_stock =10;

        //Production
        Energy_prod= 1;
        Steel_prod = 2;
        Brick_prod = 1;
        Money_prod = 2;

        //Victory points
        PV=0;

        List_cards = new List<Card_script>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Turn_production()
    {
        Energy_stock = Energy_prod;
        Steel_stock = Steel_prod;
        Brick_stock = Brick_prod;
        Money_stock = Money_prod;

    }
    public void E_prod_change(float E_P_Change)
    {
        Energy_prod = Energy_prod +E_P_Change;
    }
    public void S_prod_change(float S_P_Change)
    {
        Steel_prod = Steel_prod +S_P_Change;
    }
    public void B_prod_change(float B_P_Change)
    {
        Brick_prod = Brick_prod +B_P_Change;
    }
    public void M_prod_change(float M_P_Change)
    {
        Money_prod = Money_prod +M_P_Change;
    }
    public void E_stock_change (float E_S_change)
    {
        Energy_stock =Energy_stock +E_S_change;
    }
    public void S_stock_change (float S_S_change)
    {
        Steel_stock=Steel_stock +S_S_change;
    }
    public void B_stock_change (float B_S_change)
    {
        Brick_stock =Brick_stock +B_S_change;
    }
    public void M_stock_change (float M_S_change)
    {
        Money_stock = Money_stock +M_S_change;
    }
    public void PV_stock_change(float PV_change)
    {
        PV=PV+PV_change;
    }

}

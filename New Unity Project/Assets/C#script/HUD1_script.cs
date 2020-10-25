using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD1_script : MonoBehaviour
{
    //Link with UI manager
    public GameObject UI_Man;
    public UI_Manager_script UI_values;
    public CanvasGroup Display_status;

    //Player fond stock and production
    public Text S_P; public Text E_P; public Text M_P; public Text D_P;
    public Text S_S; public Text E_S; public Text M_S; public Text D_S;

    //Information
    public Text Intel;

    //Players
    public GameObject P1;public GameObject P2;public GameObject P3;public GameObject P4;
    public Player_script Values_P1;public Player_script Values_P2;
    public Player_script Values_P3;public Player_script Values_P4;

    //Case selection to deploy unit and building
    public string Selection; //Name of selected case

    // Start is called before the first frame update
    void Start()
    {
        //Link to UI
        UI_Manager_script UI_values = UI_Man.GetComponent<UI_Manager_script>();
        Display_status = GetComponent<CanvasGroup>();

        //Link to players
        Player_script Values_P1 = P1.GetComponent<Player_script>();
        Player_script Values_P2 = P2.GetComponent<Player_script>();
        Player_script Values_P3 = P3.GetComponent<Player_script>();
        Player_script Values_P4 = P4.GetComponent<Player_script>();
        
        //Set-up Phase 1
        Selection = "";
        Intel.text="";
      
    }

    // Update is called once per frame
    void Update()
    {
        if(UI_values.Player_turn == "P1")
        {
            S_P.text=Values_P1.Steel_prod.ToString(); S_S.text = Values_P1.Steel_stock.ToString();
            E_P.text=Values_P1.Energy_prod.ToString(); E_S.text=Values_P1.Energy_stock.ToString();
            M_P.text=Values_P1.Brick_prod.ToString(); M_S.text=Values_P1.Brick_stock.ToString();
            D_P.text=Values_P1.Money_prod.ToString(); D_S.text=Values_P1.Money_stock.ToString();
        }
        if(UI_values.Player_turn == "P2")
        {
            S_P.text=Values_P2.Steel_prod.ToString(); S_S.text = Values_P2.Steel_stock.ToString();
            E_P.text=Values_P2.Energy_prod.ToString(); E_S.text=Values_P2.Energy_stock.ToString();
            M_P.text=Values_P2.Brick_prod.ToString(); M_S.text=Values_P2.Brick_stock.ToString();
            D_P.text=Values_P2.Money_prod.ToString(); D_S.text=Values_P2.Money_stock.ToString();
        }
        if(UI_values.Player_turn == "P3")
        {
            S_P.text=Values_P3.Steel_prod.ToString(); S_S.text = Values_P3.Steel_stock.ToString();
            E_P.text=Values_P3.Energy_prod.ToString(); E_S.text=Values_P3.Energy_stock.ToString();
            M_P.text=Values_P3.Brick_prod.ToString(); M_S.text=Values_P3.Brick_stock.ToString();
            D_P.text=Values_P3.Money_prod.ToString(); D_S.text=Values_P3.Money_stock.ToString();
        }
        if(UI_values.Player_turn == "P4")
        {
            S_P.text=Values_P4.Steel_prod.ToString(); S_S.text = Values_P4.Steel_stock.ToString();
            E_P.text=Values_P4.Energy_prod.ToString(); E_S.text=Values_P4.Energy_stock.ToString();
            M_P.text=Values_P4.Brick_prod.ToString(); M_S.text=Values_P4.Brick_stock.ToString();
            D_P.text=Values_P4.Money_prod.ToString(); D_S.text=Values_P4.Money_stock.ToString();
        }
        if(UI_values.Phase_number!= 1)
        {
            Display_status.alpha=0;
            Display_status.interactable=true;
            Display_status.blocksRaycasts=true;
        }
        else
        {
            Display_status.alpha=1;
            Display_status.interactable=false;
            Display_status.blocksRaycasts=false;
        }
       }
    public void BT_ADD_Rifle(int a){
        //player must have enough ressources: 1 Steel and 1 Devise
        if(float.Parse(S_S.text) > 0 && float.Parse(D_S.text) > 0)
        {
            //=>Update information message
            Intel.text="Rifle can be created, where do you want it to be deployed?";
            UI_values.Step = "Case selection for unit";
        }
        else
        {
            //=>Update information message
            Intel.text="You don't have enough ressources to deploy a new unit.";
        }

    }
    public void BT_ADD_Bazooka(int a){}
    public void BT_ADD_Jeep(int a){}
    public void BT_ADD_Tank(int a){}
    public void BT_ADD_Artillery(int a){}

    public void Is_selected_case_ok()
    {
        /*
        Unit
        Deploy location must be :
        -free of occupation
        -next to an activated building of the same player

        Building
        Deploy location must be :
        -free of occupation
        -next to an activated unit of the same player
        -no adjacent building from any player
        */
    }
}

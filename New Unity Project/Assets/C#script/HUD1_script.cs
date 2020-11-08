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
    public Player_script PlayerToPlay;

    //Unit about to be deploy
    public Unit_script.UnitType Unit_To_Be_Deployed;
    public GameObject UnitPrefab;
    public int UnitDeployed;
    

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
        UnitDeployed =0;
        Unit_To_Be_Deployed = Unit_script.UnitType.Default;
        Intel.text="";
      
    }
    public void WhosGonnaPlay()
    {
        switch(UI_values.Player_turn)
        {
            case 0:
                break; //Game have not started    
            case 1:
                AffectPlayer(Values_P1);
                break;
            case 2:
                AffectPlayer(Values_P2);
                break;
            case 3:
                AffectPlayer(Values_P3);
                break;
            case 4:
                AffectPlayer(Values_P4);
                break;
        }
    }
    public void AffectPlayer(Player_script Player)
    {
        PlayerToPlay = Player;
    }
    // Update is called once per frame
    void Update()
    {
        //Display ressources and production of Player
        WhosGonnaPlay();
        S_P.text=PlayerToPlay.Steel_prod.ToString(); S_S.text = PlayerToPlay.Steel_stock.ToString();
        E_P.text=PlayerToPlay.Energy_prod.ToString(); E_S.text=PlayerToPlay.Energy_stock.ToString();
        M_P.text=PlayerToPlay.Brick_prod.ToString(); M_S.text=PlayerToPlay.Brick_stock.ToString();
        D_P.text=PlayerToPlay.Money_prod.ToString(); D_S.text=PlayerToPlay.Money_stock.ToString();
        
        if(UI_values.Phase_number!= 1)
        {
            Display_status.alpha=0;
            Display_status.interactable=false;
            Display_status.blocksRaycasts=false;
        }
        else
        {
            Display_status.alpha=1;
            Display_status.interactable=true;
            Display_status.blocksRaycasts=true;
        }
       }
    public void Unit_Deployment(GameObject Deployment_Case)
    {
        if(IsThisTerrainAcceptableForSuchUnit(Deployment_Case))
        {
            //GameObject Selected_case => Case_script
                Case_script Deployment_zone = Deployment_Case.GetComponent<Case_script>();
            //Unit_creation
                GameObject NewUnit= (GameObject) Instantiate (UnitPrefab);
                Unit_script NewUnitValue =  NewUnit.GetComponent<Unit_script>();
            //Unit Set_up
                //GameObject
                NewUnit.name = "UNIT_" + UnitDeployed;
                NewUnit.transform.position = Deployment_Case.transform.position + new Vector3(0,0.2f,0);
                //Unit values
                NewUnitValue.Owner = UI_values.Player_turn;
                NewUnitValue.Occupied_case = Deployment_zone;
                NewUnitValue.Set_up_unit_values(Unit_To_Be_Deployed);
                //Color
                NewUnitValue.Change_Color();
            
            //Case update
                Deployment_zone.OccupingUnit = NewUnitValue;
                Deployment_zone.Occupation = Case_script.OccupationType.Unit;

            //Player Update
                //Ressources
                Ressources_Payment(Unit_To_Be_Deployed);
            
            //Miscellaneous
                UnitDeployed++;
                UI_values.Step=UI_Manager_script.StepType.none;
                Intel.text ="P"+UI_values.Player_turn+" created "+Unit_To_Be_Deployed;
        }
        else
        {
            Intel.text = "Terrain not suitable for "+Unit_To_Be_Deployed.ToString();
        }
    }
private bool IsThisTerrainAcceptableForSuchUnit(GameObject Deployment_Case)
    {
        bool returnValue = true;
        Material CaseTerrain=Deployment_Case.GetComponent<Case_script>().Terrain;
        switch (Unit_To_Be_Deployed)
        {
            case Unit_script.UnitType.Jeep:
                if (CaseTerrain.name == "Rivière")
                {
                    returnValue =false;
                }
                break;
            case Unit_script.UnitType.Tank:
                if (CaseTerrain.name == "Montagne")
                {
                    returnValue =false;
                }
                break;
            case Unit_script.UnitType.Artillery:
                if (CaseTerrain.name == "Montagne")
                {
                    returnValue =false;
                }
                break;
        }
        return returnValue;
    }
    public void Ressources_Payment(Unit_script.UnitType type)
    {
         switch (type)
        {
            case Unit_script.UnitType.Rifle:
                PlayerToPlay.S_stock_change(-1f);
                PlayerToPlay.E_stock_change(0f);
                PlayerToPlay.B_stock_change(0f);
                PlayerToPlay.M_stock_change(-1f);
                break;
            case Unit_script.UnitType.Bazooka:
                PlayerToPlay.S_stock_change(-1f);
                PlayerToPlay.E_stock_change(-1f);
                PlayerToPlay.B_stock_change(0f);
                PlayerToPlay.M_stock_change(-2f);
                break;
            case Unit_script.UnitType.Jeep:
                PlayerToPlay.S_stock_change(-2f);
                PlayerToPlay.E_stock_change(-1f);
                PlayerToPlay.B_stock_change(0f);
                PlayerToPlay.M_stock_change(-1f);
                break;
            case Unit_script.UnitType.Tank:
                PlayerToPlay.S_stock_change(-3f);
                PlayerToPlay.E_stock_change(-1f);
                PlayerToPlay.B_stock_change(0f);
                PlayerToPlay.M_stock_change(-2f);
                break;
            case Unit_script.UnitType.Artillery:
                PlayerToPlay.S_stock_change(-2f);
                PlayerToPlay.E_stock_change(-1f);
                PlayerToPlay.B_stock_change(0f);
                PlayerToPlay.M_stock_change(-2f);
                break;
        }
    }
    public void BT_ADD_Rifle(int a){
        //player must have enough ressources: 1 Steel and 1 Devise
        if(PlayerToPlay.Energy_stock > -1   && PlayerToPlay.Steel_stock >0 
        && PlayerToPlay.Brick_stock > -1    && PlayerToPlay.Money_stock >0)
        {
            //=>Update information message
            Intel.text="Rifle can be created, where do you want it to be deployed?";
            UI_values.Step = UI_Manager_script.StepType.Token_creation_case;
            Unit_To_Be_Deployed =  Unit_script.UnitType.Rifle;
        }
        else
        {
            //=>Update information message
            Intel.text="You don't have enough ressources to deploy a new unit.";
            Unit_To_Be_Deployed =  Unit_script.UnitType.Default;
        }

    }
    public void BT_ADD_Bazooka(int a)
    {
        //player must have enough ressources: 1 Steel, 1 Energy and 2 Devise
        if(PlayerToPlay.Energy_stock > 0   && PlayerToPlay.Steel_stock >0 
        && PlayerToPlay.Brick_stock > -1    && PlayerToPlay.Money_stock >1)
        {
            //=>Update information message
            Intel.text="Bazooka can be created, where do you want it to be deployed?";
            UI_values.Step = UI_Manager_script.StepType.Token_creation_case;
            Unit_To_Be_Deployed =  Unit_script.UnitType.Bazooka;
        }
        else
        {
            //=>Update information message
            Intel.text="You don't have enough ressources to deploy a new unit.";
            Unit_To_Be_Deployed =  Unit_script.UnitType.Default;
        }
    }
    public void BT_ADD_Jeep(int a)
    {
        //player must have enough ressources: 2 Steel, 1 Energy and 1 Devise
        if(PlayerToPlay.Energy_stock > 0   && PlayerToPlay.Steel_stock >1 
        && PlayerToPlay.Brick_stock > -1    && PlayerToPlay.Money_stock >0)
        {
            //=>Update information message
            Intel.text="Jeep can be created, where do you want it to be deployed?";
            UI_values.Step = UI_Manager_script.StepType.Token_creation_case;
            Unit_To_Be_Deployed =  Unit_script.UnitType.Jeep;
        }
        else
        {
            //=>Update information message
            Intel.text="You don't have enough ressources to deploy a new unit.";
            Unit_To_Be_Deployed =  Unit_script.UnitType.Default;
        }
    }
    public void BT_ADD_Tank(int a)
    {
        //player must have enough ressources: 3 Steel, 1 Energy and 2 Devise
        if(PlayerToPlay.Energy_stock > 0   && PlayerToPlay.Steel_stock >2 
        && PlayerToPlay.Brick_stock > -1    && PlayerToPlay.Money_stock >1)
        {
            //=>Update information message
            Intel.text="Tank can be created, where do you want it to be deployed?";
            UI_values.Step = UI_Manager_script.StepType.Token_creation_case;
            Unit_To_Be_Deployed =  Unit_script.UnitType.Tank;
        }
        else
        {
            //=>Update information message
            Intel.text="You don't have enough ressources to deploy a new unit.";
            Unit_To_Be_Deployed =  Unit_script.UnitType.Default;
        }
    }
    public void BT_ADD_Artillery(int a)
    {
         //player must have enough ressources: 2 Steel, 1 Energy and 2 Devise
        if(PlayerToPlay.Energy_stock > 0   && PlayerToPlay.Steel_stock >1 
        && PlayerToPlay.Brick_stock > -1    && PlayerToPlay.Money_stock >1)
        {
            //=>Update information message
            Intel.text="Artillery can be created, where do you want it to be deployed?";
            UI_values.Step = UI_Manager_script.StepType.Token_creation_case;
            Unit_To_Be_Deployed =  Unit_script.UnitType.Artillery;
        }
        else
        {
            //=>Update information message
            Intel.text="You don't have enough ressources to deploy a new unit.";
            Unit_To_Be_Deployed =  Unit_script.UnitType.Default;
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD2_display : MonoBehaviour
{
    public GameObject UI_Man;       public UI_Manager_script UI_values;
    public GameObject PH2_Intel;    public CanvasGroup Intel_UI;
    public Image Image_icon; public Text TYPE_text; public Text HV_text; public Text HL_text;
    public Text AV_text; public Text AL_text; public Text MV_text; public Text ML_text; public Text Player_text;
    public GameObject PH2_Attack;   public CanvasGroup Attack_UI;
    public GameObject PH2_Move;     public CanvasGroup Move_UI;
    public GameObject PH2_SD;       public CanvasGroup SD_UI;
    public GameObject ElementA;
    public GameObject ElementB;

    // Start is called before the first frame update
    void Start()
    { 
        //Link to UI manager
        UI_Man = GameObject.Find("UI_Manager");
        UI_Manager_script UI_values = UI_Man.GetComponent<UI_Manager_script>();

        //Link to UI canvas to display/hide
        Intel_UI = PH2_Intel.GetComponent<CanvasGroup>();
        Attack_UI = PH2_Attack.GetComponent<CanvasGroup>();
        Move_UI = PH2_Move.GetComponent<CanvasGroup>();
        SD_UI = PH2_SD.GetComponent<CanvasGroup>();

        ElementA = null;
        ElementB = null;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public bool HideShowHUD(CanvasGroup HUDtoHideorShow)
    {
        if (HUDtoHideorShow.alpha == 0){
             HUDtoHideorShow.alpha = 1;
             HUDtoHideorShow.interactable = true;
             HUDtoHideorShow.blocksRaycasts = true;
            return true;
        }
        else
        {
             HUDtoHideorShow.alpha = 0;
             HUDtoHideorShow.interactable = false;
             HUDtoHideorShow.blocksRaycasts =false ;
             return false;
        }  
    }
    //Intel Methods
    public void Intel_display (int a)
    {
        if(HideShowHUD(Intel_UI))
        {
            UI_values.Step=UI_Manager_script.StepType.Intel;
        }
        else
        {
            Image_icon.material = Resources.Load<Material>("Textures/UI_Blank");
            TYPE_text.text = "";
            HV_text.text =  "";
            HL_text.text =  "";
            AV_text.text =  "";
            AL_text.text=  "";
            MV_text.text=  "";
            ML_text.text=  "";
            Player_text.text=  "";
            UI_values.Step = UI_Manager_script.StepType.none;
        }  
    }
    //Attack Methods
    public void Attack_display (int a)
    {
        if(HideShowHUD(Attack_UI))
        {
            UI_values.Step=UI_Manager_script.StepType.Attack_A;
        }
        else
        {
            Attack_cancel(1);
        } 
    }
    public void Attack_cancel (int a)
    {
        //Mise en blanc de tous les champs

        //Fermeture UI_Attack
        HideShowHUD(Attack_UI);
        UI_values.Step =UI_Manager_script.StepType.none;
        ElementA = null;
        ElementB = null;
    }
    public void Attack_Perform (int a)
    {
        if(UI_values.Step ==UI_Manager_script.StepType.Attack_C)
        {
            //Random function
            float random_value =0f;
            //Success value
            float success_value =0f;

            GameObject Attacker =ElementA;
            GameObject Attacked =ElementB;

            if( ElementA.GetComponent("Unit_script") as Unit_script!=null)
            {
                Unit_script Attacker_values = Attacker.GetComponent<Unit_script>();
                //Ammo stock reduction
                Attacker_values.Change_Ammo_stock(-1f);
            }
            else
            {         
                Batiment_script Attacker_values = Attacker.GetComponent<Batiment_script>();
                //Ammo stock reduction
                Attacker_values.Change_Ammo_stock(-1f);
            }
        
            if(ElementB.GetComponent("Unit_script")!=null)
            {    
                Unit_script Attacked_values = Attacked.GetComponent<Unit_script>();
                //Success? => loose health
                if(random_value >= success_value)
                {
                    Attacked_values.Change_Health_stock(-1f);
                }
            }
            else
            {
                Batiment_script Attacked_values = Attacked.GetComponent<Batiment_script>();
                //Success? => loose health
                if(random_value >= success_value)
                {
                    Attacked_values.Change_Health_stock(-1f);
                }
            }
        
            Attack_cancel(1);
        }
    }
    //Move Methods
    public void Move_display (int a)
    {
        if(HideShowHUD(Move_UI))
        {
            UI_values.Step=UI_Manager_script.StepType.Move_A;
        }
        else
        {
            Move_Cancel(1);
        }   
    }
    public void Move_Cancel (int a)
    {
        // Mise en blanc de tous les champs
        
        //Fermeture UI_Move
        HideShowHUD(Move_UI);
        UI_values.Step =UI_Manager_script.StepType.none;
        ElementA = null;
        ElementB =null;
    }
    public void Move_Perform (int a)
    {if(UI_values.Step==UI_Manager_script.StepType.Move_C)
    {
        GameObject Voyager =ElementA;
        Unit_script Voyager_values = Voyager.GetComponent<Unit_script>();
        GameObject destination =ElementB;
        Case_script destination_values = destination.GetComponent<Case_script>();

        Voyager.transform.position =destination.transform.position + new Vector3(0,0.2f,0);
        destination_values.Occupation =Case_script.OccupationType.Unit;
        destination_values.Owning_control =Voyager_values.Owner;
        Voyager_values.Change_Mvt_stock(-1f);
        
        Move_Cancel(1);
    }
    }
    //Self destruct Methods
    public void SD_display (int a)
    {
        if(HideShowHUD(Move_UI))
        {
            UI_values.Step=UI_Manager_script.StepType.SD_A;
        }
        else
        {
            SD_cancel(1);
        }
    }
    public void SD_cancel (int a)
    {
        //Mise en blac de tous les champs
        

        //Fermeture UI_SD
        HideShowHUD(SD_UI);
        UI_values.Step =UI_Manager_script.StepType.none;
        ElementA = null;

    }
    public void SD_Perform (int a)
    {
        if(UI_values.Step==UI_Manager_script.StepType.SD_B)
        {
            //Random function
            float random_value =0f;
            //Success value
            float success_value =0f;

            GameObject SDer =ElementA;
            if(random_value >= success_value)
            {
                if( ElementA.GetComponent("Unit_script") as Unit_script!=null){    
                    Unit_script SDer_values = SDer.GetComponent<Unit_script>();
                    //Success? => loose health
                    SDer_values.Change_Health_stock(-10f);
                    
                }else{
                    Batiment_script SDer_values = SDer.GetComponent<Batiment_script>();
                    //Success? => loose health
                    SDer_values.Change_Health_stock(-10f);
                    
                }
            }
            SD_cancel(1);
            
        }
    }
    //End Turn Methods
    public void End_turn (int a)
    {
        UI_values.ChangePlayerTurn();
    }
    
    public int WhatKindOfObjectIsThisOne(GameObject Object)
    {
    //What type of GameObject (TOKEN) ?
        int returnValue;
        returnValue=0;
        if( Object.GetComponent("Unit_script") as Unit_script!=null)
        {
            returnValue= 1; //TOKEN is unit
        }
        if( Object.GetComponent("Batiment_script") as Batiment_script !=null)
        {
            returnValue= 2; //TOKEN is batiment
        }
        if( Object.GetComponent("Case_script") as Case_script!=null)
        {
            returnValue= 3; //TOKEN is case
        }
        return returnValue;
    }
    //UI update Methods
    public void Intel_UI_update (GameObject Object)
    {
        //Find object type and display according to
        switch(WhatKindOfObjectIsThisOne(Object))
        {
            case 0:
                Debug.Log("Object Type cannot be defined");
                break;
            case 1:
                Unit_script UnitValues = Object.GetComponent<Unit_script>();
                //Display
                Image_icon.material = Resources.Load<Material>("Textures/UI_"+UnitValues.type);
                TYPE_text.text = UnitValues.type.ToString();
                HV_text.text = UnitValues.health.ToString();
                HL_text.text = UnitValues.H_limit.ToString();
                AV_text.text = UnitValues.ammo.ToString();
                AL_text.text= UnitValues.A_limit.ToString();
                MV_text.text= UnitValues.mvt.ToString();
                ML_text.text= UnitValues.M_limit.ToString();
                Player_text.text= UnitValues.Owner.ToString();
                break;
            case 2:
                Batiment_script BuildingValues = Object.GetComponent<Batiment_script>();
                //Display
                Image_icon.material = Resources.Load<Material>("Textures/UI_"+BuildingValues.type);
                TYPE_text.text = BuildingValues.type.ToString();
                HV_text.text =  "";
                HL_text.text =  "";
                AV_text.text =  "";
                AL_text.text=  "";
                MV_text.text=  "";
                ML_text.text=  "";
                Player_text.text=  "";
                break;
            case 3:
                Case_script CaseValues = Object.GetComponent<Case_script>();
                //Display
                Image_icon.material = Resources.Load<Material>("Textures/UI_"+CaseValues.Terrain.name);
                TYPE_text.text = CaseValues.Terrain.name.ToString();
                HV_text.text = "N/A";
                HL_text.text = "N/A";
                AV_text.text = "N/A";
                AL_text.text = "N/A";
                MV_text.text = "N/A";
                ML_text.text = "N/A";
                Player_text.text= CaseValues.Occupation.ToString();
                break;
        }
    }
    public void Attack_A_UI_update (string icon, string description,GameObject Object)
    {
        //Mise à jour des données à afficher


        UI_values.Step =UI_Manager_script.StepType.Attack_B;
        ElementA =Object;
    }
    public void Attack_B_UI_update(string icon, string description,GameObject Object)
    {
        if(ElementA  != Object)
        {
           //Mise à jour des données à afficher

            UI_values.Step =UI_Manager_script.StepType.Attack_C;
            ElementB =Object;
        }    
    }
    public void Move_A_UI_update (string icon, string description,GameObject Object)
    {
        //Mise à jour des données à afficher

        UI_values.Step = UI_Manager_script.StepType.Move_B;
        ElementA =Object;
    }
    public void Move_B_UI_update (string icon, string description,GameObject Object)
    {
        //Mise à jour des données à afficher

        UI_values.Step=UI_Manager_script.StepType.Move_C;
        ElementB =Object;
    }
    public void SD_UI_update (string icon, string description,GameObject Object)
    {
        //Mise à jour des données à afficher

        UI_values.Step =UI_Manager_script.StepType.SD_B;
        ElementA =Object;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD2_display : MonoBehaviour
{
    public GameObject UI_Man;       public UI_Manager_script UI_values;
    //Token reference
    public enum TokenType
    {
        Unit,
        Building,
        Case,
        Default,
    } public TokenType Token;
    //Intel reference
    public GameObject PH2_Intel;    public CanvasGroup Intel_UI;
    public Image Image_icon; public Text TYPE_text; public Text HV_text; public Text HL_text;
    public Text AV_text; public Text AL_text; public Text MV_text; public Text ML_text; public Text Player_text;
    //Attack reference
    public GameObject PH2_Attack;   public CanvasGroup Attack_UI;
    public Image Attacker_icon; public Text Attacker_HV; public Text Attacker_HL; 
    public Text Attacker_AV; public Text Attacker_AL; public Text Attacker_MV;
    public Text Attacker_ML; public Text Attacker_Player;
    public Image Attacked_icon; public Text Attacked_HV; public Text Attacked_HL;
    public Text Attacked_AV; public Text Attacked_AL; public Text Attacked_MV; 
    public Text Attacked_ML; public Text Attacked_Player;
    //Move reference
    public GameObject PH2_Move;     public CanvasGroup Move_UI;
    public Image Voyager_icon; public Text Voyager_HV; public Text Voyager_HL; public Text Voyager_AV;
    public Text Voyager_AL; public Text Voyager_MV; public Text Voyager_ML; public Text Voyager_Player;
     public Image destination_icon; public Text destination_text;
     //Self-destruct reference
    public GameObject PH2_SD;       public CanvasGroup SD_UI;

    //Perform reference
    public GameObject ElementA;    public GameObject ElementB;

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
        Attacker_icon.material= Resources.Load<Material>("Textures/UI_Blank");
        Attacker_HV.text = "";
        Attacker_HL.text = "";
        Attacker_AV.text = "";
        Attacker_AL.text = "";
        Attacker_MV.text = "";
        Attacker_ML.text = "";
        Attacker_Player.text = "";


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
            //Attacker loses 1 ammo
            switch(WhatKindOfObjectIsThisOne(ElementA))
            {
                case TokenType.Default: 
                    Debug.Log("ElementA Type cannot be defined");
                    break;
                case TokenType.Unit:
                    Unit_script UnitValues = ElementA.GetComponent<Unit_script>();
                    //Call function reducing ammo
                    UnitValues.Change_Ammo_stock(-1f);
                    break;
                case TokenType.Building:
                    Batiment_script BuildingValues = ElementA.GetComponent<Batiment_script>();
                    //Call function reducing ammo
                    BuildingValues.Change_Ammo_stock(-1f);
                    break;
                case TokenType.Case:
                Debug.Log("Case cannot attack / No function available");
                break;
            }
            //Attacked might loses 1 health
            if(random_value>=success_value)
            {
                switch(WhatKindOfObjectIsThisOne(ElementB))
                {
                    case TokenType.Default: 
                        Debug.Log("Object Type cannot be defined");
                        break;
                    case TokenType.Unit:
                        Unit_script UnitValues = ElementB.GetComponent<Unit_script>();
                        //Call function reducing ammo
                        UnitValues.Change_Health_stock(-1f);
                        break;
                    case TokenType.Building:
                        Batiment_script BuildingValues = ElementA.GetComponent<Batiment_script>();
                        //Call function reducing ammo
                        BuildingValues.Change_Health_stock(-1f);
                        break;
                    case TokenType.Case:
                    Debug.Log("Case cannot be attacked / No function available");
                    break;
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
        Voyager_icon.material = Resources.Load<Material>("Textures/UI_Blank");
        Voyager_HV.text = "";
        Voyager_HL.text = "";
        Voyager_AV.text = "";
        Voyager_AL.text = "";
        Voyager_MV.text = "";
        Voyager_ML.text = "";
        Voyager_Player.text = "";
        destination_icon.material = Resources.Load<Material>("Textures/UI_Blank");
        destination_text.text = "";
        
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
        Case_script Origin_Case = Voyager_values.Occupied_case.GetComponent<Case_script>();
        GameObject destination = ElementB;
        Case_script destination_values = destination.GetComponent<Case_script>();

        //Remplace l'occupation de la case occupée avant le mouvement par l'unité
        Origin_Case.Occupation = Case_script.OccupationType.Free;
        //Le token unité bouge à son nouvel emplacement
        Voyager.transform.position = destination.transform.position + new Vector3(0,0.2f,0);
        Voyager_values.Occupied_case = destination_values;
        //La case destination est occupée
        destination_values.Occupation =Case_script.OccupationType.Unit;
        destination_values.Owning_control =Voyager_values.Owner;
        //L'unité perd un mouvement
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
                switch (WhatKindOfObjectIsThisOne(ElementA))
                {
                    case TokenType.Default:
                         Debug.Log("Object Type cannot be defined");
                        break;
                    case TokenType.Unit:
                        Unit_script Unit_values = SDer.GetComponent<Unit_script>();
                        //Success? => loose health
                        Unit_values.Change_Health_stock(-10f);
                        break;
                    case TokenType.Building:               
                        Batiment_script Building_values = SDer.GetComponent<Batiment_script>();
                        //Success? => loose health
                        Building_values.Change_Health_stock(-10f);
                        break;
                    case TokenType.Case:
                        Debug.Log("Case cannot Self-destruct / No function available");
                        break;
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
    
    public TokenType WhatKindOfObjectIsThisOne(GameObject Object)
    {
    //What type of GameObject (TOKEN) ?
        TokenType returnValue;
        returnValue = TokenType.Default;
        if( Object.GetComponent("Unit_script") as Unit_script!=null)
        {
            returnValue= TokenType.Unit; //TOKEN is unit
        }
        if( Object.GetComponent("Batiment_script") as Batiment_script !=null)
        {
            returnValue= TokenType.Building; //TOKEN is batiment
        }
        if( Object.GetComponent("Case_script") as Case_script!=null)
        {
            returnValue= TokenType.Case; //TOKEN is case
        }
        return returnValue;
    }
    //UI update Methods
    public void Intel_UI_update (GameObject Object)
    {
        //Find object type and display according to
        switch(WhatKindOfObjectIsThisOne(Object))
        {
            case TokenType.Default:
                Debug.Log("Object Type cannot be defined");
                break;
            case TokenType.Unit:
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
            case TokenType.Building:
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
            case TokenType.Case:
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
                Player_text.text=CaseValues.Occupation.ToString();
                break;
        }
    }
    public void Attack_A_UI_update (GameObject Object)
    {
        //Find object type and display according to
        switch(WhatKindOfObjectIsThisOne(Object))
        {
             case TokenType.Default:
                Debug.Log("Object Type cannot be defined");
                break;
            case TokenType.Unit:
                Unit_script UnitValues = Object.GetComponent<Unit_script>();
                //Display
                Attacker_icon.material= Resources.Load<Material>("Textures/UI_"+UnitValues.type);
                Attacker_HV.text = UnitValues.health.ToString();
                Attacker_HL.text = UnitValues.H_limit.ToString();
                Attacker_AV.text = UnitValues.ammo.ToString();
                Attacker_AL.text = UnitValues.A_limit.ToString();
                Attacker_MV.text = UnitValues.mvt.ToString();
                Attacker_ML.text = UnitValues.M_limit.ToString();
                Attacker_Player.text = UnitValues.Owner.ToString();
                break;
            case TokenType.Building:
                Batiment_script BuildingValues = Object.GetComponent<Batiment_script>();
                //Display
                Attacker_icon.material = Resources.Load<Material>("Textures/UI_"+BuildingValues.type);
                Attacker_HV.text =BuildingValues.health.ToString();
                Attacker_HL.text =BuildingValues.H_limit.ToString();
                Attacker_AV.text =BuildingValues.ammo.ToString();
                Attacker_AL.text =BuildingValues.A_limit.ToString();
                Attacker_MV.text ="N/A";
                Attacker_ML.text ="N/A";
                Attacker_Player.text =BuildingValues.Owner.ToString();
                break;
            case TokenType.Case:
                Debug.Log("Case cannot attack / No function available");
                break;
        }


        UI_values.Step =UI_Manager_script.StepType.Attack_B;
        ElementA =Object;
    }
    public void Attack_B_UI_update(GameObject Object)
    {
        if(ElementA  != Object && UI_values.Step == UI_Manager_script.StepType.Attack_B)
        {
            //Find object type and display according to
            switch(WhatKindOfObjectIsThisOne(Object))
            {
                case TokenType.Default:
                    Debug.Log("Object Type cannot be defined");
                    break;
                case TokenType.Unit:
                    Unit_script UnitValues = Object.GetComponent<Unit_script>();
                    //Display
                    Attacked_icon.material= Resources.Load<Material>("Textures/UI_"+UnitValues.type);
                    Attacked_HV.text = UnitValues.health.ToString();
                    Attacked_HL.text = UnitValues.H_limit.ToString();
                    Attacked_AV.text = UnitValues.ammo.ToString();
                    Attacked_AL.text = UnitValues.A_limit.ToString();
                    Attacked_MV.text = UnitValues.mvt.ToString();
                    Attacked_ML.text = UnitValues.M_limit.ToString();
                    Attacked_Player.text = UnitValues.Owner.ToString();
                    break;
                case TokenType.Building:
                    Batiment_script BuildingValues = Object.GetComponent<Batiment_script>();
                    //Display
                    Attacked_icon.material = Resources.Load<Material>("Textures/UI_"+BuildingValues.type);
                    Attacked_HV.text =BuildingValues.health.ToString();
                    Attacked_HL.text =BuildingValues.H_limit.ToString();
                    Attacked_AV.text =BuildingValues.ammo.ToString();
                    Attacked_AL.text =BuildingValues.A_limit.ToString();
                    Attacked_MV.text ="N/A";
                    Attacked_ML.text ="N/A";
                    Attacked_Player.text =BuildingValues.Owner.ToString();
                    break;
                case TokenType.Case:
                    Debug.Log("Case cannot be attacked / No function available");
                    break;
            }
                UI_values.Step =UI_Manager_script.StepType.Attack_C;
                ElementB = Object;
        }    
    }
    public void Move_A_UI_update (GameObject Object)
    {
        Unit_script UnitValues = Object.GetComponent<Unit_script>();
        Voyager_icon.material = Resources.Load<Material>("Textures/UI_"+UnitValues.type);
        Voyager_HV.text = UnitValues.health.ToString();
        Voyager_HL.text = UnitValues.H_limit.ToString();
        Voyager_AV.text = UnitValues.ammo.ToString();
        Voyager_AL.text = UnitValues.A_limit.ToString();
        Voyager_MV.text = UnitValues.mvt.ToString();
        Voyager_ML.text = UnitValues.M_limit.ToString();
        Voyager_Player.text = UnitValues.Owner.ToString();

        UI_values.Step = UI_Manager_script.StepType.Move_B;
        ElementA =Object;
    }
    public void Move_B_UI_update (GameObject Object)
    {
        Case_script CaseValues = Object.GetComponent<Case_script>();
        destination_icon.material = Resources.Load<Material>("Textures/UI_"+CaseValues.Terrain.name);
        destination_text.text = CaseValues.Terrain.name;

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

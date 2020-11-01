using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD2_display : MonoBehaviour
{
    public GameObject UI_Man;
    public GameObject PH2_Intel;
    public CanvasGroup Intel_UI;
    public GameObject PH2_Attack;
    public CanvasGroup Attack_UI;
    public GameObject PH2_Move;
    public CanvasGroup Move_UI;
    public GameObject PH2_SD;
    public CanvasGroup SD_UI;
    public UI_Manager_script UI_values;
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
    //Intel Methods
    public void Intel_display (int a)
    {
        if (Intel_UI.alpha == 0){
             Intel_UI.alpha = 1;
             Intel_UI.interactable = true;
             Intel_UI.blocksRaycasts = true;
            //Change UI_Manager step value
            UI_values.Step=UI_Manager_script.StepType.Intel;
 
        }else{
             Intel_UI.alpha = 0;
             Intel_UI.interactable = false;
             Intel_UI.blocksRaycasts =false ;
             UI_values.Step = UI_Manager_script.StepType.none;
        }  
        
    }
    //Attack Methods
    public void Attack_display (int a)
    {
         
         if (Attack_UI.alpha == 0){
             Attack_UI.alpha = 1;
             Attack_UI.interactable = true;
             Attack_UI.blocksRaycasts =true;
             UI_values.Step=UI_Manager_script.StepType.Attack_A;
 
         }else{
             Attack_cancel(1);
        }
    }
    public void Attack_cancel (int a)
    {
        //Mise en blanc de tous les champs

        //Fermeture UI_Attack
        Attack_UI.alpha = 0;
        Attack_UI.interactable = false;
        Attack_UI.blocksRaycasts =false;
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
         if (Move_UI.alpha == 0){
             Move_UI.alpha = 1;
             Move_UI.interactable = true;
             Move_UI.blocksRaycasts =true;
             UI_values.Step =UI_Manager_script.StepType.Move_A;
 
         }else{
             Move_Cancel(1);
        }   
    }
    public void Move_Cancel (int a)
    {
        // Mise en blanc de tous les champs
        
        //Fermeture UI_Move
        Move_UI.alpha = 0;
        Move_UI.interactable = false;
        Move_UI.blocksRaycasts =false;
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
         if (SD_UI.alpha == 0){
             SD_UI.alpha = 1;
             SD_UI.interactable = true;
             SD_UI.blocksRaycasts =true;
             UI_values.Step =UI_Manager_script.StepType.SD_A;
 
         }else{
             SD_cancel(1);
        }   
    }
    public void SD_cancel (int a)
    {
        //Mise en blac de tous les champs
        

        //Fermeture UI_SD
        SD_UI.alpha =0;
        SD_UI.interactable =false;
        SD_UI.blocksRaycasts =false;
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
        UI_values .ChangePlayerTurn();
    }
    //UI update Methods
    public void Intel_UI_update (string icon, string description,GameObject Object)
    {
        //Changement d'icon
        GameObject UI = GameObject.Find("Intel_icon");
        Image UI_icon = UI.GetComponent<Image>();
        UI_icon.material = Resources.Load<Material>("Textures/UI_"+icon);

        //Changement de texte
        GameObject UI2=GameObject.Find("Intel_text");
        Text UI_text= UI2.GetComponent<Text>();
        UI_text.text = description;
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

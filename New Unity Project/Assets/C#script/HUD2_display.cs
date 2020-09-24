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
    public string ElementA;
    public string ElementB;

    public string description;
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

        ElementA ="";
        ElementB ="";
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
            //Change UI_Manager step value
            UI_values.Step="Intel";
 
        }else{
             Intel_UI.alpha = 0;
             Intel_UI.interactable = false;
             UI_values.Step ="none";
        }  
        
    }
    //Attack Methods
    public void Attack_display (int a)
    {
         
         if (Attack_UI.alpha == 0){
             Attack_UI.alpha = 1;
             Attack_UI.interactable = true;
             UI_values.Step="Attack A";
 
         }else{
             Attack_cancel(1);
        }
    }
    public void Attack_cancel (int a)
    {
        //Set Icon => neutre
        GameObject UI = GameObject.Find("Attacker_icon");
        Image UI_icon = UI.GetComponent<Image>();
        UI_icon.material = Resources.Load<Material>("Textures/UI_Blank");
        //Set Text => neutre
        GameObject UI2=GameObject.Find("Attacker_text");
        Text UI2_text= UI2.GetComponent<Text>();
        UI2_text.text = "";
        //Set Icon => neutre
        GameObject UI3 = GameObject.Find("Attacked_icon");
        Image UI3_icon = UI3.GetComponent<Image>();
        UI3_icon.material = Resources.Load<Material>("Textures/UI_Blank");
        //Set Text => neutre
        GameObject UI4=GameObject.Find("Attacked_text");
        Text UI4_text= UI4.GetComponent<Text>();
        UI4_text.text = "";

        //Fermeture UI_Attack
        Attack_UI.alpha = 0;
        Attack_UI.interactable = false;
        UI_values.Step ="none";
        ElementA = "";
        ElementB ="";
    }
    public void Attack_Perform (int a)
    {if(UI_values.Step =="Attack Ready")
    {
        //Random function
        float random_value =0f;
        //Success value
        float success_value =0f;

        GameObject Attacker =GameObject.Find(ElementA);
        GameObject Attacked =GameObject.Find(ElementB);

        if( ElementA.Substring(0,4)=="Unit"){
              Unit_script Attacker_values = Attacker.GetComponent<Unit_script>();
              //Ammo stock reduction
            Attacker_values.Change_Ammo_stock(-1f);
        }else{         
            Batiment_script Attacker_values = Attacker.GetComponent<Batiment_script>();
            //Ammo stock reduction
            Attacker_values.Change_Ammo_stock(-1f);
        }
        
        if( ElementB.Substring(0,4)=="Unit"){    
            Unit_script Attacked_values = Attacked.GetComponent<Unit_script>();
            //Success? => loose health
            if(random_value >= success_value)
            {
                Attacked_values.Change_Health_stock(-1f);
            }
        }else{
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
             UI_values.Step ="Move A";
 
         }else{
             Move_Cancel(1);
        }   
    }
    public void Move_Cancel (int a)
    {
        //Changement d'icon
        GameObject UI = GameObject.Find("Voyager_icon");
        Image UI_icon = UI.GetComponent<Image>();
        UI_icon.material = Resources.Load<Material>("Textures/UI_Blank");
        //Changement de texte
        GameObject UI2=GameObject.Find("Voyager_text");
        Text UI2_text= UI2.GetComponent<Text>();
        UI2_text.text = "";
        //Changement d'icon
        GameObject UI3 = GameObject.Find("destination_icon");
        Image UI_icon2 = UI3.GetComponent<Image>();
        UI_icon.material = Resources.Load<Material>("Textures/UI_Blank");
        //Changement de texte
        GameObject UI4=GameObject.Find("destination_text");
        Text UI4_text= UI4.GetComponent<Text>();
        UI4_text.text = "";
        
        //Fermeture UI_Move
        Move_UI.alpha = 0;
        Move_UI.interactable = false;
        UI_values.Step ="none";
        ElementA = "";
        ElementB ="";
    }
    public void Move_Perform (int a)
    {if(UI_values.Step=="Move Ready")
    {
        GameObject Voyager =GameObject.Find(ElementA);
        Unit_script Voyager_values = Voyager.GetComponent<Unit_script>();
        GameObject destination =GameObject.Find(ElementB);
        Case_script destination_values = destination.GetComponent<Case_script>();

        Voyager.transform.position =destination.transform.position + new Vector3(0,0.2f,0);
        destination_values.Occupation ="Unit";
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
             UI_values.Step ="SD";
 
         }else{
             SD_cancel(1);
        }   
    }
    public void SD_cancel (int a)
    {
        //Changement d'icon
        GameObject UI = GameObject.Find("SD_icon");
        Image UI_icon = UI.GetComponent<Image>();
        UI_icon.material = Resources.Load<Material>("Textures/UI_Blank");

        //Changement de texte
        GameObject UI2=GameObject.Find("SD_Text");
        Text UI_text= UI2.GetComponent<Text>();
        UI_text.text = "";
        

        //Fermeture UI_SD
        SD_UI.alpha =0;
        SD_UI.interactable =false;
        UI_values.Step ="none";
        ElementA = "";

    }
    public void SD_Perform (int a)
    {if(UI_values.Step=="SD ready")
    {
        //Random function
        float random_value =0f;
        //Success value
        float success_value =0f;

        GameObject SDer =GameObject.Find(ElementA);
        if( ElementA.Substring(0,4)=="Unit"){    
            Unit_script SDer_values = SDer.GetComponent<Unit_script>();
            //Success? => loose health
            if(random_value >= success_value)
            {
                SDer_values.Change_Health_stock(-10f);
            }
        }else{
            Batiment_script SDer_values = SDer.GetComponent<Batiment_script>();
            //Success? => loose health
            if(random_value >= success_value)
            {
                SDer_values.Change_Health_stock(-10f);
            }
        }
        SD_cancel(1);
    }
    }
    //End Turn Methods
    public void End_turn (int a)
    {
        UI_values.Player_turn = UI_values.Player_turn +1;
    }
    //UI update Methods
    public void Intel_UI_update (string icon, string description,string Objectname)
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
    public void Attack_A_UI_update (string icon, string description,string Objectname)
    {
        //Changement d'icon
        GameObject UI = GameObject.Find("Attacker_icon");
        Image UI_icon = UI.GetComponent<Image>();
        UI_icon.material = Resources.Load<Material>("Textures/UI_"+icon);

        //Changement de texte
        GameObject UI2=GameObject.Find("Attacker_text");
        Text UI_text= UI2.GetComponent<Text>();
        UI_text.text = description;
        UI_values.Step ="Attack B";

        ElementA =Objectname;
    }
    public void Attack_B_UI_update(string icon, string description,string Objectname)
    {
        if(ElementA  != Objectname)
        {
            //Changement d'icon
            GameObject UI = GameObject.Find("Attacked_icon");
            Image UI_icon = UI.GetComponent<Image>();
            UI_icon.material = Resources.Load<Material>("Textures/UI_"+icon);

            //Changement de texte
            GameObject UI2=GameObject.Find("Attacked_text");
            Text UI_text= UI2.GetComponent<Text>();
            UI_text.text = description;
            UI_values.Step ="Attack Ready";

            ElementB =Objectname;
        }    
    }
    public void Move_A_UI_update (string icon, string description,string Objectname)
    {
        //Changement d'icon
        GameObject UI = GameObject.Find("Voyager_icon");
        Image UI_icon = UI.GetComponent<Image>();
        UI_icon.material = Resources.Load<Material>("Textures/UI_"+icon);

        //Changement de texte
        GameObject UI2=GameObject.Find("Voyager_text");
        Text UI_text= UI2.GetComponent<Text>();
        UI_text.text = description;
        UI_values.Step = "Move B";

        ElementA =Objectname;
    }
    public void Move_B_UI_update (string icon, string description,string Objectname)
    {
        //Changement d'icon
        GameObject UI = GameObject.Find("destination_icon");
        Image UI_icon = UI.GetComponent<Image>();
        UI_icon.material = Resources.Load<Material>("Textures/UI_"+icon);

        //Changement de texte
        GameObject UI2=GameObject.Find("destination_text");
        Text UI_text= UI2.GetComponent<Text>();
        UI_text.text = description;
        UI_values.Step="Move Ready";

        ElementB =Objectname;
    }
    public void SD_UI_update (string icon, string description,string Objectname)
    {
        //Changement d'icon
        GameObject UI = GameObject.Find("SD_icon");
        Image UI_icon = UI.GetComponent<Image>();
        UI_icon.material = Resources.Load<Material>("Textures/UI_"+icon);

        //Changement de texte
        GameObject UI2=GameObject.Find("SD_Text");
        Text UI_text= UI2.GetComponent<Text>();
        UI_text.text = description;
        UI_values.Step ="SD ready";

        ElementA =Objectname;
    }

    
}

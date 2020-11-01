using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_script : MonoBehaviour
{

public enum UnitType
{
    Rifle,
    Bazooka,
    Jeep,
    Tank, 
    Artillery
}
public UnitType type;
public float health; public float H_limit;
public float mvt; public float M_limit;
public float ammo; public float A_limit;
public float spawn; 
public float reload; public float R_limit; 
public float aim; public float Aim_limit;
public int Owner;
public bool activated;
public Vector3 Position;
public GameObject UI_Man; UI_Manager_script UI_values;
//Link to intel_display
public GameObject HUD2GO; HUD2_display HUD2Value;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        health =1;
        type = UnitType.Rifle;
        Set_up_unit_values();
        Recolte();
        //Link to UI_Manager
        UI_Man = GameObject.Find("UI_Manager");
        UI_values = UI_Man.GetComponent<UI_Manager_script>();
        HUD2GO =GameObject.Find("Phase II");
        HUD2Value = HUD2GO.GetComponent<HUD2_display>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Set up is called after unit instantiation
    public void Set_up_unit_values()
    {
        switch (type)
        {
            case UnitType.Rifle:
                H_limit = 1;
                M_limit =1;
                A_limit = 1;
                activated =true;
                spawn =0;
                R_limit = 1;
                Aim_limit =1;
                break;
            case UnitType.Bazooka:
                H_limit=1;
                M_limit=1;
                A_limit=2;
                spawn=1;
                R_limit=1;
                Aim_limit =1;
                break;
            case UnitType.Jeep:
                H_limit=1;
                M_limit =4;
                A_limit=2;
                spawn=1;
                R_limit=1;
                Aim_limit=1;
                break;
            case UnitType.Tank:
                H_limit=2;
                M_limit=3;
                A_limit=3;
                spawn=2;
                R_limit=1;
                Aim_limit=1;
                break;
            case UnitType.Artillery:
                H_limit=1;
                M_limit=2;
                A_limit=3;
                spawn=2;
                R_limit=1;
                Aim_limit=2;
                break;
        }
    }
    //Recolte is called in phase 3 or at building unit
    public void Recolte()
    {
        health = H_limit;
        mvt =M_limit;
        ammo=A_limit;
    }

    public void Change_time_before_activation()
    {
        if(spawn !=0)
        {
            spawn=spawn-1;
        }
        if(spawn==0)
        {
            activated=true;
            health=H_limit;
            mvt=M_limit;
            ammo=A_limit;
            reload=R_limit;
            aim=Aim_limit;
        }
    }

    public void Change_Health_stock(float Health_change)
    {
        if(health +Health_change <H_limit)
        {
            health=health+Health_change;
        }
        if(health<=0)
        {

            Destroy(this.gameObject);
            Debug.Log(name + " from owner " + Owner +" has been killed");
        }
    }

    public void Change_Ammo_stock(float Ammo_change)
    {
        if(ammo+Ammo_change< A_limit)
        {
            ammo=ammo+Ammo_change;
        }
    }
    public void Change_Mvt_stock(float Mvt_change)
    {
        if(mvt+Mvt_change<M_limit)
        {
            mvt=mvt+Mvt_change;
        }
    }
    
    private void OnMouseUp() 
    {
        //Phase 2 Step Intel
        if (UI_values.Step == UI_Manager_script.StepType.Intel)
        {
            string description = 
            type                +"\n"+"\r"+ //type
            Owner               +"\n"+"\r"+ //Owner
            health +"/"+H_limit +"\n"+"\r"+ //Health
            mvt +"/"+M_limit    +"\n"+"\r"+ //Move
            ammo +"/"+A_limit             ; //Ammo

            HUD2Value.Intel_UI_update("none",description,this.gameObject);
        }

        //Phase 2 Step Attack A
        // Owner Must be verified
        
        if (UI_values.Step == UI_Manager_script.StepType.Attack_A)
        {
            if(ammo>0)// Ammo stock must be verified
            {
                string description =
                type                +"\n"+"\r"+ //type
                Owner               +"\n"+"\r"+ //Owner
                health +"/"+H_limit +"\n"+"\r"+ //Health
                mvt +"/"+M_limit    +"\n"+"\r"+ //Move
                ammo +"/"+A_limit             ; //Ammo

                HUD2Value.Attack_A_UI_update("none",description,this.gameObject);
            }
            else
            {
                Debug.Log("no ammo");
            }
        }

        //Phase 2 Step Attack B
        if (UI_values.Step == UI_Manager_script.StepType.Attack_B)
        {
        //Link to intel_display
        GameObject Attacker = HUD2Value.ElementA;
        Unit_script Attacker_values = Attacker.GetComponent<Unit_script>();

            if(Attacker_values.name!=name && Attacker_values.Owner!=Owner) //Owner must be different
            {
                if(Vector3.Distance(Attacker.transform.position,transform.position)< Attacker_values.Aim_limit *1.8)
                {
                    string description =
                    type                +"\n"+"\r"+ //type
                    "Chance : " +   ""  +"\n"+"\r"+ // Calcul des proba de réussite
                    Owner               +"\n"+"\r"+ //Owner
                    health +"/"+H_limit +"\n"+"\r"+ //Health
                    mvt +"/"+M_limit    +"\n"+"\r"+ //Move
                    ammo +"/"+A_limit             ; //Ammo

                    HUD2Value.Attack_B_UI_update("none",description,this.gameObject);
                }
                else
                {
                    Debug.Log("Target is too far : "+Vector3.Distance(Attacker.transform.position,transform.position));
                }
            }
        }

        //Phase 2 Step Move A
        // Owner Must be verified
        if (UI_values.Step == UI_Manager_script.StepType.Move_A)
        {
            if(mvt>0f)//Mvt must be verified
            {
                //Link to intel_display
                string description =
                type                +"\n"+"\r"+ //type
                Owner               +"\n"+"\r"+ //Owner
                health +"/"+H_limit +"\n"+"\r"+ //Health
                mvt +"/"+M_limit    +"\n"+"\r"+ //Move
                ammo +"/"+A_limit             ; //Ammo

                HUD2Value.Move_A_UI_update("none",description,this.gameObject);
            }
            else
            {
                Debug.Log("no move available");
            }
        }
        
        //Phase 2 Step Self-Destruct
        //Owner must be verified
        if (UI_values.Step ==UI_Manager_script.StepType.SD_A)
        {
            //Link to intel_display
            string description =
            type                +"\n"+"\r"+ //type
            "Chance : " + "87%"    +"\n"+"\r"+ // Proba fixe de 87% (ne pas faire 1 sur un dé à 6 faces)
            Owner               +"\n"+"\r"+ //Owner
            health +"/"+H_limit +"\n"+"\r"+ //Health
            mvt +"/"+M_limit    +"\n"+"\r"+ //Move
            ammo +"/"+A_limit             ; //Ammo

            HUD2Value.SD_UI_update("none",description,this.gameObject);
        }
    }    
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Case_script : MonoBehaviour
{
    //Case property
    public Vector3 Position;
    public Material Terrain;
    public int  Owning_control;
    
    public Unit_script OccupingUnit;
    public Batiment_script OccupingBuilding;
    public enum OccupationType
    {
        Unit,
        Building,
        Free,
    };

    public int id;
    public OccupationType Occupation;

    public bool Supply;
    public int voisin_index;

    private List <Case_script> Cases_voisines= new List<Case_script>();
    public List<string> Cases_libres_voisines =new List<string>();
    public List<string> Cases_batiment_voisines =new List<string>();
    public List<string> Cases_ennemies_voisines =new List<string>();
    

    //Case reference for methods
    public GameObject BatimentPrefab;
    public GameObject Board;
    public Case_script Actual_property;
    public GameObject UI_case_Prefab;

    public GameObject UI_Man; UI_Manager_script UI_values;
    public GameObject HUD1GO; HUD1_script HUD1Value;
    public GameObject HUD2GO; HUD2_display HUD2Value;

    // Start is called before the first frame update
    void Start()
    {
         //Link to UI_Manager
        GameObject UI_Man = GameObject.Find("UI_Manager");
        UI_values = UI_Man.GetComponent<UI_Manager_script>();
        //Link to HUD1_display
        GameObject HUD1GO= GameObject.Find("Phase I");
        HUD1Value = HUD1GO.GetComponent<HUD1_script>();
        //Link to HUD2_display
        GameObject HUD2GO= GameObject.Find("Phase II");
        HUD2Value = HUD2GO.GetComponent<HUD2_display>();
        
        Owning_control =-1;
        Supply =false;
        Occupation=OccupationType.Free;       
     }
    public void AddNeighbour(Case_script CaseToAdd)
    {
        Cases_voisines.Add(CaseToAdd);
    }
    
    // Update is called once per frame
    void Update()
    {        
        
    }
    bool HasCaseAnAdjacentBuilding()
    {
        bool returnValue = false;
        foreach(Case_script CaseVoisine in Cases_voisines)
        {
            if (CaseVoisine.Occupation == OccupationType.Building)
            {
                returnValue = true;
            }
        }
        return returnValue;
    }
    bool HasCaseAnAdjacentAllyBuilding()
    {
        bool returnValue = false;
        foreach(Case_script CaseVoisine in Cases_voisines)
        {
            if (CaseVoisine.Occupation == OccupationType.Building 
                && CaseVoisine.Owning_control == Owning_control)
            {
                returnValue = true;
            }
        }
        return returnValue; 
    }
    bool HasCaseAnAdjacentEnemyUnit()
    {
        bool returnValue = false;
        foreach(Case_script CaseVoisine in Cases_voisines)
        {
            if (CaseVoisine.Occupation == OccupationType.Unit 
                && CaseVoisine.Owning_control != Owning_control)
            {
                returnValue = true;
            }
        }
        return returnValue;
    } 
    bool HasCaseAnAdjacentAllyUnit()
    {
        bool returnValue = false;
        foreach(Case_script CaseVoisine in Cases_voisines)
        {
            if (CaseVoisine.Occupation == OccupationType.Unit 
                && CaseVoisine.Owning_control == Owning_control)
            {
                returnValue = true;
            }
        }
        return returnValue; 
    }
    
    bool CheckVoisine(Case_script Case_to_check)
    {
        bool returnValue = false;
        foreach(Case_script CaseVoisine in Cases_voisines)
        {
            if (CaseVoisine == Case_to_check)
            {
                returnValue = true;
            }
        }
        return returnValue;
    }

    public List<Case_script> GetCasesToDistance(int myDistance)
    {
        List <Case_script> ListCases = new List<Case_script>();
        ListCases.Add(this);
        for (int RemainingDistance = 1; RemainingDistance <= myDistance; RemainingDistance++)
        {
            List<Case_script> tempList = new List<Case_script>();
            foreach(Case_script CurrentCase in ListCases)
            {
                foreach (Case_script CurrentNeighbour in CurrentCase.Cases_voisines)
                {
                    if (!ListCases.Contains(CurrentNeighbour) && !tempList.Contains(CurrentNeighbour))
                    {
                        tempList.Add(CurrentNeighbour);
                    }
                }
            }
            ListCases.AddRange(tempList);
        }
        return ListCases;
    }
    private void OnMouseUp() 
    {
        //Phase 1 Step Case selection for unit
        if (UI_values.Phase_number ==1 && UI_values.Step ==UI_Manager_script.StepType.Token_creation_case)
        {
            //Check if case is free
            if (Occupation == OccupationType.Free)
            {
                //Check if valid building is near
                if(true /*HasCaseAnAdjacentAllyBuilding()*/)
                {
                    //Send Case adress to GameManager (HUD1)
                    HUD1Value.Unit_Deployment(this.gameObject);
                }
            }

        }

        //Phase 2 Step Intel
        if (UI_values.Phase_number ==2 && UI_values.Step == UI_Manager_script.StepType.Intel)
        {
            HUD2Value.Intel_UI_update(this.gameObject);
        }
        
        //Phase 2 Step Move B
        if (UI_values.Phase_number ==2 && UI_values.Step == UI_Manager_script.StepType.DefineDestination)
        {
            if(Occupation==OccupationType.Free) //Case must be free
            {
                GameObject Voyager=HUD2Value.ElementA;
                Unit_script Voyager_values = Voyager.GetComponent<Unit_script>();
                //Debug.Log(Voyager.name);
                Case_script Origin_Case = Voyager_values.Occupied_case.GetComponent<Case_script>();
                if (CheckVoisine(Origin_Case)) //Case must be adjacente
                {
                    HUD2Value.Move_B_UI_update(this.gameObject);
                }
                else
                {
                    Debug.Log("This case is not a good choice");
                }
            }
            else
            {
                Debug.Log("Destination is not free");            
            }
        }
    }


    /*int AddTokenToCase(Unit_script token)
    {
        OccupingUnit = token;
        return;
    }
    Player_script getOwningControl()
    {
        if (OccupingUnit != null)
        {

        }
        if (OccupingBuilding != null)
        {

        }

            //Token présent?
            //Si non => Free
            //Si oui:
                //Renvoyer Joueur à qui ca appartient

    }*/
}

using System.Collections;
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
        Free
    }

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

    // Start is called before the first frame update
    void Start()
    {
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
    
    public void Create_Building(
        string input_name,int input_health,
        string input_type, string input_effect,int input_Owner)

    { 
        HasCaseAnAdjacentBuilding();
        //Créer un Building si pas de building adjacent et pas d'unités ennemies adjacentes.
        if(Cases_batiment_voisines.Count== 0 && Cases_ennemies_voisines.Count==0)
        {
            GameObject Batiment= (GameObject) Instantiate (BatimentPrefab);
            Batiment_script Batiment_Property =  Batiment.AddComponent<Batiment_script>();
            Batiment.transform.parent=transform;
            Batiment_Property.Owner=input_Owner;
            Batiment_Property.name =input_name;
            Batiment_Property.type =input_type;
            Batiment_Property.health=input_health;
            Batiment_Property.Effect=input_effect;
            Batiment.transform.position= Position + new Vector3(0,1.1f,0);
            Batiment_Property.Position= Position + new Vector3(0,1.1f,0);
            Batiment_Property.Case=name;

            Owning_control = input_Owner;
            Supply = true;
            Occupation = OccupationType.Building;
        }
        else 
        {
        
        }
     }
     private void OnMouseUp() 
    {
         //Link to UI_Manager
        GameObject UI_Man = GameObject.Find("UI_Manager");
        UI_Manager_script UI_values = UI_Man.GetComponent<UI_Manager_script>();

        //Phase 1 Step Case selection for unit
        if (UI_values.Phase_number ==1 && UI_values.Step ==UI_Manager_script.StepType.Token_creation_case)
        {
            //Check if case is free
            //Check if valid building is near
        }

        //Phase 2 Step Intel
        if (UI_values.Phase_number ==2 && UI_values.Step == UI_Manager_script.StepType.Intel)
        {
            //Link to intel_display
            GameObject HUD2= GameObject.Find("Phase II");
            HUD2_display HUD = HUD2.GetComponent<HUD2_display>();
            string description =name + "\n"+"\r"+
            Terrain.name + "\n"+ "\r"+
            "Owned by : " +Owning_control +"\n"+ "\r"+
            Occupation +" of use";
            HUD.Intel_UI_update(Terrain.name,description,this.gameObject);
        }
        
        //Phase 2 Step Move B
        if (UI_values.Phase_number ==2 && UI_values.Step == UI_Manager_script.StepType.Move_B)
        {
            if(Occupation==OccupationType.Free) //Case must be free
            {
                //Link to Move_display
                GameObject HUD2= GameObject.Find("Phase II");
                HUD2_display HUD = HUD2.GetComponent<HUD2_display>();
                GameObject Voyager=HUD.ElementA;
                if (Vector3.Distance(Voyager.transform.position, transform.position)< 1.8f) //Case must be adjacente
                {
                    string description =name + "\n"+"\r"+
                    Terrain.name + "\n"+ "\r";
                    HUD.Move_B_UI_update(Terrain.name,description,this.gameObject);
                }
                else
                {
                    Debug.Log("case is to far");
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

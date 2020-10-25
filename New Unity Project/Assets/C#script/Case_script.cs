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
        BatimentPrefab =Resources.Load<GameObject>("fixe/Batiment");
        Cases_libres_voisines.Clear();
        Cases_batiment_voisines.Clear();
        Cases_ennemies_voisines.Clear();
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
    
    public void Find_Free_Adjacentes()
    {
        Cases_libres_voisines.Clear();
        //Load Plateau intel
        Board = GameObject.Find("Plateau");
        Plateau_script plateau_property = Board.GetComponent<Plateau_script>();

        // Verif position toutes les cases

        for (int i=1;i<=plateau_property.numberOfCases-1;i++){
            //Load Case intel
            if("Case n°" + i != name){
                Actual_property = GameObject.Find("/Case n°" + i).GetComponent<Case_script>();
                
                /* si la case esst adjacente et que son occupation est libre, ça compte pour une case libre*/
                if( Vector3.Distance(Actual_property.Position,Position)<=1.8 
                    && Vector3.Distance(Actual_property.Position,Position)>0
                    && Actual_property.Occupation == OccupationType.Free)
                {
                    Cases_libres_voisines.Add(Actual_property.name);
                 }
            }
        } 
    }
    public void Find_Ennemies_Adjacentes()
    {
        Cases_ennemies_voisines.Clear();
        //Load Plateau intel
        Board = GameObject.Find("Plateau");
        Plateau_script plateau_property = Board.GetComponent<Plateau_script>();

        // Verif position toutes les cases

        for (int i=1;i<=plateau_property.numberOfCases-1;i++){
            //Load Case intel
            if("Case n°" + i != name){
                Actual_property = GameObject.Find("/Case n°" + i).GetComponent<Case_script>();

                /*Si case adjacente et le controle de celle-ci est différent de la case regardée,
                 ça compte pour une présence ennemie (batiment ou unités) */

                if( Vector3.Distance(Actual_property.Position,Position)<=1.8 
                    && Vector3.Distance(Actual_property.Position,Position)>0
                    && Owning_control!=Actual_property.Owning_control)
                {
                    Cases_ennemies_voisines.Add(Actual_property.name);
                 }
            }
        } 
    }
    
    public void AddNeighbour(Case_script CaseToAdd)
    {
        Cases_voisines.Add(CaseToAdd);
    }

    public void Create_Building(
        string input_name,int input_health,
        string input_type, string input_effect,int input_Owner)

    { 
        HasCaseAnAdjacentBuilding();
        Find_Ennemies_Adjacentes () ;
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
        if (UI_values.Phase_number ==1 && UI_values.Step == "Case selection for unit")
        {
            //Check if case is free
            //Check if valid building is near
        }

        //Phase 2 Step Intel
        if (UI_values.Phase_number ==2 && UI_values.Step == "Intel")
        {
            //Link to intel_display
            GameObject HUD2= GameObject.Find("Phase II");
            HUD2_display HUD = HUD2.GetComponent<HUD2_display>();
            string description =name + "\n"+"\r"+
            Terrain.name + "\n"+ "\r"+
            "Owned by : " +Owning_control +"\n"+ "\r"+
            Occupation +" of use";
            HUD.Intel_UI_update(Terrain.name,description,name);
        }
        
        //Phase 2 Step Move B
        if (UI_values.Phase_number ==2 && UI_values.Step == "Move B")
        {
            if(Occupation==OccupationType.Free) //Case must be free
            {
                //Link to Move_display
                GameObject HUD2= GameObject.Find("Phase II");
                HUD2_display HUD = HUD2.GetComponent<HUD2_display>();
                GameObject Voyager=GameObject.Find(HUD.ElementA);
                if (Vector3.Distance(Voyager.transform.position, transform.position)< 1.8f) //Case must be adjacente
                {
                    string description =name + "\n"+"\r"+
                    Terrain.name + "\n"+ "\r";
                    HUD.Move_B_UI_update(Terrain.name,description,name);
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

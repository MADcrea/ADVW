using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Case_script : MonoBehaviour
{
    //Case property
    public Vector3 Position;
    public Material Terrain;
    public string Owning_control;
    public string Occupation;
    public bool Supply;
    public int voisin_index;
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
        Owning_control ="none";
        Supply =false;
        Occupation="free";
        BatimentPrefab =Resources.Load<GameObject>("fixe/Batiment");
        Cases_libres_voisines.Clear();
        Cases_batiment_voisines.Clear();
        Cases_ennemies_voisines.Clear();


        
     }
    // Update is called once per frame
    void Update()
    {        
        
     }
    public void Find_Batiments_Adjacents()
    {
        Cases_batiment_voisines.Clear();
        //Load Plateau intel
        Board = GameObject.Find("Plateau");
        Plateau_script plateau_property = Board.GetComponent<Plateau_script>();

        // Verif position toutes les cases

        for (int i=1;i<=plateau_property.id-1;i++){
            //Load Case intel
            if("Case n°" + i != name){
                Actual_property = GameObject.Find("/Case n°" + i).GetComponent<Case_script>();

                /*Si la case adjacente contient un batiment, ca compte pour une case occupée par un batiment*/
                if( Vector3.Distance(Actual_property.Position,Position)<=1.8 
                    && Vector3.Distance(Actual_property.Position,Position)>0
                    && Actual_property.Occupation == "Building")
                {
                    Cases_batiment_voisines.Add(Actual_property.name);
                 }
            }
        } 
    }
    public void Find_Free_Adjacentes()
    {
        Cases_libres_voisines.Clear();
        //Load Plateau intel
        Board = GameObject.Find("Plateau");
        Plateau_script plateau_property = Board.GetComponent<Plateau_script>();

        // Verif position toutes les cases

        for (int i=1;i<=plateau_property.id-1;i++){
            //Load Case intel
            if("Case n°" + i != name){
                Actual_property = GameObject.Find("/Case n°" + i).GetComponent<Case_script>();
                
                /* si la case esst adjacente et que son occupation est libre, ça compte pour une case libre*/
                if( Vector3.Distance(Actual_property.Position,Position)<=1.8 
                    && Vector3.Distance(Actual_property.Position,Position)>0
                    && Actual_property.Occupation == "free")
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

        for (int i=1;i<=plateau_property.id-1;i++){
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
    
    public void Create_Building(
        string input_name,int input_health,
        string input_type, string input_effect,string input_Owner)

    { 
        Find_Batiments_Adjacents();
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
            Occupation = "Building";
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
            if(Occupation=="free") //Case must be free
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
}

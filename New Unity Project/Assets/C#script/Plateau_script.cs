using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plateau_script : MonoBehaviour
{
    
    public const int NUMBER_PLAYER_MAX = 4;
    public GameObject CasePrefab;
    public int Board_size;
    public float décalage_x;
    public int numberOfCases;
    public string Terrain;
    public GameObject UI_Man;
    Material CaseObjectMaterial ;

    const float CASE_WIDTH = 1.7f;

    float CASE_DIAGONAL = CASE_WIDTH * Mathf.Sqrt(3) / 2;


    
    Dictionary<int, Case_script> Liste_cases = new Dictionary<int, Case_script>();

    // Start is called before the first frame update
    void Start()
    {
        UI_Manager_script UI_values = UI_Man.GetComponent<UI_Manager_script>();

        if (SetBoardSize(UI_values.NUMBER_OF_PLAYERS) == false)
        {
            Debug.Log("Board Initilization failed");
            return;
        }
        int ErrorCode = InitializeBoard();
        switch (ErrorCode)
        {
            case 1:
                Debug.Log("Error 1: Description");
                break;
            case 2:
                Debug.Log("Error 2: Description");
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int InitializeBoard()
    {
               //Incrément du noms des cases
        numberOfCases=0;
        // l'utilisation d'hexagones/cercles impose une décalage une rangée sur 2.
        décalage_x = 0f;
        
        for(int i =1 ; i<=Board_size ; i++){
            for(int j = 1 ; j<=Board_size ; j++ ){
                //Set up du décalage
                if(i%2 == 0){
                    décalage_x=CASE_WIDTH/2f;
                }
                else{
                    décalage_x=0f;
                }
                GameObject CaseObject= (GameObject) Instantiate (CasePrefab);
                //Case_script Case =  CaseObject.AddComponent<Case_script>();
                Case_script Case =  CaseObject.AddComponent<Case_script>();
                Case.id = numberOfCases;
                /* CaseObject.transform.parent = transform; */
                CaseObject.name = "Case n°"+numberOfCases;
                Liste_cases.Add(numberOfCases,Case);
                //Debug.Log("Case ajoutée:" + Case);
                //Case.Position = new Vector3 (i*CASE_WIDTH+décalage_x,0,j*CASE_DIAGONAL);
                CaseObject.transform.position = new Vector3 (j*CASE_WIDTH+décalage_x,0,i*CASE_DIAGONAL);
                //Génération aléatoire du terrain
                int rand = Random.Range(0,16);
                if(rand <= 3f){
                    Terrain="Rivière";
                }
                if(rand>3f && rand<=8f){
                    Terrain="Plaine";
                }
                if(rand>8f && rand<= 12f){
                    Terrain = "Montagne";
                }
                if(rand>12f){
                    Terrain = "Forêt";
                }
                CaseObjectMaterial = Resources.Load<Material>("Textures/"+Terrain);
                MeshRenderer meshRenderer = CaseObject.GetComponent<MeshRenderer>();
                // Set the new material on the GameObject
                meshRenderer.material = CaseObjectMaterial;
                Case.Terrain = CaseObjectMaterial;
                numberOfCases++;
            }
        }
        InitializeNeighbourCases();
        return 0;
    }

    private bool SetBoardSize(int NumberOfPlayers)
    {
        bool BoardCreated = true;
        switch (NumberOfPlayers)
        {
            case 2:
                Board_size = 15;
                break;
            case 3:
                Board_size = 20;
                break;
            case 4:
                Board_size = 25;
            break;
            default:
                Board_size = 0;
                BoardCreated = false;
            break;
        }
        return BoardCreated;
    }

    private void InitializeNeighbourCases()
    {
        List<bool> neighbourList;
        int correctionOdd = 0;
        for(int i =0 ; i<numberOfCases ; i++)
        {
            //Debug.Log("CaseId:" + i);
            neighbourList = CheckCaseLocation(i);

            if (neighbourList[6])
            {
                correctionOdd = 1;
            }
            else
            {
                correctionOdd = 0;
            }
            if (neighbourList[0])
            {
                Liste_cases[i].AddNeighbour(Liste_cases[i-1]);
            }
            if (neighbourList[1])
            {
                Liste_cases[i].AddNeighbour(Liste_cases[i+Board_size-1+correctionOdd]);
            }
            if (neighbourList[2])
            {
                Liste_cases[i].AddNeighbour(Liste_cases[i+Board_size+correctionOdd]);
            }
            if (neighbourList[3])
            {
                Liste_cases[i].AddNeighbour(Liste_cases[i+1]);
            }
            if (neighbourList[4])
            {
                Liste_cases[i].AddNeighbour(Liste_cases[i-Board_size+correctionOdd]);
            }
            if (neighbourList[5])
            {
                Liste_cases[i].AddNeighbour(Liste_cases[i-Board_size-1+correctionOdd]);
            }
        }
    }

    private List<bool> CheckCaseLocation(int index)
    {
        int idCase;
        int quotient;
        int reste;
        List<bool> neighbourList = new List<bool>();
        for (int j=0;j<6;j++)
        {
            neighbourList.Add(true);
        }
        idCase = Liste_cases[index].id;
        reste = idCase%Board_size;
        quotient = (idCase-reste) / Board_size;
        if (quotient == Board_size-1)                   //Case au bord du haut
        {
            neighbourList[1] = false;
            neighbourList[2] = false;
        }
        if (quotient == 0)                              //Case au bord du bas
        {
            neighbourList[4] = false;
            neighbourList[5] = false;
        }
        if (reste == 0)                                //Case au bord gauche
        {
            neighbourList[0] = false;
            if (quotient%2 == 0)                       //Si ligne impaire on n'a pas de voisins
            {
                neighbourList[1] = false;
                neighbourList[5] = false;
            }
        }
        if (reste == (Board_size-1))                                //Case au bord droit
        {
            neighbourList[3] = false;
            if (quotient%2 == 1)                       //Si ligne paire on n'a pas de voisins
            {
                neighbourList[2] = false;
                neighbourList[4] = false;
            }
        }
        neighbourList.Add(quotient%2 == 1);            //Info sur la parité de la ligne
        return neighbourList;
    }
}

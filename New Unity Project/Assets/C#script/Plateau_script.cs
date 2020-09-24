using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau_script : MonoBehaviour
{
    public GameObject CasePrefab;
    public int Board_size;
    public float décalage_x;
    public int id;
    public string Terrain;
    Material CaseObjectMaterial ;
    Dictionary<string, Case_script> Liste_cases = new Dictionary<string, Case_script>();
     
    // Start is called before the first frame update
    void Start()
    {
        //Taille du plateau à corriger en fct du nombre de joueurs
        Board_size = 15;
        //Incrément du noms des cases
        id=1;
        // l'utilisation d'hexagones/cercles impose une décalage une rangée sur 2.
        décalage_x = 0f;

        for(int i =1 ; i<=Board_size ; i++){
            for(int j = 1 ; j<=Board_size ; j++ ){
                //Set up du décalage
                if(j/2f-Mathf.Round(j/2f) == 0){
                    décalage_x=1.7f/2f;
                }
                else{
                    décalage_x=0f;
                }
                GameObject CaseObject= (GameObject) Instantiate (CasePrefab);
                Case_script Case =  CaseObject.AddComponent<Case_script>();
                /* CaseObject.transform.parent = transform; */
                CaseObject.name = "Case n°"+id;
                Liste_cases.Add("Case n°"+id,Case);
                Case.Position = new Vector3 (i*1.7f+décalage_x,0,j*1.445f);
                CaseObject.transform.position = new Vector3 (i*1.7f+décalage_x,0,j*1.445f);
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
                id++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

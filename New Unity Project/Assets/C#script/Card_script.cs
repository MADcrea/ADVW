using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_script : MonoBehaviour
{
    public GameObject BuildingPrefab;
    public GameObject UpdatePrefab;
    public enum CardTypeI
    {TEST,CityUpdate,Building,}
    public enum CardTypeII
    {TEST,Production,Unlock,Attack,Reload,Defense,Bonus}
    public enum CardName 
    {
    TEST,Incinerateur,Satellite_IEM,Economie_d_energie,Transports_collectifs_gratuits,Ecole_de_guerre,Scierie,
    Programme_de_sante,Exposition_universelle,Deforestation,Poste_avance,Acierie,Cimenterie,Serveurs_informatique,
    Pyramide,Barrage_hydro_elec,Eolienne,Carriere,Casino,Ligne_de_metro,Aquaponie,Marche,Gratte_ciel,Ecole_de_pilotage,
    Centre_de_recherche,Fabrique_d_explosifs,Service_secret,Puits_de_petrole,Centre_spatial,Centrale_nucleaire,
    Plateforme_petroliere,Tour_de_controle,Bouclier_anti_missile,Decheterie,Bras_robot,Marches_publiques,
    Aeroport_commercial,Centrale_fusion_froide,Labo_secret,Port_spatial,Memorial,Loi_sur_les_emballages,
    Loi_de_libre_echange,Zaibatsu_conglomerat,Base_aerienne,Base_militaire,Colossius,Guardian,Hopital_de_campagne,
    }

    private int[,] cardsProperties = new int [,] { 
    //#card|TYPE1|TYPE2|        COUT      |     PROD          | SPAWN | HEALTH |  ATTACK   |SPECIAL |
    { 0,    0,      0,      0  ,0  ,0  ,0      ,0  ,0  ,0  ,0    ,0      ,0      ,0  ,0  ,0   ,0},//TEST
    { 1,    1,      1,      0  ,0  ,3  ,0      ,0  ,1  ,0  ,0    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 2,    1,      3,      0  ,5  ,1  ,4      ,0  ,0  ,0  ,0    ,3      ,-1     ,1  ,50 ,1   ,0},
    { 3,    1,      1,      0  ,0  ,0  ,6      ,0  ,2  ,0  ,0    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 4,    1,      1,      0  ,0  ,0  ,3      ,0  ,1  ,0  ,0    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 5,    2,      2,      1  ,0  ,3  ,0      ,0  ,0  ,0  ,0    ,2      ,1      ,0  ,0  ,0   ,0},
    { 6,    2,      1,      1  ,1  ,2  ,0      ,0  ,0  ,2  ,0    ,1      ,2      ,0  ,0  ,0   ,0},
    { 7,    1,      5,      0  ,0  ,1  ,5      ,0  ,0  ,0  ,0    ,3      ,-1     ,0  ,0  ,0   ,0},
    { 8,    2,      6,      0  ,0  ,-1 ,1      ,0  ,0  ,0  ,0    ,0      ,1      ,0  ,0  ,0   ,0},
    { 9,    2,      6,      0  ,-1 ,0  ,0      ,0  ,0  ,0  ,0    ,0      ,1      ,0  ,0  ,0   ,0},
    { 10,   2,      3,      1  ,0  ,1  ,0      ,0  ,0  ,0  ,0    ,1      ,2      ,1  ,50 ,1   ,0},
    { 11,   2,      1,      0  ,0  ,2  ,0      ,1  ,0  ,0  ,0    ,1      ,1      ,0  ,0  ,0   ,0},
    { 12,   2,      1,      0  ,0  ,2  ,0      ,0  ,0  ,0  ,1    ,1      ,1      ,0  ,0  ,0   ,0},
    { 13,   2,      1,      0  ,0  ,2  ,0      ,0  ,0  ,0  ,1    ,1      ,1      ,0  ,0  ,0   ,0},
    { 14,   2,      1,      0  ,0  ,2  ,0      ,0  ,0  ,1  ,0    ,1      ,1      ,0  ,0  ,0   ,0},
    { 15,   2,      1,      0  ,0  ,2  ,0      ,0  ,1  ,0  ,0    ,1      ,1      ,0  ,0  ,0   ,0},
    { 16,   2,      1,      0  ,0  ,2  ,0      ,0  ,1  ,0  ,0    ,1      ,1      ,0  ,0  ,0   ,0},
    { 17,   2,      1,      0  ,0  ,2  ,0      ,1  ,0  ,0  ,0    ,1      ,1      ,0  ,0  ,0   ,0},
    { 18,   1,      1,      0  ,0  ,0  ,3      ,0  ,0  ,0  ,1    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 19,   1,      1,      0  ,0  ,3  ,0      ,0  ,0  ,0  ,1    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 20,   1,      1,      0  ,0  ,3  ,0      ,0  ,1  ,0  ,0    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 21,   1,      1,      0  ,0  ,3  ,0      ,0  ,0  ,0  ,1    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 22,   1,      1,      0  ,0  ,3  ,0      ,0  ,0  ,0  ,1    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 23,   2,      2,      1  ,0  ,3  ,0      ,0  ,0  ,0  ,0    ,2      ,1      ,0  ,0  ,0   ,0},
    { 24,   2,      2,      1  ,0  ,3  ,0      ,0  ,0  ,0  ,0    ,2      ,1      ,0  ,0  ,0   ,0},
    { 25,   2,      2,      1  ,0  ,3  ,0      ,0  ,0  ,0  ,0    ,2      ,1      ,0  ,0  ,0   ,0},
    { 26,   2,      2,      1  ,0  ,3  ,0      ,0  ,0  ,0  ,0    ,2      ,1      ,0  ,0  ,0   ,0},
    { 27,   2,      1,      2  ,0  ,1  ,1      ,0  ,2  ,0  ,0    ,1      ,2      ,0  ,0  ,0   ,0},
    { 28,   2,      1,      2  ,0  ,2  ,0      ,1  ,0  ,1  ,0    ,1      ,2      ,0  ,0  ,0   ,0},
    { 29,   2,      1,      1  ,0  ,3  ,0      ,0  ,2  ,0  ,0    ,1      ,2      ,0  ,0  ,0   ,0},
    { 30,   2,      1,      2  ,0  ,2  ,0      ,0  ,2  ,0  ,0    ,1      ,2      ,0  ,0  ,0   ,0},
    { 31,   2,      5,      3  ,0  ,2  ,0      ,0  ,0  ,0  ,0    ,3      ,2      ,0  ,0  ,0   ,0},
    { 32,   2,      5,      2  ,1  ,2  ,0      ,0  ,0  ,0  ,0    ,3      ,2      ,0  ,0  ,0   ,0},
    { 33,   1,      1,      2  ,0  ,3  ,1      ,1  ,0  ,1  ,0    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 34,   1,      1,      3  ,0  ,1  ,2      ,1  ,0  ,0  ,1    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 35,   1,      1,      0  ,0  ,0  ,6      ,0  ,0  ,0  ,2    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 36,   2,      1,      1  ,0  ,3  ,2      ,0  ,0  ,0  ,3    ,1      ,3      ,0  ,0  ,0   ,0},
    { 37,   2,      1,      4  ,0  ,2  ,0      ,0  ,3  ,0  ,0    ,1      ,3      ,0  ,0  ,0   ,0},
    { 38,   2,      2,      2  ,0  ,6  ,0      ,0  ,0  ,0  ,0    ,2      ,2      ,0  ,0  ,0   ,0},
    { 39,   2,      1,      3  ,3  ,1  ,1      ,2  ,0  ,2  ,0    ,1      ,4      ,0  ,0  ,0   ,0},
    { 40,   1,      1,      0  ,0  ,0  ,9      ,0  ,0  ,3  ,0    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 41,   1,      1,      3  ,3  ,0  ,3      ,0  ,0  ,2  ,1    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 42,   1,      1,      0  ,0  ,0  ,9      ,1  ,1  ,1  ,0    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 43,   1,      1,      0  ,0  ,0  ,9      ,1  ,0  ,2  ,0    ,1      ,-1     ,0  ,0  ,0   ,0},
    { 44,   2,      3,      2  ,0  ,8  ,0      ,0  ,0  ,0  ,0    ,3      ,2      ,1  ,50 ,1   ,0},
    { 45,   2,      5,      2  ,0  ,8  ,0      ,0  ,0  ,0  ,0    ,3      ,2      ,0  ,0  ,0   ,0},
    { 46,   2,      5,      0  ,0  ,5  ,5      ,0  ,-5 ,0  ,0    ,3      ,2      ,0  ,0  ,0   ,0},
    { 47,   2,      5,      5  ,0  ,0  ,5      ,0  ,-5 ,0  ,0    ,3      ,2      ,0  ,0  ,0   ,0},
    { 48,   2,      5,      1  ,0  ,1  ,8      ,0  ,0  ,0  ,0    ,3      ,2      ,0  ,0  ,0   ,0},
    //#card|TYPE1|TYPE2|        COUT      |     PROD          | SPAWN | HEALTH |  ATTACK   |SPECIAL |
    };
    private string [] CardDescriptions=new string[]
    {
        "TEST",
        "La ville augmente la production 1 unité de pétrole",
        "Empeche une ville ennemie de produire ses ressources pendant 1 tour. Ne fonctionne qu'une fois",
        "La ville augmente la poduction de 2 unité de pétrole",
        "La ville augmente la production de 1 unité de pétrole",
        "Permet de constuire une base militaire",
        "Augmente la production de 2 unités de matériaux.",
        "Les fusiliers soignées par la ville reçoive 1 santé supplémentaire",
        "Produit 1 unité de Matériaux et 1 PV à la construction. Ne produit plus rien ensuite.",
        "Produit 5 unité de pétrole à la construction. Ne produit plus rien ensuite.",
        "Sans effet",
        "Augmente la production 1 unité d'acier",
        "Augmente la production 1 unité de devise",
        "Augmente la production 1 unité de devise",
        "Augmente la production 1 unité de Matériaux",
        "Augmente la production 1 unité de pétrole",
        "Augmente la production 1 unité de pétrole",
        "Augmente la production 1 unité d'acier",
        "La ville augmente la production 1 unité de devise",
        "La ville augmente la production 1 unité de devise",
        "La ville augmente la production 1 unité de pétrole",
        "La ville augmente la production 1 unité de devise",
        "La ville augmente la production 1 unité de devise",
        "Permet de construire une base aérienne",
        "Permet d'améliorer les unités Jeep",
        "Permet d'améliorer les unités Bazooka",
        "Permet d'améliorer les unités fusiliers",
        "Augmente la production 2 unités de pétrole",
        "Augmente la production 1 unité d'acier et 1 unité de Matériaux",
        "Augmente la production 2 unité de pétrole",
        "Augmente la production 2 unité de pétrole",
        "Protège les unités amies située à 2 cases d'une attaque aérienne",
        "Les unités amies adjacentes à la ville ne peuvent pas être attaquées par un mortier.",
        "La ville augmente la production 1 unité d'acier et 1 unité de matériaux",
        "La ville augmente la production 1 unité d'acier et 1 unité de devise",
        "La ville augmente la production 2 unité de devise",
        "Augmente la production 3 unité de devise",
        "Augmente la production 3 unité de pétrole",
        "Permet d'améliorer les unités Char et Mortier",
        "Augmente la production de 2 unités d'acier et 2 unités de matériaux",
        "La ville augmente la production 3 unité de Matériaux",
        "La ville augmente la production 2 unité de Matériaux et 1 unité de devise à chaque phase de récolte",
        "La ville augmente la production 1 unité d'acier, 1 unité de pétrole et 1 unité de Matériaux",
        "La ville augmente la production d'1 unité d'acier et 2 unité de matériaux",
        "Permet de lancer une attaque aérienne sur une unité ennemies (sur tout le plateau) à chaque phase de temps court. Consomme 1 unité de pétrole pour attaquer, la cible perd systématiquement 2 santé",
        "Recharge d'1 munition supplémentaire en phase de récolte à toutes les unités amies adjacentes",
        "La ville est considérée en état de siège si 4 unités ennemies sont adjacentes à la ville. La production de pétrole est réduite de 5.",
        "La ville est considérée en état de siège si 4 unités ennemies sont adjacentes à la ville. La production de pétrole est réduite de 5.",
        "Soigne 1 santé supplémentaire en phase récolte à toutes les unités amies adjacentes"

    };
    public CardName Card;
    public CardTypeI TYPEI;
    public CardTypeII TYPEII;
    private int SteelCost;
    private int EnergyCost;
    private int BrickCost;
    private int MoneyCost;
    private int SteelProd;
    private int EnergyProd;
    private int BrickProd;
    private int MoneyProd;
    private int SpawnTime;
    private int H_limit;
    private int A_limit;
    private int Aim_limit;
    private int R_limit;
    private int SpecialEffect;
    private string CardDescription;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0,49);
        Debug.Log(rand);
        SetCardParameters(rand);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SetCardParameters (int Cardnumber)
    {
    CardName Card       = (CardName) Cardnumber; 
    CardTypeI TYPEI     = (CardTypeI) cardsProperties[Cardnumber,1];
    CardTypeII TYPEII   = (CardTypeII) cardsProperties[Cardnumber,2];
    SteelCost           = cardsProperties[Cardnumber,3];
    EnergyCost          = cardsProperties[Cardnumber,4];
    BrickCost           = cardsProperties[Cardnumber,5];
    MoneyCost           = cardsProperties[Cardnumber,6];
    SteelProd           = cardsProperties[Cardnumber,7];
    EnergyProd          = cardsProperties[Cardnumber,8];
    BrickProd           = cardsProperties[Cardnumber,9];
    MoneyProd           = cardsProperties[Cardnumber,10];
    SpawnTime           = cardsProperties[Cardnumber,11];
    H_limit             = cardsProperties[Cardnumber,12];
    A_limit             = cardsProperties[Cardnumber,13];
    Aim_limit           = cardsProperties[Cardnumber,14];
    R_limit             = cardsProperties[Cardnumber,15];
    SpecialEffect       = cardsProperties[Cardnumber,16];
    CardDescription     = CardDescriptions[Cardnumber];
    //Debug.Log(Card);Debug.Log(TYPEI);Debug.Log(CardDescription);
    }
    public void CardChildGOCreation()
    {
        switch(TYPEI)
        {
            case CardTypeI.Building:
                //Instantiates a BUILDING Prefab with NO script in it.
                GameObject BuildingGO= (GameObject) Instantiate (BuildingPrefab);
                //Add ResourceManager to new created CardChildGO and set-it up
                ResourceManagerCreation(Card,BuildingGO); 
                //Add Building parameters to new created CardChildGO and set-it up
                SetUpBuilding(Card,BuildingGO);
                break;
            case CardTypeI.CityUpdate:
                //Instantiates an UPDATE Prefab with NO script in it.
                GameObject UpdateGO= (GameObject) Instantiate (UpdatePrefab);
                //Add ResourceManager to new created CardChildGO and set-it up
                ResourceManagerCreation(Card,UpdateGO);
                break;
        }
    }
   
    public void ResourceManagerCreation(CardName Card,GameObject CardChildGO)
    {
        //instantiates ResourceManager_script
        ResourceManager_script ResourceManager = CardChildGO.AddComponent<ResourceManager_script>();
        ResourceManager.SetUpCost(SteelCost, EnergyCost,BrickCost,MoneyCost);
    }
    private Batiment_script.BuildingType ConvertTYPEIItoBatimentType(CardTypeII TYPEII)
    {
        Batiment_script.BuildingType returnValue;
        returnValue = Batiment_script.BuildingType.Production;
        switch(TYPEII)
        {
            case CardTypeII.TEST:
                returnValue = Batiment_script.BuildingType.Production;
                break;
            case CardTypeII.Production:
                returnValue = Batiment_script.BuildingType.Production;
                break;
            case CardTypeII.Unlock:
                returnValue = Batiment_script.BuildingType.Unlock;
                break;
            case CardTypeII.Attack:
                returnValue = Batiment_script.BuildingType.Attack;
                break;
            case CardTypeII.Reload:
                returnValue = Batiment_script.BuildingType.Reload;
                break;
            case CardTypeII.Defense:
                returnValue = Batiment_script.BuildingType.Defense;
                break;
            case CardTypeII.Bonus:
                returnValue = Batiment_script.BuildingType.Bonus;
                break;
        }
        return returnValue;
    }
    public void SetUpBuilding(CardName Card,GameObject CardChildGO)
    //instantiates Batiment_script
    {
        //instantiates BuildingStat_script
        Batiment_script BuildingStat = CardChildGO.AddComponent<Batiment_script>();
       
        BuildingStat.Set_up_batiment_value(Card,ConvertTYPEIItoBatimentType(TYPEII), H_limit,A_limit,Aim_limit,SpawnTime);
        
        
       

    }
}

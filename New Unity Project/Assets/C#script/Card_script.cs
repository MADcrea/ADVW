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
    {TEST,Production,Unlock,Attack,Reload,Defense,Bonus,}
    public enum CardName 
    {
    TEST,Incinérateur,Satellite_IEM,Economie_d_énergie,Transports_collectifs_gratuits,Ecole_de_guerre,Scierie,
    Programme_de_santé,Exposition_universelle,Déforestation,Poste_avancé,Acierie,Cimenterie,Serveurs_informatique,
    Pyramide,Barrage_hydro_elec,Eolienne,Carrière,Casino,Ligne_de_métro,Aquaponie,Marché,Gratte_ciel,Ecole_de_pilotage,
    Centre_de_recherche,Fabrique_d_explosifs,Service_secret,Puits_de_pétrole,Centre_spatial,Centrale_nucleaire,
    Plateforme_pétrolière,Tour_de_contrôle,Bouclier_anti_missile,Déchèterie,Bras_robot,Marchés_publiques,
    Aéroport_commercial,Centrale_fusion_froide,Labo_secret,Port_spatial,Mémorial,Loi_sur_les_emballages,
    Loi_de_libre_échange,Zaibatsu_conglomérat,Base_aérienne,Base_militaire,Colossius,Guardian,Hopital_de_campagne,
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
    private CardName Card;
    private CardTypeI TYPEI;
    private CardTypeII TYPEII;
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
    // Start is called before the first frame update
    void Start()
    {
        //int rand = Random.Range(0,49);
        int rand =5;
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
    Debug.Log(Card);
    Debug.Log(TYPEI);
    Debug.Log(TYPEII);
    Debug.Log(SteelCost);
    }
    public void CardChildGOCreation()
    {
        /*switch(WhatTypeIsThisCard(Card))
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
        }*/
    }
   
    public void ResourceManagerCreation(CardName Card,GameObject CardChildGO)
    {
        //instantiates ResourceManager_script
        ResourceManager_script ResourceManager = CardChildGO.AddComponent<ResourceManager_script>();
        
    }
    public void SetUpBuilding(CardName Card,GameObject CardChildGO)
    //instantiates Batiment_script
    {
        //instantiates BuildingStat_script
        Batiment_script BuildingStat = CardChildGO.AddComponent<Batiment_script>();
        /*
        Set_up_batiment_value(
        Card_script.CardName Card, Batiment_script.BuildingType Set_up_type, float Set_up_H_limit,
        float Set_up_A_limit, float Set_up_Aim_limit, float Set_up_spawn)*/
        
       

    }
}

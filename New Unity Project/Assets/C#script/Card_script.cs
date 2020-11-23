using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_script : MonoBehaviour
{
    public GameObject BuildingPrefab;
    public GameObject UpdatePrefab;
    public enum CardTypeI
    {CityUpdate,Building,}
    public enum CardTypeII
    {Production,Unlock,Attack,Reload,Defense,Bonus,}
    public enum CardName 
    {
    Incinérateur,Satellite_IEM,Economie_d_énergie,Transports_collectifs_gratuits,Ecole_de_guerre,Scierie,
    Programme_de_santé,Exposition_universelle,Déforestation,Poste_avancé,Acierie,Cimenterie,Serveurs_informatique,
    Pyramide,Barrage_hydro_elec,Eolienne,Carrière,Casino,Ligne_de_métro,Aquaponie,Marché,Gratte_ciel,Ecole_de_pilotage,
    Centre_de_recherche,Fabrique_d_explosifs,Service_secret,Puits_de_pétrole,Centre_spatial,Centrale_nucleaire,
    Plateforme_pétrolière,Tour_de_contrôle,Bouclier_anti_missile,Déchèterie,Bras_robot,Marchés_publiques,
    Aéroport_commercial,Centrale_fusion_froide,Labo_secret,Port_spatial,Mémorial,Loi_sur_les_emballages,
    Loi_de_libre_échange,Zaibatsu_conglomérat,Base_aérienne,Base_militaire,Colossius,Guardian,Hopital_de_campagne,
    }
    public CardName Card;
    public CardTypeI TYPEI;
    public CardTypeII TYPEII;
    public int SteelCost;
    public int EnergyCost;
    public int BrickCost;
    public int MoneyCost;
    public int SteelProd;
    public int EnergyProd;
    public int BrickProd;
    public int MoneyProd;
    public int SpawnTime;
    public int H_limit;
    public int A_limit;
    public int Aim_limit;
    public int R_limit;
    public int SpecialEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetCardParameters (CardTypeI T1, CardTypeII T2,
                            int SC,int EC,int BC,int MC,
                            int SP,int EP,int BP,int MP,
                            int ST,int HL,int AL,int AimL,
                            int RL,int SE)
    {
    TYPEI           =T1;
    TYPEII          =T2;
    SteelCost       =SC;
    EnergyCost      =EC;
    BrickCost       =BC;
    MoneyCost       =MC;
    SteelProd       =SP;
    EnergyProd      =EP;
    BrickProd       =BP;
    MoneyProd       =MP;
    SpawnTime       =ST;
    H_limit         =HL;
    A_limit         =AL;
    Aim_limit       =AimL;
    R_limit         =RL;
    SpecialEffect   =SE;
    }
    void ReadCardParameters(CardName Card)
    {
        /*
        CardTypeI TYPEI, CardTypeII TYPEII,
        int SC,int EC,int BC,int MC,
        int SP,int EP,int BP,int MP,
        int ST,int HL,int AL,int AimL,
        int RL,int SE
        */
        switch(Card)
        {
            case CardName.Incinérateur:
                                                                            //COUT      PROD     SPAWN HEALTH ATTACK SPECIAL
                SetCardParameters(CardTypeI.CityUpdate,CardTypeII.Production,0,0,3,0,   0,1,0,0,    1,    0,  0,0,0     ,0);
                break;
            case CardName.Satellite_IEM :
                SetCardParameters(CardTypeI.CityUpdate,CardTypeII.Attack    ,0,5,1,4,   0,0,0,0,    3,    0,  0,0,0     ,1);
                break;
            case CardName.Economie_d_énergie:
                SetCardParameters(CardTypeI.CityUpdate,CardTypeII.Production,0,0,0,6,   0,2,0,0,    1,    0,  0,0,0     ,0);
                break;
            case CardName.Transports_collectifs_gratuits:
                SetCardParameters(CardTypeI.CityUpdate,CardTypeII.Production,0,0,0,3,   0,1,0,0,    1,    0,  0,0,0     ,0);
                break;
            case CardName.Ecole_de_guerre:
                SetCardParameters(CardTypeI.Building,CardTypeII.Unlock      ,1,0,3,0,   0,0,0,0,    2,    1,  0,0,0     ,2);
                break;
            case CardName.Scierie:
                SetCardParameters(CardTypeI.Building,CardTypeII.Production  ,1,1,2,0,   0,0,2,0,    1,    2,  0,0,0     ,0);
                break;
            case CardName.Programme_de_santé:
                SetCardParameters(0,0,1,5);
                break;
            case CardName.Exposition_universelle:
                SetCardParameters(0,0,0,1);
                break;
            case CardName.Déforestation:
                SetCardParameters(0,1,0,0);
                break;
            case CardName.Poste_avancé:
                SetCardParameters(1,0,1,0);
                break;
            case CardName.Acierie:
                SetCardParameters(0,0,2,0);
                break;
            case CardName.Cimenterie:
                SetCardParameters(0,0,2,0);
                break;
            case CardName.Serveurs_informatique:
                SetCardParameters(0,0,2,0);
                break;
            case CardName.Pyramide:
                SetCardParameters(0,0,2,0);
                break;
            case CardName.Barrage_hydro_elec:
                SetCardParameters(0,0,2,0);
                break;
            case CardName.Eolienne:
                SetCardParameters(0,0,2,0);
                break;
            case CardName.Carrière:
                SetCardParameters(0,0,2,0);
                break;
            case CardName.Casino:
                SetCardParameters(0,0,0,3);
                break;
            case CardName.Ligne_de_métro:
                SetCardParameters(0,0,3,0);
                break;
            case CardName.Aquaponie:
                SetCardParameters(0,0,3,0);
                break;
            case CardName.Marché:
                SetCardParameters(0,0,3,0);
                break;
            case CardName.Gratte_ciel:
                SetCardParameters(0,0,3,0);
                break;
            case CardName.Ecole_de_pilotage:
                SetCardParameters(1,0,3,0);
                break;
            case CardName.Centre_de_recherche:
                SetCardParameters(1,0,3,0);
                break;
            case CardName.Fabrique_d_explosifs:
                SetCardParameters(1,0,3,0);
                break;
            case CardName.Service_secret:
                SetCardParameters(1,0,3,0);
                break;
            case CardName.Puits_de_pétrole:
                SetCardParameters(2,0,1,1);
                break;
            case CardName.Centre_spatial:
                SetCardParameters(2,0,2,0);
                break;
            case CardName.Centrale_nucleaire:
                SetCardParameters(1,0,3,0);
                break;
            case CardName.Plateforme_pétrolière:
                SetCardParameters(2,0,2,0);
                break;
            case CardName.Tour_de_contrôle:
                SetCardParameters(3,0,2,0);
                break;
            case CardName.Bouclier_anti_missile:
                SetCardParameters(2,1,2,0);
                break;
            case CardName.Déchèterie:
                SetCardParameters(2,0,3,1);
                break;
            case CardName.Bras_robot:
                SetCardParameters(3,0,1,2);
                break;
            case CardName.Marchés_publiques:
                SetCardParameters(0,0,0,6);
                break;
            case CardName.Aéroport_commercial:
                SetCardParameters(1,0,3,2);
                break;
            case CardName.Centrale_fusion_froide:
                SetCardParameters(4,0,2,0);
                break;
            case CardName.Labo_secret:
                SetCardParameters(2,0,6,0);
                break;
            case CardName.Port_spatial:
                SetCardParameters(3,3,1,1);
                break;
            case CardName.Mémorial:
                SetCardParameters(0,0,0,9);
                break;
            case CardName.Loi_sur_les_emballages:
                SetCardParameters(3,3,0,3);
                break;
            case CardName.Loi_de_libre_échange:
                SetCardParameters(0,0,0,9);
                break;
            case CardName.Zaibatsu_conglomérat:
                SetCardParameters(0,0,0,9);
                break;
            case CardName.Base_aérienne:
                SetCardParameters(2,0,8,0);
                break;
            case CardName.Base_militaire:
                SetCardParameters(2,0,8,0);
                break;
            case CardName.Colossius:
                SetCardParameters(0,0,5,5);
                break;
            case CardName.Guardian:
                SetCardParameters(5,0,0,5);
                break;
            case CardName.Hopital_de_campagne:
                SetCardParameters(1,0,1,8);
                break;
        
        }
    }
    public void CardChildGOCreation()
    {
        switch(WhatTypeIsThisCard(Card))
        {
            case CardType.Building:
                //Instantiates a BUILDING Prefab with NO script in it.
                GameObject BuildingGO= (GameObject) Instantiate (BuildingPrefab);
                //Add ResourceManager to new created CardChildGO and set-it up
                ResourceManagerCreation(Card,BuildingGO); 
                //Add Building parameters to new created CardChildGO and set-it up
                SetUpBuilding(Card,BuildingGO);
                break;
            case CardType.Update:
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
        switch (Card)
        {
            //SetUpCost(Steel,Energy,Brick,Money)
            case CardName.Incinérateur:
                ResourceManager.SetUpCost(0,0,3,0);
                break;
            case CardName.Satellite_IEM :
                ResourceManager.SetUpCost(0,5,1,4);
                break;
            case CardName.Economie_d_énergie:
                ResourceManager.SetUpCost(0,0,0,6);
                break;
            case CardName.Transports_collectifs_gratuits:
                ResourceManager.SetUpCost(0,0,0,3);
                break;
            case CardName.Ecole_de_guerre:
                ResourceManager.SetUpCost(1,0,3,0);
                break;
            case CardName.Scierie:
                ResourceManager.SetUpCost(1,1,2,0);
                break;
            case CardName.Programme_de_santé:
                ResourceManager.SetUpCost(0,0,1,5);
                break;
            case CardName.Exposition_universelle:
                ResourceManager.SetUpCost(0,0,0,1);
                break;
            case CardName.Déforestation:
                ResourceManager.SetUpCost(0,1,0,0);
                break;
            case CardName.Poste_avancé:
                ResourceManager.SetUpCost(1,0,1,0);
                break;
            case CardName.Acierie:
                ResourceManager.SetUpCost(0,0,2,0);
                break;
            case CardName.Cimenterie:
                ResourceManager.SetUpCost(0,0,2,0);
                break;
            case CardName.Serveurs_informatique:
                ResourceManager.SetUpCost(0,0,2,0);
                break;
            case CardName.Pyramide:
                ResourceManager.SetUpCost(0,0,2,0);
                break;
            case CardName.Barrage_hydro_elec:
                ResourceManager.SetUpCost(0,0,2,0);
                break;
            case CardName.Eolienne:
                ResourceManager.SetUpCost(0,0,2,0);
                break;
            case CardName.Carrière:
                ResourceManager.SetUpCost(0,0,2,0);
                break;
            case CardName.Casino:
                ResourceManager.SetUpCost(0,0,0,3);
                break;
            case CardName.Ligne_de_métro:
                ResourceManager.SetUpCost(0,0,3,0);
                break;
            case CardName.Aquaponie:
                ResourceManager.SetUpCost(0,0,3,0);
                break;
            case CardName.Marché:
                ResourceManager.SetUpCost(0,0,3,0);
                break;
            case CardName.Gratte_ciel:
                ResourceManager.SetUpCost(0,0,3,0);
                break;
            case CardName.Ecole_de_pilotage:
                ResourceManager.SetUpCost(1,0,3,0);
                break;
            case CardName.Centre_de_recherche:
                ResourceManager.SetUpCost(1,0,3,0);
                break;
            case CardName.Fabrique_d_explosifs:
                ResourceManager.SetUpCost(1,0,3,0);
                break;
            case CardName.Service_secret:
                ResourceManager.SetUpCost(1,0,3,0);
                break;
            case CardName.Puits_de_pétrole:
                ResourceManager.SetUpCost(2,0,1,1);
                break;
            case CardName.Centre_spatial:
                ResourceManager.SetUpCost(2,0,2,0);
                break;
            case CardName.Centrale_nucleaire:
                ResourceManager.SetUpCost(1,0,3,0);
                break;
            case CardName.Plateforme_pétrolière:
                ResourceManager.SetUpCost(2,0,2,0);
                break;
            case CardName.Tour_de_contrôle:
                ResourceManager.SetUpCost(3,0,2,0);
                break;
            case CardName.Bouclier_anti_missile:
                ResourceManager.SetUpCost(2,1,2,0);
                break;
            case CardName.Déchèterie:
                ResourceManager.SetUpCost(2,0,3,1);
                break;
            case CardName.Bras_robot:
                ResourceManager.SetUpCost(3,0,1,2);
                break;
            case CardName.Marchés_publiques:
                ResourceManager.SetUpCost(0,0,0,6);
                break;
            case CardName.Aéroport_commercial:
                ResourceManager.SetUpCost(1,0,3,2);
                break;
            case CardName.Centrale_fusion_froide:
                ResourceManager.SetUpCost(4,0,2,0);
                break;
            case CardName.Labo_secret:
                ResourceManager.SetUpCost(2,0,6,0);
                break;
            case CardName.Port_spatial:
                ResourceManager.SetUpCost(3,3,1,1);
                break;
            case CardName.Mémorial:
                ResourceManager.SetUpCost(0,0,0,9);
                break;
            case CardName.Loi_sur_les_emballages:
                ResourceManager.SetUpCost(3,3,0,3);
                break;
            case CardName.Loi_de_libre_échange:
                ResourceManager.SetUpCost(0,0,0,9);
                break;
            case CardName.Zaibatsu_conglomérat:
                ResourceManager.SetUpCost(0,0,0,9);
                break;
            case CardName.Base_aérienne:
                ResourceManager.SetUpCost(2,0,8,0);
                break;
            case CardName.Base_militaire:
                ResourceManager.SetUpCost(2,0,8,0);
                break;
            case CardName.Colossius:
                ResourceManager.SetUpCost(0,0,5,5);
                break;
            case CardName.Guardian:
                ResourceManager.SetUpCost(5,0,0,5);
                break;
            case CardName.Hopital_de_campagne:
                ResourceManager.SetUpCost(1,0,1,8);
                break;
        }
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
        switch (Card)
        {
            case CardName.Ecole_de_guerre:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Unlock,1,0,0,2);
                break;
            case CardName.Scierie:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,2,0,0,1);
                break;
            case CardName.Exposition_universelle:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Bonus,1,0,0,0);
                break;
            case CardName.Déforestation:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Bonus,1,0,0,0);
                break;
            case CardName.Poste_avancé:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Attack,2,0,0,1);
                break;
            case CardName.Acierie:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,1,0,0,1);
                break;
            case CardName.Cimenterie:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,1,0,0,1);
                break;
            case CardName.Serveurs_informatique:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,1,0,0,1);
                break;
            case CardName.Pyramide:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,1,0,0,1);
                break;
            case CardName.Barrage_hydro_elec:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,1,0,0,1);
                break;
            case CardName.Eolienne:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,1,0,0,1);
                break;
            case CardName.Carrière:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,1,0,0,1);
                break;
            case CardName.Ecole_de_pilotage:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Unlock,1,0,0,2);
                break;
            case CardName.Centre_de_recherche:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Unlock,1,0,0,2);
                break;
            case CardName.Fabrique_d_explosifs:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Unlock,1,0,0,2);
                break;
            case CardName.Service_secret:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Unlock,1,0,0,2);
                break;
            case CardName.Puits_de_pétrole:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,2,0,0,1);
                break;
            case CardName.Centre_spatial:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,2,0,0,1);
                break;
            case CardName.Centrale_nucleaire:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,2,0,0,1);
                break;
            case CardName.Plateforme_pétrolière:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,2,0,0,1);
                break;
            case CardName.Tour_de_contrôle:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Defense,2,0,0,3);
                break;
            case CardName.Bouclier_anti_missile:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Defense,2,0,0,3);
                break;
            case CardName.Aéroport_commercial:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,3,0,0,1);
                break;
            case CardName.Centrale_fusion_froide:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,3,0,0,1);
                break;
            case CardName.Labo_secret:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Unlock,2,0,0,2);
                break;
            case CardName.Port_spatial:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Production,4,0,0,1);
                break;
            case CardName.Base_aérienne:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Attack,2,1,100,3);
                break;
            case CardName.Base_militaire:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Defense,2,0,0,3);
                break;
            case CardName.Colossius:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Defense,2,0,0,3);
                break;
            case CardName.Guardian:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Defense,2,0,0,3);
                break;
            case CardName.Hopital_de_campagne:
                BuildingStat.Set_up_batiment_value(Card,Batiment_script.BuildingType.Defense,2,0,0,3);
                break;
        }
       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager_script : MonoBehaviour
{
    public int Phase_number;
    public StepType Step;
    public int NUMBER_OF_PLAYERS = 2;

    public List<Card_script> MainDeck;
    public enum StepType
    {
        //Phase 1
        Token_creation_case,
        //Phase 2
        Intel, DefineAttacker, DefineAttacked, ReadyAttack, DefineVoyager, DefineDestination, ReadyMove, DefineSelfDestruct, ReadySelfDestruct,
        //Phase 3

        //Miscellaneous
        none
    }
    public int Player_turn;
    // Start is called before the first frame update
    void Start()
    {
        Phase_number=1;
        Player_turn = 1;
        Step=StepType.none;
        InitMainDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangePlayerTurn()
    {
        if (Player_turn +1 > NUMBER_OF_PLAYERS)
        {
            Player_turn=1;
        }
        else
        {
            Player_turn = Player_turn +1;
        }
    }

    private void InitMainDeck()
    {
        List<Card_script> MainDeck =new List<Card_script>();
    

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Card_N
{
    [SerializeField]
    int N; //카드 수
    
    public int Get_N
    {
        get { return N; }
    }

    [SerializeField]
    string card_name; //카드 종류

    public string Get_player_card_name
    {
        get { return card_name; }
    }
}


public class Player_Type : MonoBehaviour
{

    public Card_N[] Red;
    public Card_N[] Green;
    public Card_N[] Yellow;
    public Card_N[] Blue;
    public Card_N[] Purple;

}

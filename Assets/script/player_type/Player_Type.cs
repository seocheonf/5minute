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
    GameObject player_card; //카드 종류

    public GameObject Get_player_card
    {
        get { return player_card; }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//적은 모두 다름을 가정하여, 진행함. 플레이어 카드처럼 같은게 있을 거란 가정 X.

[System.Serializable]
public class Enemy_Card_N
{
    [SerializeField]
    string card_name;

    public string Get_enemy_card_name
    {
        get { return card_name; }
    }
}


public class Enemy_Type : MonoBehaviour
{

    public Enemy_Card_N[] A;
    public Enemy_Card_N[] B;
    public Enemy_Card_N[] C;
    public Enemy_Card_N[] D;
    public Enemy_Card_N[] E;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� ��� �ٸ��� �����Ͽ�, ������. �÷��̾� ī��ó�� ������ ���� �Ŷ� ���� X.

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

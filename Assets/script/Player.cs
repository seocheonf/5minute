using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<string> card_in_deck;

    public List<GameObject> card_in_hand;


    public List<string> card_in_sacrifice;

    public Card_N[] player_type;

    public Player_Type typetest;



    public Dictionary<string, GameObject> All_Card_Information;

    private void Awake()//�÷��̾� ī�� Ÿ�� ���� �׽�Ʈ
    {
        if (gameObject.name.Equals("Player"))
            player_type = typetest.Green;
        else
            player_type = typetest.Red;
    }

    private void Start()
    {
        StartCoroutine(drawcard());
    }

    IEnumerator drawcard()
    {
        yield return new WaitUntil(()=>All_Card_Information != null); //�� ������ �Ϸ�Ǳ� �������� ��ο����� �ʴ´�. ---- //��ü ĳ���� ������ ��ȸ�Ͽ�, �ʿ��� ī�������� �����Ǳ� �������� ī�� ��ο츦 �������� �ʴ´� -> All_Card_Information != null

        while (true)
        {
            if (card_in_hand.Count < 4 && card_in_deck.Count > 0)
            {
                //GameObject Drawing_Card = Instantiate()
                card_in_hand.Add(All_Card_Information[card_in_deck[0]]); //��� ī�� �������� ī�带 ������, �� ī�带 �ڵ忡 ����
                card_in_deck.RemoveAt(0); //���� �� ���� ������
            }
            yield return null;
        }
    }

    
}

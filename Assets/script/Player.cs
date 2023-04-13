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

    private void Awake()//플레이어 카드 타입 지정 테스트
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
        yield return new WaitUntil(()=>All_Card_Information != null); //덱 셔플이 완료되기 전까지는 드로우하지 않는다. ---- //전체 캐릭터 정보를 순회하여, 필요한 카드정보가 수집되기 전까지는 카드 드로우를 진행하지 않는다 -> All_Card_Information != null

        while (true)
        {
            if (card_in_hand.Count < 4 && card_in_deck.Count > 0)
            {
                //GameObject Drawing_Card = Instantiate()
                card_in_hand.Add(All_Card_Information[card_in_deck[0]]); //모든 카드 정보에서 카드를 빼오고, 그 카드를 핸드에 넣음
                card_in_deck.RemoveAt(0); //덱의 맨 위를 제거함
            }
            yield return null;
        }
    }

    
}

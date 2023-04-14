using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;

public class Player : MonoBehaviour
{
    public bool me;

    public List<string> card_in_deck;

    public Card_N[] player_type;

    public Player_Type typetest;

    //위의 데이터들은 네트워크를 통해 받아와야 할 다른 유저들의 정보.

    //플레이어의 생성은 나부터 시작되며, Player_Management에서 넣은 순서대로 table을 지정해줌.


    public List<GameObject> card_in_hand;


    public List<string> card_in_sacrifice;

    

    //카드 엔티티 정보

    public AssetReference Player_Card_Entity_inform;


    //player management로 부터 받는 카드에 대한 아카이브
    public Dictionary<string, GameObject> All_Card_Information;
    public Sprite Card_Back;


    private void Awake()//플레이어 카드 타입 지정 테스트
    {
        if (gameObject.name.Equals("Player"))
        {
            player_type = typetest.Green;
            me = true;
        }
        else
        {
            player_type = typetest.Red;
            me = false;
        }
    }

    private void Start()
    {
        StartCoroutine(drawcard());
    }

    IEnumerator drawcard()
    {
        yield return new WaitUntil(()=>All_Card_Information != null && Card_Back != null); //카드 정보(이미지, 텍스트 등)가 준비되기 전까지 드로우하지 않는다. //덱 셔플이 완료되기 전까지는 드로우하지 않는다. ---- //전체 캐릭터 정보를 순회하여, 필요한 카드정보가 수집되기 전까지는 카드 드로우를 진행하지 않는다 -> All_Card_Information != null

        while (true)
        {
            if (card_in_hand.Count < 4 && card_in_deck.Count > 0)
            {

                Player_Card_Entity_inform.InstantiateAsync().Completed += handler =>
                {
                    GameObject Entity = handler.Result;
                    StartCoroutine(card_generation(Entity.GetComponent<Player_Card_Entity>(), All_Card_Information[card_in_deck[0]].GetComponent<Player_Card>().card_data)); //생성된 엔티티 정보에 카드 정보를 찾아 대입시킴
                    card_in_hand.Add(Entity); //생성된 엔티티 정보를 핸드에 넣음

                };
                                
                card_in_deck.RemoveAt(0); //덱의 맨 위를 제거함
            }
            yield return null;
        }
    }

    //카드 실체 정보 생성
    IEnumerator card_generation(Player_Card_Entity A, Player_Card_Data B)
    {
        while (B.card_image_sprite == null || B.card_frame_sprite == null)
        {
            yield return null;
        }
        //Entity의 각 자식에 카드 데이터를 가져와 대입 시킴.
        A.Setup(B.card_frame_sprite, B.card_image_sprite, B.Card_name, B.Card_text, Card_Back, me);

    }


}

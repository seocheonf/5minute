using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using TMPro;

public class Player_Management : MonoBehaviour
{
    public List<Player> All_Player;

    public Dictionary<string, GameObject> All_Card_Information; //모든 플레이어가 지니고 있는 카드 정보

    string player_card_AssetReference_path = "Assets/Prefab/Player_card/";


    public GameObject Player_Card_Entity;
    public GameObject Entity_Spawn_Point;

    private void Start()
    {
        All_Card_Information = new Dictionary<string, GameObject>();

        Find_player();
        Preparing_player_deck();
        Preparing_all_card();
        
    }


    //-----------------------------------------------------------------

    //player정보를 탐색하는 과정
    private void Find_player()
    {
        GameObject[] All_Player_tempt = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Each_Player_tempt in All_Player_tempt)
        {
            All_Player.Add(Each_Player_tempt.GetComponent<Player>());
            Each_Player_tempt.GetComponent<Player>().All_Card_Information = All_Card_Information; // 카드 전체 정보에 대해 플레이어가 알 수 있도록 딕셔너리를 공유한다.
        }
    }


    //-----------------------------------------------------------------


    //player정보를 기반으로 필요한 덱을 준비하는 과정
    private void Preparing_player_deck()
    {
        foreach(Player each_player in All_Player)
        {
            Building_player_deck(each_player, each_player.player_type);
        }
    }
    
    //전체 카드 정보 등록
    private void Preparing_all_card()
    {
        foreach(KeyValuePair<string,GameObject> card_information in All_Card_Information)
        {
            string card_name = card_information.Key;
            Addressables.InstantiateAsync(player_card_AssetReference_path + card_name + ".prefab").Completed += handler =>
            {
                GameObject Card_result = handler.Result; //카드 정보 불러오기
                
                //카드 정보에서 실제 정보를 함수로 넘겨주기
                All_Card_Information[card_name] = card_generation(Card_result.GetComponent<Player_Card>()); //카드 실체 정보를 생성하고 카드 정보를 저장
            };
        }

        StartCoroutine(Waiting_all_card());
        
    }

    //카드 실체 정보 생성
    GameObject card_generation(Player_Card card_inform)
    {

        //카드 데이터는 일반 클래스로, monobehaviour를 상속받은 Player_Card를 통해 가져와야 함
        Player_Card_Data card_data = card_inform.card_data;

        //Entity를 생성하고, 그 자식 정보를 가져옴.
        GameObject generated_object = Instantiate(Player_Card_Entity,Entity_Spawn_Point.transform);
        Transform Inform = generated_object.transform;

        StartCoroutine(Card_image_generation(card_data, Inform)); //카드 이미지 생성 대기
        
        return generated_object;


    }

    //카드 이미지 생성 대기
    IEnumerator Card_image_generation(Player_Card_Data P, Transform T)
    {
        while(P.card_image_sprite == null || P.card_frame_sprite == null)
        {
            yield return null;
        }
        //Entity의 각 자식에 카드 데이터를 가져와 대입 시킴.
        T.Find("Description Image").GetComponent<SpriteRenderer>().sprite = P.card_image_sprite;
        T.Find("Frame").GetComponent<SpriteRenderer>().sprite = P.card_frame_sprite;
        T.Find("Name Text").GetComponent<TextMeshPro>().text = P.Card_name;
        T.Find("Description Text").GetComponent<TextMeshPro>().text = P.Card_text;


    }


    //
    IEnumerator Waiting_all_card() //전체 카드에 대한 오브젝트가 모두 생성되기 전까지는, 플레이어가 카드를 드로우하지 못하게 한다.
    {

        bool waiting_card = true;
        while (waiting_card)
        {
            waiting_card = false;
            foreach (KeyValuePair<string, GameObject> card_information in All_Card_Information)
            {
                if (card_information.Value == null)
                {
                    waiting_card = true;
                    break;
                }
            }
            yield return null;
        }
        yield return null;

        //위의 과정을 통해 모든 오브젝트가 준비가 된다면
        //모든 카드 정보를 각 플레이어에게 전달한다.
        foreach (Player each_player in All_Player)
        {
            each_player.All_Card_Information = All_Card_Information;
        }
        yield return null;
    }

    //덱 빌딩
    private void Building_player_deck(Player each_player, Card_N[] player_type)
    {
        string card_name;
        //직업에따라 정해진 카드와 그 숫자에 맞게, 덱에 카드 넣기
        foreach(Card_N Card_Information_in_player_type in player_type) //직업의 카드 정보 셋을 하나씩 살펴본다
        {
            card_name = Card_Information_in_player_type.Get_player_card_name;
            if(All_Card_Information.ContainsKey(card_name) != true) //만약 카드 정보가 딕셔너리에 없다면?
            {
                All_Card_Information.Add(card_name,null); //카드 정보(이름)을 딕셔너리에 제공한다.
            }

            for (int i = 0; i<Card_Information_in_player_type.Get_N; i++) //카드 정보 셋에서 카드 수만큼을 반복하며
            {
                each_player.card_in_deck.Add(Card_Information_in_player_type.Get_player_card_name); //카드 정보 셋에서 카드 정보를 불러와 덱에 넣어주도록 한다.
            }
        }

        //덱에 있는 카드 섞기
        Shuffle.Shuffle_Run(each_player.card_in_deck);

    }

    
}

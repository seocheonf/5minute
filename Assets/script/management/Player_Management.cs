using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using TMPro;

public class Player_Management : MonoBehaviour
{
    public List<Player> All_Player;

    public Dictionary<string, GameObject> All_Card_Information; //모든 플레이어가 지니고 있는 카드 정보


    [SerializeField]
    AssetReferenceAtlasedSprite Card_Back_Reference; //카드 뒷면 정보

    public Sprite Card_Back; //카드 뒷면 정보


    //플레이어 카드 정보 어드레서블 참조
    string player_card_AssetReference_path = "Assets/Prefab/Player_card/";


    private void Awake()
    {
        Card_Back_Reference.LoadAssetAsync().Completed += handler =>
        {
            Card_Back = handler.Result;
        };
    }


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
            Player A = Each_Player_tempt.GetComponent<Player>();
            All_Player.Add(A);
            A.Card_Back = Card_Back;
            
            //Each_Player_tempt.GetComponent<Player>().All_Card_Information = All_Card_Information; // 카드 전체 정보에 대해 플레이어가 알 수 있도록 딕셔너리를 공유한다.
        }

        Board_Management board_management = GameObject.Find("Board_Management").GetComponent<Board_Management>();
        board_management.Board_Generation(All_Player); //플레이어 정보를 넘겨, 각각에 맞는 보드 판 생성
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
                All_Card_Information[card_name] = handler.Result; //카드 정보 불러오기
            };
        }

        StartCoroutine(Waiting_all_card());
        
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

        while (Card_Back == null) //카드 뒷면 정보를 모두 불러오기 전까지는 플레이어가 카드를 드로우하지 못하게 한다.
        {
            yield return null;
        }

        yield return null;

        //위의 과정을 통해 모든 오브젝트가 준비가 된다면
        //모든 카드 정보를 각 플레이어에게 전달한다.
        foreach (Player each_player in All_Player)
        {
            each_player.All_Card_Information = All_Card_Information;
            each_player.Card_Back = Card_Back;
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

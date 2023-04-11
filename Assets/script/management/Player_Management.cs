using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Player_Management : MonoBehaviour
{
    public List<Player> All_Player;

    public HashSet<AssetReferenceAtlasedSprite> handler;

    public GameObject[] testingt;

    private void Start()
    {
        handler = new HashSet<AssetReferenceAtlasedSprite>();

        Find_player();
        Preparing_player_deck();
        StartCoroutine(testing());
        
    }


    //-----------------------------------------------------------------

    //player정보를 탐색하는 과정
    private void Find_player()
    {
        GameObject[] All_Player_tempt = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Each_Player_tempt in All_Player_tempt)
        {
            All_Player.Add(Each_Player_tempt.GetComponent<Player>());
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

    //덱 빌딩
    private void Building_player_deck(Player each_player, Card_N[] player_type)
    {
        
        //직업에따라 정해진 카드와 그 숫자에 맞게, 덱에 카드 넣기
        foreach(Card_N Card_with_Number in player_type) //직업의 카드 정보 셋을 하나씩 살펴본다
        {
            if(handler.Contains(Card_with_Number.Get_player_card.GetComponent<Player_Card>().card_data.card_frame_sprite_atlased) != true)
            {
                handler.Add(Card_with_Number.Get_player_card.GetComponent<Player_Card>().card_data.card_frame_sprite_atlased);
            }

            for (int i = 0; i<Card_with_Number.Get_N; i++) //카드 정보 셋에서 카드 수만큼을 반복하며
            {
                each_player.card_in_deck.Add(Card_with_Number.Get_player_card); //카드 정보 셋에서 카드 정보를 불러와 덱에 넣어주도록 한다.

            }
        }

        //덱에 있는 카드 섞기
        Shuffle.Shuffle_Run(each_player.card_in_deck);

    }
    
    IEnumerator testing()
    {
        yield return new WaitForSeconds(1f);
        int k = 0;
        bool flag = false;
        foreach(AssetReferenceAtlasedSprite test in handler)
        {
            Debug.Log("why?");
            test.LoadAssetAsync().Completed += hi => { testingt[k].GetComponent<SpriteRenderer>().sprite = hi.Result; k++; flag = true; };


            yield return new WaitUntil(()=>flag);
            flag = false;
        }
    }
    
}

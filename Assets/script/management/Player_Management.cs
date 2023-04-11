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

    //player������ Ž���ϴ� ����
    private void Find_player()
    {
        GameObject[] All_Player_tempt = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Each_Player_tempt in All_Player_tempt)
        {
            All_Player.Add(Each_Player_tempt.GetComponent<Player>());
        }
    }


    //-----------------------------------------------------------------


    //player������ ������� �ʿ��� ���� �غ��ϴ� ����
    private void Preparing_player_deck()
    {
        foreach(Player each_player in All_Player)
        {
            Building_player_deck(each_player, each_player.player_type);
        }
    }

    //�� ����
    private void Building_player_deck(Player each_player, Card_N[] player_type)
    {
        
        //���������� ������ ī��� �� ���ڿ� �°�, ���� ī�� �ֱ�
        foreach(Card_N Card_with_Number in player_type) //������ ī�� ���� ���� �ϳ��� ���캻��
        {
            if(handler.Contains(Card_with_Number.Get_player_card.GetComponent<Player_Card>().card_data.card_frame_sprite_atlased) != true)
            {
                handler.Add(Card_with_Number.Get_player_card.GetComponent<Player_Card>().card_data.card_frame_sprite_atlased);
            }

            for (int i = 0; i<Card_with_Number.Get_N; i++) //ī�� ���� �¿��� ī�� ����ŭ�� �ݺ��ϸ�
            {
                each_player.card_in_deck.Add(Card_with_Number.Get_player_card); //ī�� ���� �¿��� ī�� ������ �ҷ��� ���� �־��ֵ��� �Ѵ�.

            }
        }

        //���� �ִ� ī�� ����
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

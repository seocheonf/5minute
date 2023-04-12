using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;

public class Player_Management : MonoBehaviour
{
    public List<Player> All_Player;

    public Dictionary<string, GameObject> All_Card_Information; //��� �÷��̾ ���ϰ� �ִ� ī�� ����

    string player_card_AssetReference_path = "Assets/Prefab/Player_card/";

    private void Start()
    {
        All_Card_Information = new Dictionary<string, GameObject>();

        Find_player();
        Preparing_player_deck();
        Preparing_all_card();
        
    }


    //-----------------------------------------------------------------

    //player������ Ž���ϴ� ����
    private void Find_player()
    {
        GameObject[] All_Player_tempt = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Each_Player_tempt in All_Player_tempt)
        {
            All_Player.Add(Each_Player_tempt.GetComponent<Player>());
            Each_Player_tempt.GetComponent<Player>().All_Card_Information = All_Card_Information; // ī�� ��ü ������ ���� �÷��̾ �� �� �ֵ��� ��ųʸ��� �����Ѵ�.
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
    
    //��ü ī�� ���� ���
    private void Preparing_all_card()
    {
        foreach(KeyValuePair<string,GameObject> card_information in All_Card_Information)
        {
            string card_name = card_information.Key;
            Addressables.InstantiateAsync(player_card_AssetReference_path + card_name + ".prefab").Completed += handler =>
            {
                All_Card_Information[card_name] = handler.Result;
            };
        }

        StartCoroutine(Waiting_all_card());
        
    }

    //
    IEnumerator Waiting_all_card() //��ü ī�忡 ���� ������Ʈ�� ��� �����Ǳ� ��������, �÷��̾ ī�带 ��ο����� ���ϰ� �Ѵ�.
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

        //���� ������ ���� ��� ������Ʈ�� �غ� �ȴٸ�
        //��� ī�� ������ �� �÷��̾�� �����Ѵ�.
        foreach (Player each_player in All_Player)
        {
            each_player.All_Card_Information = All_Card_Information;
        }
        yield return null;
    }

    //�� ����
    private void Building_player_deck(Player each_player, Card_N[] player_type)
    {
        string card_name;
        //���������� ������ ī��� �� ���ڿ� �°�, ���� ī�� �ֱ�
        foreach(Card_N Card_Information_in_player_type in player_type) //������ ī�� ���� ���� �ϳ��� ���캻��
        {
            card_name = Card_Information_in_player_type.Get_player_card_name;
            if(All_Card_Information.ContainsKey(card_name) != true) //���� ī�� ������ ��ųʸ��� ���ٸ�?
            {
                All_Card_Information.Add(card_name,null); //ī�� ����(�̸�)�� ��ųʸ��� �����Ѵ�.
            }

            for (int i = 0; i<Card_Information_in_player_type.Get_N; i++) //ī�� ���� �¿��� ī�� ����ŭ�� �ݺ��ϸ�
            {
                each_player.card_in_deck.Add(Card_Information_in_player_type.Get_player_card_name); //ī�� ���� �¿��� ī�� ������ �ҷ��� ���� �־��ֵ��� �Ѵ�.
            }
        }

        //���� �ִ� ī�� ����
        Shuffle.Shuffle_Run(each_player.card_in_deck);

    }

    
}

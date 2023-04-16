using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//getcomponent<Player_Card>().Using_Card() �� ���� ī�� ������ ȿ���� ������ �� ����



public class Player_Card : MonoBehaviour //�Ϲ�, ��ȭ ����ī��� �� ��ũ��Ʈ�� �״�� �����ϸ� ��.
{
    
    public Player_Card_Data card_data; //ī�忡 ���� �ڼ��� ����


    private void Awake() //ī�尡 �ҷ����� ���ÿ�, ī���� ��������Ʈ�� ��Ʋ��(��巹���� ����)�� ���� �ҷ����� ����.
    {
        card_data.card_frame_sprite_atlas.LoadAssetAsync().Completed += handler =>
        {
            card_data.card_frame_sprite = handler.Result;
        };
        card_data.card_image_sprite_atlas.LoadAssetAsync().Completed += handler =>
        {
            card_data.card_image_sprite = handler.Result;
        };
    }

    //Specialī���� ���, �ݵ�� �� ������ �ؾ���.

    public virtual bool Using_Card(GameObject Enemy_object)//Player owner, Player_Management player_management, Board_Management board_management)
    {
        if (card_data.Card_type.Equals("Special")) //Specialī���� �ɷ� �籸���� ���� ��, ������ �߻����� �ʵ��� ���� ó��.
            return true;
        else //�Ϲ�, ��ȭ ����ī���� ���� ����.
        {
            Enemy enemy_tempt;
            enemy_tempt = Enemy_object.GetComponent<Enemy>();
            if(enemy_tempt.hp.ContainsKey(card_data.Card_name))
            {
                if (enemy_tempt.hp[card_data.Card_name] > 0)
                {
                    enemy_tempt.hp[card_data.Card_name] -= 1;
                    enemy_tempt.hp_all -= 1;
                }
            }
            if(enemy_tempt.hp_all == 0)
            {
                enemy_tempt.alive = false;
            }
            return true;
        }
    }

}

//virtual�� ���, �� ��ü�� �״�� ������ �ᵵ ������, �� Ŭ������ ��ӹ��� �������� �����Ͽ� ����� ���� �ְ� ����.
//�� Ŭ������ �Ϲ� Ŭ������, �Լ��� �Ϲ��̶��, override�� ���� �ʴ���.

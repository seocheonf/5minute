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

    public virtual void Using_Card()//Player owner, Player_Management player_management, Board_Management board_management)
    {
        if (card_data.Card_type.Equals("Special")) //Specialī���� �ɷ� �籸���� ���� ��, ������ �߻����� �ʵ��� ���� ó��.
            return;
        else //�Ϲ�, ��ȭ ����ī���� ���� ����.
        {
            Debug.Log("normal");
        }
    }

}

//virtual�� ���, �� ��ü�� �״�� ������ �ᵵ ������, �� Ŭ������ ��ӹ��� �������� �����Ͽ� ����� ���� �ְ� ����.
//�� Ŭ������ �Ϲ� Ŭ������, �Լ��� �Ϲ��̶��, override�� ���� �ʴ���.

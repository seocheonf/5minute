using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//getcomponent<Player_Card>().Using_Card() 를 통해 카드 각각의 효과를 가져올 수 있음



public class Player_Card : MonoBehaviour //일반, 강화 공격카드는 이 스크립트를 그대로 부착하면 됨.
{
    
    public Player_Card_Data card_data; //카드에 대한 자세한 정보


    private void Awake() //카드가 불려지는 동시에, 카드의 스프라이트를 아틀라스(어드레서블 에셋)로 부터 불러오는 과정.
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

    //Special카드의 경우, 반드시 재 구현을 해야함.

    public virtual bool Using_Card(GameObject Enemy_object)//Player owner, Player_Management player_management, Board_Management board_management)
    {
        if (card_data.Card_type.Equals("Special")) //Special카드의 능력 재구현을 잊을 시, 오류가 발생하지 않도록 예외 처리.
            return true;
        else //일반, 강화 공격카드의 동작 로직.
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

//virtual의 경우, 이 자체를 그대로 가져다 써도 되지만, 이 클래스를 상속받은 누군가가 수정하여 사용할 수도 있게 해줌.
//이 클래스가 일반 클래스에, 함수도 일반이라면, override가 되지 않더라.

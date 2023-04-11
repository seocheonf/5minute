using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//getcomponent<Player_Card>().Using_Card() 를 통해 카드 각각의 효과를 가져올 수 있음



public class Player_Card : MonoBehaviour //일반, 강화 공격카드는 이 스크립트를 그대로 부착하면 됨.
{
    
    public Player_Card_Data card_data; //카드에 대한 자세한 정보

    //Special카드의 경우, 반드시 재 구현을 해야함.

    public virtual void Using_Card()//Player owner, Player_Management player_management, Board_Management board_management)
    {
        if (card_data.Card_type.Equals("Special")) //Special카드의 능력 재구현을 잊을 시, 오류가 발생하지 않도록 예외 처리.
            return;
        else //일반, 강화 공격카드의 동작 로직.
        {
            Debug.Log("normal");
        }
    }

}

//virtual의 경우, 이 자체를 그대로 가져다 써도 되지만, 이 클래스를 상속받은 누군가가 수정하여 사용할 수도 있게 해줌.
//이 클래스가 일반 클래스에, 함수도 일반이라면, override가 되지 않더라.

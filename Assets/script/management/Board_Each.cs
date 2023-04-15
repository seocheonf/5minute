using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//각 보드는 플레이어의 카드가 추가될 때 함수를 호출받아 정렬을 수행한다.

public class Board_Each : MonoBehaviour
{
    public void SpreadCard(List<GameObject> Player_card_in_hand)
    {
        foreach(GameObject each_card in Player_card_in_hand)
        {
            each_card.transform.position = transform.position;
            each_card.transform.rotation = transform.rotation;
        }
    }
}

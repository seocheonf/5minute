using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�� ����� �÷��̾��� ī�尡 �߰��� �� �Լ��� ȣ��޾� ������ �����Ѵ�.

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

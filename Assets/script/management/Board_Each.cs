using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�� ����� �÷��̾��� ī�尡 �߰��� �� �Լ��� ȣ��޾� ������ �����Ѵ�.

public class Board_Each : MonoBehaviour
{
    public void SpreadCard(List<GameObject> Player_card_in_hand)
    {
        if (Player_card_in_hand.Count <= 0)
            return;

        float frame_width = Player_card_in_hand[0].GetComponent<Player_Card_Entity>().GetSize().x;
        int card_count = Player_card_in_hand.Count;
        float narrow_width = 0.25f * frame_width * (1 + (card_count / (2 * card_count - 1)));
        float all_width = frame_width * card_count - (card_count - 1) * narrow_width;


        for (int i = 0; i < Player_card_in_hand.Count; i++)
        {
            //ī�� ������, ������ �ҷ�����
            GameObject each_card = Player_card_in_hand[i];
            Player_Card_Entity entity_tempt = each_card.GetComponent<Player_Card_Entity>();

            //ī�� sorting layer order ����
            entity_tempt.SetOriginOrder(i);

            //ī�� ���� ����
            each_card.transform.rotation = transform.rotation;

            //ī�� x��ǥ ����
            float set_position_x = -(0.5f * all_width) + 0.5f * frame_width + (i * (frame_width - narrow_width));
            each_card.transform.position = new Vector3(set_position_x,transform.position.y, transform.position.z);

            
            

            //ī�� ��ġ ����
            entity_tempt.Set_card_origin();
        }
    }
}

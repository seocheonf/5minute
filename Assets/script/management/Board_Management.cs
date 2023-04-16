using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ����� ������Ʈ ��ġ���� �����Ѵ�.

public class Board_Management : MonoBehaviour
{
    public GameObject Board_Each_Object;

    public GameObject Board_Each_Object_enemy;

    public void Board_Generation(List<Player> All_Player)
    {
        int all_player_count = All_Player.Count; // �÷��̾� ����ŭ ����

        int me = 0; //me�� �׻� �Ʒ��� �־�� �ϹǷ�, �׿����� ó��.

        float rotation_degree_unit = 360 / all_player_count; // ���尡 ȸ���ؾ��� ���� ����
        int rotation_number = all_player_count - 1; // �̹� ���尡 ȸ���ؾ��� ������ ���� ����. ���������� �̸� ���ϸ� ��.
        

        for(int i = 0; i < all_player_count; i++)
        {
            if (All_Player[i].me)
            {
                me = i;
                continue;
            }

            GameObject board_each_tempt = Instantiate(Board_Each_Object,transform); // ������ �ڽ����� ����

            All_Player[i].Player_Board = board_each_tempt.GetComponent<Board_Each>(); //�÷��̾ ������ ������ �Է�

            //board_each_tempt.GetComponent<Board_Each>().player = All_Player[i]; // ���忡 �÷��̾� ���� �Է�.

            //around�� Ȱ������ ���� ȸ��
            board_each_tempt.transform.Rotate(new Vector3(0,0,1), rotation_degree_unit * rotation_number);

            Vector3 position_tempt = board_each_tempt.transform.position; // local��ǥ�� �ۼ��� ����.
            float rotation_angle = rotation_number * rotation_degree_unit; // ȸ�� �� = ���� �� * ���� Ƚ��
            rotation_angle *= Mathf.Deg2Rad;

            board_each_tempt.transform.position = new Vector3(position_tempt.x * Mathf.Cos(rotation_angle) - position_tempt.y * Mathf.Sin(rotation_angle), position_tempt.x * Mathf.Sin(rotation_angle) + position_tempt.y * Mathf.Cos(rotation_angle),0f);
            

            // ȸ�� : https://hwanggoon.tistory.com/16
            // ���Ϻ�ȯ ����
            // ���Ϻ�ȯ ���� : https://www.youtube.com/watch?v=arj7HrCe_r0

            /* ����ȭ ���
             * �ο����� ���� �ǹǷ� (��ȹ�� 2~5) ������ ���� cos, sin ���� �̸� �����ϰ� ������ �� �� ����. ���� Mathf�Ⱦ���. ������ �߻����� ����
             * �׷��� �� ������ ���� �����Ƿ� ���� �ʿ�� �����
             */

            /* ������ǥ ����
             * position�� �ҷ����� �ִ°� local�� �۵���. ���� ��ü �������� ��ġ�� �޶����� ������ �������� �� ������ ��
             * ���� �ȵȴٸ�, ������ǥ�� ���� ��ǥ��ǥ������ ���͸� ���� ��, �� ���͸� ȸ���� ��(�̷��� z�� ȸ���� ��), �ٽ� ���� ��ǥ�� ���� ���� �������� ��.
             */

            rotation_number -= 1; // �ѹ� ������ Ƚ�� ����(���� ����)

        }

        //me ����
        GameObject board_each_me = Instantiate(Board_Each_Object, transform);

        //board_each_tempt.transform.parent = transform; // ������ �ڽ����� ����

        All_Player[me].Player_Board = board_each_me.GetComponent<Board_Each>(); //�÷��̾ ������ ������ �Է�

        //board_each_me.GetComponent<Board_Each>().player = All_Player[me]; // ���忡 �÷��̾� ���� �Է�.
    }

    public void Board_Generation_Enemy(List<GameObject> Enemy_deck)
    {
        GameObject board_each_tempt = Instantiate(Board_Each_Object_enemy, transform);
        board_each_tempt.GetComponent<Board_Enemy>().Enemy_deck = Enemy_deck;

    }




    /* 
     * 
     * �� ī�� ������ ������ Enemy_Management�� ���� �ű⼭ ���� ���ʷ��̼� �Լ��� ȣ���ϴ� ���?
     * Enemy.cs�� �� ���� ���� ���� ����
     * 
     * �� �ǿ� ���� ������ ��ũ��Ʈ��, �ڽ��� �����ϰ�
     * �� �� ��ũ��Ʈ����, �ߵ� ī�忡 ���� Ʈ���Ÿ� ���� �ӹ� ����
     * Board_Enemy.cs �̷�������. board generation���� ��� ����
     * ���̵��� ���� ���� ���� �޾�, ���̵��� �´� �� ���� ������ ��.
     * 
     * 
     */


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 보드는 오브젝트 위치만을 결정한다.

public class Board_Management : MonoBehaviour
{
    public GameObject Board_Each_Object;

    public GameObject Board_Each_Object_enemy;

    public void Board_Generation(List<Player> All_Player)
    {
        int all_player_count = All_Player.Count; // 플레이어 수만큼 생성

        int me = 0; //me는 항상 아래에 있어야 하므로, 그에대한 처리.

        float rotation_degree_unit = 360 / all_player_count; // 보드가 회전해야할 단위 각도
        int rotation_number = all_player_count - 1; // 이번 보드가 회전해야할 각도를 위한 변수. 단위각도에 이를 곱하면 됨.
        

        for(int i = 0; i < all_player_count; i++)
        {
            if (All_Player[i].me)
            {
                me = i;
                continue;
            }

            GameObject board_each_tempt = Instantiate(Board_Each_Object,transform); // 보드의 자식으로 생성

            All_Player[i].Player_Board = board_each_tempt.GetComponent<Board_Each>(); //플레이어에 보드의 정보를 입력

            //board_each_tempt.GetComponent<Board_Each>().player = All_Player[i]; // 보드에 플레이어 정보 입력.

            //around를 활용하지 않은 회전
            board_each_tempt.transform.Rotate(new Vector3(0,0,1), rotation_degree_unit * rotation_number);

            Vector3 position_tempt = board_each_tempt.transform.position; // local좌표로 작성될 것임.
            float rotation_angle = rotation_number * rotation_degree_unit; // 회전 각 = 단위 각 * 남은 횟수
            rotation_angle *= Mathf.Deg2Rad;

            board_each_tempt.transform.position = new Vector3(position_tempt.x * Mathf.Cos(rotation_angle) - position_tempt.y * Mathf.Sin(rotation_angle), position_tempt.x * Mathf.Sin(rotation_angle) + position_tempt.y * Mathf.Cos(rotation_angle),0f);
            

            // 회전 : https://hwanggoon.tistory.com/16
            // 기하변환 공식
            // 기하변환 증명 : https://www.youtube.com/watch?v=arj7HrCe_r0

            /* 최적화 방법
             * 인원수가 제한 되므로 (기획상 2~5) 각도에 따른 cos, sin 값을 미리 선언하고 가져다 쓸 수 있음. 굳이 Mathf안쓰고. 오차도 발생하지 않음
             * 그런데 이 연산이 잦지 않으므로 굳이 필요는 없어보임
             */

            /* 월드좌표 관련
             * position을 불러오고 넣는게 local로 작동함. 따라서 전체 보드판의 위치가 달라져도 보드판 기준으로 잘 동작할 것
             * 만약 안된다면, 기준좌표로 부터 목표좌표까지의 벡터를 구한 후, 그 벡터를 회전한 뒤(이래야 z축 회전이 됨), 다시 기준 좌표로 부터 뻗어 나가도록 함.
             */

            rotation_number -= 1; // 한번 썼으니 횟수 차감(방향 조절)

        }

        //me 생성
        GameObject board_each_me = Instantiate(Board_Each_Object, transform);

        //board_each_tempt.transform.parent = transform; // 보드의 자식으로 생성

        All_Player[me].Player_Board = board_each_me.GetComponent<Board_Each>(); //플레이어에 보드의 정보를 입력

        //board_each_me.GetComponent<Board_Each>().player = All_Player[me]; // 보드에 플레이어 정보 입력.
    }

    public void Board_Generation_Enemy(List<GameObject> Enemy_deck)
    {
        GameObject board_each_tempt = Instantiate(Board_Each_Object_enemy, transform);
        board_each_tempt.GetComponent<Board_Enemy>().Enemy_deck = Enemy_deck;

    }




    /* 
     * 
     * 적 카드 보드판 생성은 Enemy_Management를 만들어서 거기서 여기 제너레이션 함수를 호출하는 방식?
     * Enemy.cs로 각 적에 대한 정보 저장
     * 
     * 적 판에 대한 별도의 스크립트와, 자식을 생성하고
     * 적 판 스크립트에서, 발동 카드에 대한 트리거를 통해 임무 수행
     * Board_Enemy.cs 이런식으로. board generation에서 모두 생성
     * 난이도에 대한 변수 값을 받아, 난이도에 맞는 적 판을 생성할 것.
     * 
     * 
     */


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildcard : Player_Card
{

    public override bool Using_Card(GameObject Enemy_object)//Player owner, Player_Management player_management, Board_Management board_management)
    {
        {
            Enemy enemy_tempt;
            enemy_tempt = Enemy_object.GetComponent<Enemy>();
            foreach(KeyValuePair<string,int> A in enemy_tempt.hp)
            {
                if (A.Value > 0)
                {
                    enemy_tempt.hp[A.Key] -= 1;
                    enemy_tempt.hp_all -= 1;
                    break;
                }
            }
            if (enemy_tempt.hp_all == 0)
            {
                enemy_tempt.alive = false;
            }
            return true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<GameObject> card_in_deck;

    public List<GameObject> card_in_sacrifice;

    public Card_N[] player_type;

    public Player_Type typetest;


    private void Awake()
    {
        if (gameObject.name.Equals("Player"))
            player_type = typetest.Red;
        else
            player_type = typetest.Green;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Origin
{
    Vector3 origin_pos;
    Quaternion origin_rotation;
    Vector3 origin_scale;
    
    public Card_Origin(Vector3 origin_pos, Quaternion origin_rotation, Vector3 origin_scale)
    {
        this.origin_pos = origin_pos;
        this.origin_rotation = origin_rotation;
        this.origin_scale = origin_scale;
    }
}

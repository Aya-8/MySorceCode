using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishController : MonoBehaviour
{
    private float flowSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(flowSpeed, 0, 0);　//皿を右に移動させる。


        if (transform.position.x > 10.0f)　//画面外に出たら皿を破壊する
        {
            Destroy(gameObject);
        }
    }

    public float FlowSpeed
    {
        set { flowSpeed = value; }
    }
}

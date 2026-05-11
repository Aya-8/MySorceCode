using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishGenerator : MonoBehaviour
{
    [SerializeField] GameObject katsuPrefab;
    [SerializeField] GameObject saradaPrefab;
    [SerializeField] GameObject curryPrefab;
    [SerializeField] GameObject ramenPrefab;
    [SerializeField] GameObject misosoupPrefab;
    [SerializeField] GameObject sobaPrefab;
    float span = 0.7f;
    float delta = 0;
    private float speed = 0.1f;
    private GameObject Dish;


    // Start is called before the first frame update
    void Start()
    {
        if(MenuShow.easy)
        {
            SetParameter(0.8f, 0.1f);
        }
        else SetParameter(0.5f, 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;

        if (this.delta > this.span)
        {
            this.delta = 0;
            SpawnDish(Random.Range(0, 6));
            
        }
    }

    //難易度別に皿の生成頻度と流れるスピードを調整
    void SetParameter(float span, float speed)
    {
        this.span = span;
        this.speed = speed;
    }

    //流れてくる皿をランダムに生成
    private void SpawnDish(int dice)
    {
        if (dice <= 0)
        {
            Dish = Instantiate(katsuPrefab);
        }
        else if (dice <= 1)
        {
            Dish = Instantiate(saradaPrefab);
        }
        else if (dice <= 2)
        {
            Dish = Instantiate(curryPrefab);
        }
        else if (dice <= 3)
        {
            Dish = Instantiate(misosoupPrefab);
        }
        else if (dice <= 4)
        {
            Dish = Instantiate(ramenPrefab);
        }
        else
        {
            Dish = Instantiate(sobaPrefab);
        }
        Dish.GetComponent<DishController>().FlowSpeed = this.speed;
    }
}

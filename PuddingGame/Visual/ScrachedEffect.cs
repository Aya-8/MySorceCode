using Cysharp.Threading.Tasks;
using UnityEngine;

public class ScrachedEffect : MonoBehaviour
{
    
    [SerializeField]
    private ColliderManager cm;
    [SerializeField]
    private Transform Pudding;
    [SerializeField]
    private GameObject smoke;

    private GameObject effect;

    private bool isScratched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        smoke.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cm.GetIsScratched() && (!isScratched))
        {
            ScrachedEffectAni().Forget();


        }

        if (smoke)
        {
            smoke.transform.position = Pudding.position + new Vector3(0, 1f, -2.2f);
        }
    }

    private async UniTask ScrachedEffectAni()
    {
        isScratched = true;
        smoke.SetActive(true);
        await UniTask.Delay(600);
        smoke.SetActive(false);
        isScratched = false;
    }
}

using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class counterEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject CounterEffect;
    [SerializeField]
    private PuddingCounter pc;
    [SerializeField]
    private GameObject Pudding;
    [SerializeField]
    private DistanceToMeasure dtm;
    [SerializeField]
    private ReactionManager rm;

    [SerializeField]
    private PuddingController pcnt;

    private GameObject effect;


    public async UniTask CounterEffectAni()
    {
        Vector3 effectPos = Pudding.transform.position + new Vector3(0, 5f, 0);
        effect = Instantiate(CounterEffect, effectPos, Quaternion.identity);
        effect.SetActive(true);
        await UniTask.Delay(2000);
        Destroy(effect);
    }

}
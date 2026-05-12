using UnityEngine;
using DG.Tweening;

public class PoseAnimation : MonoBehaviour
{
    [SerializeField]
    private Renderer pudding;
    [SerializeField]
    private ColliderManager cm;

    private bool isFreeze = false;
    private Material oriColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oriColor = pudding.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (cm.GetIsScratched() && !isFreeze)
        {
            isFreeze = true;
            freezeAnimation();
        }
    }

    private void freezeAnimation()
    {
        pudding.material.DOColor(Color.red,1f).SetLoops(6,LoopType.Yoyo);
        isFreeze = false;
    }
}

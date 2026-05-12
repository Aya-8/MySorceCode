using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class SignaturePoseAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform pudding;
    private Vector3 oriScale;

    private int time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oriScale = pudding.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ControllerKey.GetKeyDownDown())
        {
            if (Time.time > time)
            {
                time = (int)Time.time + 3;
                SignaturePose();

            }
        }

    }

    private void SignaturePose()
    {
        pudding.localScale = oriScale * 2;
        pudding.DOScale(oriScale,2f);
    }
}

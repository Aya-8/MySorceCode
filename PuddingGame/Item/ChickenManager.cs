using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class ChickenManager: MonoBehaviour
{
    [SerializeField]
    private GameObject egg;
    [SerializeField]
    private GameObject boy_normal;
    [SerializeField]
    private GameObject boy_angry;
    [SerializeField]
    private GameObject chicken;
    [SerializeField]
    private GameObject boy_emotion;

    private Vector3 originalPosition;
    private bool isharmed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = chicken.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerKey.GetKeyDownRight() || ControllerKey.GetKeyDownLeft())
        {
            if (isharmed) return;
                Emotion().Forget();
            
        }
    }

    public async void AttackChicken(SliderChanger sliderChanger)
    {
        if (isharmed) return;
        isharmed = true;
        chicken.SetActive(true);

        await Task.Delay(500);
        await chicken.transform.DOMove(new Vector3(-10f, 0f, 0f),0.5f)
            .SetLoops(4, LoopType.Yoyo)
            .AsyncWaitForCompletion();
        
        chicken.SetActive(false);
        chicken.transform.position = originalPosition;

        boy_angry.SetActive(true);
        sliderChanger.SetN(1f);
        boy_normal.SetActive(false);
        await Task.Delay(3000);
        boy_normal.SetActive(true);
        sliderChanger.SetN(0.5f);
        boy_angry.SetActive(false);
        isharmed = false;

    }

    private async UniTask Emotion()
    {
        boy_emotion.SetActive(true);
        boy_normal.SetActive(false);
        await Task.Delay(2000);
        boy_normal.SetActive(true);
        boy_emotion.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;


public class ReactionManager : MonoBehaviour
{
  
    [SerializeField] private GameObject nice;
    [SerializeField] private GameObject good;
    [SerializeField] private GameObject pain;
    private bool isPain = false;
    private bool isGood = false;


    private bool isReacting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nice.gameObject.SetActive(false);
        good.gameObject.SetActive(false);
        pain.gameObject.SetActive(false);

       
            Debug.Log($"[{Time.frameCount}] {name} Start  active={nice.activeSelf}", this);
        


    }

    public async void NiceReaction()
    {
        
        await UniTask.Delay(300);
        //isReacting = true;

        if (isPain == true)
        {
            isReacting = false;
            return;
        }
        if (isGood == true)
        {
            isReacting = false;
            return;
        }
        nice.gameObject.SetActive(true);
        
        await UniTask.Delay(1500);
        nice.gameObject.SetActive(false);
        isReacting = false;
    }

    public async void PainReaction()
    {
        isReacting = true;
        isPain = true;
        pain.gameObject.SetActive(true);

        await UniTask.Delay(1500);
        pain.gameObject.SetActive(false);
        isPain = false;
        isReacting = false;
    }

    public async void GoodReaction()
    {
        isReacting = true;
        isGood = true;
        await UniTask.Delay(300);
        if (isPain == true)
        {
            isReacting = false;
            return;
        }
        good.gameObject.SetActive(true);
        await UniTask.Delay(1500);
        good.gameObject.SetActive(false);
        isGood = false;
        isReacting = false;
    }

}

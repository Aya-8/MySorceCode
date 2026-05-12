using UnityEngine;
using UnityEngine.UI;

public class EffectPresenter : MonoBehaviour
{
    [SerializeField] private ParticleSystem starParticle;
    [SerializeField] private GameObject errorImage;
    
    [SerializeField] private InputChecker inputChecker;
    
    private Animator grugruAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grugruAnimator = errorImage.GetComponent<Animator>();
        grugruAnimator.enabled = false;
        errorImage.GetComponent<Image>().enabled = false;
    }

    void OnEnable()
    {
        inputChecker.OnCorrectKeyPressed += CorrectEffect;
        inputChecker.OnWrongKeyPressed += ErrorEffect;
    }
    
    private void CorrectEffect()
    {
        starParticle.Play();
    }

    private void ErrorEffect()
    {
        grugruAnimator.enabled = true;
        grugruAnimator.SetTrigger("Error");
    }
}


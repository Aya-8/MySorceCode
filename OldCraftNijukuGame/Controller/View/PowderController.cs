using UnityEngine;

public class PowderController : MonoBehaviour
{
    [SerializeField] private Animator bambooAni;
    [SerializeField] private ParticleSystem PowderParticles;
    [SerializeField] private UIController uiController;

    

    private Transform changedTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void OnEnable()
    {
        uiController.OnStartSequenceChanged += TurnContainer;
    }

    private void OnDisable() 
    {
        uiController.OnStartSequenceChanged -= TurnContainer;
    }

    private void TurnContainer(StartSequence step)
    {
        if (step == StartSequence.EnterPowder)
        {
            bambooAni.SetTrigger("Add");
        }
    }

    public void OffContainer()
    {
        this.gameObject.SetActive(false);
    }

    public void AddPower()
    {
        PowderParticles.Play();
    }

    
}

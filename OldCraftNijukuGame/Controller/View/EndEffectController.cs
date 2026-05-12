using System.Collections;
using Kapuro2025Winter.Scripts.Stiring.Director;
using UnityEngine;

/// <summary>
/// Phase.End に入った瞬間の「終わった感」演出用
/// </summary>
public class EndEffectController : MonoBehaviour
{
    [Header("Wiring")]
    [SerializeField] private PGDirector pgDirector;

    [Header("Camera Shake")]
    [SerializeField] private Transform cameraRoot; // Main Camera の Transform
    [SerializeField] private float shakeDuration = 0.25f;
    [SerializeField] private float shakeAmplitude = 0.08f;

    [Header("Particles")]
    [SerializeField] private ParticleSystem endParticle;

    [Header("Potの演出")]
    [SerializeField] private Transform potRoot;
    [SerializeField] private float squashDuration = 0.3f;
    [SerializeField] private Vector3 squashScale = new Vector3(1.08f, 0.85f, 1.08f);
    
    [Header("おわりのPanel")]
    [SerializeField] private GameObject finPanel;

    private Coroutine effectRoutine;
    private Vector3 camBasePos;
    private Vector3 potBaseScale;
    
    public event System.Action OnFinishEffect;
    public event System.Action OnNextEffect;

    private void Awake()
    {
        if (cameraRoot != null) camBasePos = cameraRoot.localPosition;
        if (potRoot != null) potBaseScale = potRoot.localScale;
    }

    private void OnEnable()
    {
        if (pgDirector != null)
            pgDirector.OnChangePhase += OnPhaseChanged;
    }

    private void OnDisable()
    {
        if (pgDirector != null)
            pgDirector.OnChangePhase -= OnPhaseChanged;

        if (effectRoutine != null)
        {
            StopCoroutine(effectRoutine);
            effectRoutine = null;
        }

        ResetTransforms();
    }

    private void OnPhaseChanged(Phase phase)
    {
        if (phase != Phase.End) return;

        if (effectRoutine != null)
            StopCoroutine(effectRoutine);

        effectRoutine = StartCoroutine(PlayEndEffects());
    }

    private IEnumerator PlayEndEffects()
    {
        // 1) Particle
        if (endParticle != null)
            endParticle.Play(true);

        // 2) 揺れ・潰れを同時に
        if (cameraRoot != null)
            StartCoroutine(CameraShake());

        if (potRoot != null)
            StartCoroutine(PotSquash());

        // 一番長い演出だけ待つ
        float wait = Mathf.Max(shakeDuration, squashDuration);
        yield return new WaitForSecondsRealtime(wait);

        ResetTransforms();
        effectRoutine = null;
    }

    private IEnumerator CameraShake()
    {
        float t = 0f;
        camBasePos = cameraRoot.localPosition;

        while (t < shakeDuration)
        {
            t += Time.unscaledDeltaTime;
            Vector3 offset = Random.insideUnitSphere * shakeAmplitude;
            cameraRoot.localPosition = camBasePos + offset;
            yield return null;
        }

        cameraRoot.localPosition = camBasePos;
    }

    private IEnumerator PotSquash()
    {
        potBaseScale = potRoot.localScale;

        float half = squashDuration * 0.4f;

        // squash
        for (float t = 0f; t < half; t += Time.unscaledDeltaTime)
        {
            float a = t / half;
            potRoot.localScale = Vector3.Lerp(
                potBaseScale,
                Vector3.Scale(potBaseScale, squashScale),
                a
            );
            yield return null;
        }

        // return
        float back = squashDuration - half;
        for (float t = 0f; t < back; t += Time.unscaledDeltaTime)
        {
            float a = t / back;
            potRoot.localScale = Vector3.Lerp(
                Vector3.Scale(potBaseScale, squashScale),
                potBaseScale,
                a
            );
            yield return null;
        }

        potRoot.localScale = potBaseScale;
        OpenEndCanvas();
        yield return new WaitForSecondsRealtime(1f);
        OnFinishEffect?.Invoke();
        yield return new WaitForSecondsRealtime(2f);
        OnNextEffect?.Invoke();
        yield return new WaitForSecondsRealtime(3f);
    }

    private void ResetTransforms()
    {
        if (cameraRoot != null)
            cameraRoot.localPosition = camBasePos;

        if (potRoot != null)
            potRoot.localScale = potBaseScale;
    }
    
    private void OpenEndCanvas()
    {
        Debug.Log("OpenEndCanvas!!!!!!!!");
        finPanel.SetActive(true);
    }
    
}

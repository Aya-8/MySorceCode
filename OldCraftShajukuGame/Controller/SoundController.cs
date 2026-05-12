using System.Collections;
using UnityEngine;
using FMODUnity;

namespace Kapuro2025Winter
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private StudioEventEmitter[] emitter;
        [SerializeField] private InputChecker inputChecker;
        [SerializeField] private TimeController timeController;
        [SerializeField] private UIController uiController;
        [SerializeField] private EndEffectController endEffectController;
        
        FMOD.Studio.VCA seVCA;

        private int correctCount;
        private int incorrectCount;
        
        void Start()
        {
            seVCA = RuntimeManager.GetVCA("vca:/SE");
            seVCA.setVolume(10);
            emitter[9].Play();
        }
        void OnEnable()
        {
            inputChecker.OnCorrectKeyPressed += PlayCorrectSE;
            inputChecker.OnWrongKeyPressed += PlayIncorrectSE;
            
            timeController.OnPhaseTimeReached += PlayMixOppositeSE;
            uiController.OnCountdownStarted += PlayCountDownSE;
            uiController.OnEnterPowder += PlaySodaSE;
            uiController.OnExplainGame += PlayExplainSE;
            uiController.OnExplainSoda += PlayExplainSodaSE;
            uiController.OnMixNabe += PlayMixNabeSE;
            
            endEffectController.OnFinishEffect += PlayFinishSE;
            endEffectController.OnNextEffect += PlayNextSE;
            
            uiController.OnStartSequenceChanged += SellectSE; //ついかしたやつ

        }

        void OnDisable()
        {
            inputChecker.OnCorrectKeyPressed -= PlayCorrectSE;
            inputChecker.OnWrongKeyPressed -= PlayIncorrectSE;
            timeController.OnPhaseTimeReached -= PlayMixOppositeSE;
            uiController.OnCountdownStarted -= PlayCountDownSE;
            uiController.OnEnterPowder -= PlaySodaSE;
            uiController.OnExplainGame -= PlayExplainSE;
            uiController.OnExplainSoda -= PlayExplainSodaSE;
            uiController.OnMixNabe += PlayMixNabeSE;
            endEffectController.OnFinishEffect -= PlayFinishSE;
            endEffectController.OnNextEffect -= PlayNextSE;

            uiController.OnStartSequenceChanged -= SellectSE; //ついかしたやつ
        }

        private void SellectSE(StartSequence sequence)
        {
            switch (sequence)
            {
                case StartSequence.ExplainGame: PlayExplainSE(); break;
                case StartSequence.ExplainSoda: PlayExplainSodaSE(); break;
                case StartSequence.MixPot: PlayMixNabeSE(); break;
                case StartSequence.Countdown: PlayCountDownSE(); break;
            }
        }
        
        private void PlayCountDownSE()
        {
            emitter[1].Play();
        }
        
        private void PlayMixNabeSE()
        {
            emitter[0].Play();
        }
        private void PlayMixOppositeSE(Phase phase)
        {
            if (phase == Phase.Left)
            {
                emitter[5].Play();
            }
        }
        
        private void PlayCorrectSE()
        {
            correctCount++;

            bool playSpecial = (correctCount % 19 == 0);

            if (playSpecial)
            {
                int index = Random.Range(2, 4);
                switch (index)
                {
                    case 2: emitter[2].Play(); break;
                    case 3: emitter[3].Play(); break;
                }
            }
        }

        private void PlayIncorrectSE()
        {
            int index = Random.Range(4, 6);
            switch (index)
            {
                case 4: emitter[4].Play(); break;
                case 5: emitter[5].Play(); break;
            }
        }

        private void PlaySodaSE()
        {
            emitter[6].Play();
        }

        private void PlayFinishSE()
        {
            Debug.Log("おわりSE");
            emitter[8].Play();
        }

        private void PlayExplainSE()
        {
            emitter[9].Play();
        }

        private void PlayNextSE()
        {
            emitter[10].Play();
        }

        private void PlayExplainSodaSE()
        {
            seVCA.setVolume(30);
            emitter[11].Play();
            seVCA.setVolume(10);
        }
        
    }
}



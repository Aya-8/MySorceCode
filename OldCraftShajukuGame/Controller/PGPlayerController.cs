using Kapuro2025Winter.Scripts.Stiring.Director;
using Kapuro2025Winter.Scripts.Stiring.Pole;
using UnityEngine;

namespace Kapuro2025Winter.Scripts.Stiring.Controller
{
    public class PGPlayerController : MonoBehaviour
    {
        private float time = 0f;
        
        [Header("一回の入力で何秒間棒が回転するか")] 
        [SerializeField] private float rotatingTime = 0.1f;
        [SerializeField] private PGDirector director;
        [SerializeField] private InputController inputController;
        [SerializeField] private ParticleSystem star;
        [SerializeField] private GameObject stickBase;
        [SerializeField] private CommandController commandController;

        private InputCommand currentInput;
        private float multi = 1.0f;
        [Header("フィーバータイムの回す速度")]
        public float x = 1.0f;

        private PoleMoveAnimation poleMove;
        private bool isStarted = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            time = 0;
            currentInput = InputCommand.None;
            poleMove = stickBase.GetComponent<PoleMoveAnimation>();
        }

        // Update is called once per frame
        void Update()
        {
            if (director.CurrentPhase() == Phase.Start) return;
            currentInput = InputCommand.None;
            time += Time.deltaTime;

            currentInput = commandController.CurrentInputCommand;
            
            multi = (director.CurrentPhase() == Phase.Both) ? x : 1.0f;

            if (currentInput != InputCommand.None)
            {
               MixPot(currentInput);
            }
        
            //時間で混ぜる棒の動きを変える
            if (time >= rotatingTime)
            {
                poleMove.IsMoving = false;
            }
        }

        //入力キーによって回す方向を決める
        private void MixPot(InputCommand input)
        {
            if (poleMove.IsMoving) return;
            poleMove.IsMoving = true;
            time = 0;
            if (input == InputCommand.Right)
            {
                poleMove.Angle = 3*multi;
            }

            if (input == InputCommand.Left)
            {
                poleMove.Angle = -3*multi;
            }

            if (director.CurrentPhase() == Phase.Both)
            {
                if (input == InputCommand.Right)
                {
                    poleMove.Angle = 3*multi;
                }

                if (input == InputCommand.Left)
                {
                    poleMove.Angle = -3*multi;
                }
            }
            
        
        }

        //入力しているキーを確認できるプロパティ
        // これここにあるのダメなんじゃない？
        public InputCommand CurrentInput
        {
            get { return currentInput; }
        }
    }
}

using Kapuro2025Winter.Scripts.Stiring.Director;
using UnityEngine;

namespace Kapuro2025Winter.Scripts.Stiring.Controller.View
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private PGDirector pgd;
        [SerializeField] private GameObject RArrow;
        [SerializeField] private GameObject LArrow;
        [SerializeField] private GameObject BRArrow;
        [SerializeField] private GameObject BLArrow;
    
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            RotateArrow(pgd.CurrentPhase());
        }
    
        //矢印を時間差で回転させる
        private void RotateArrow(Phase phase)
        {
            if (phase == Phase.Right)
            {
                LArrow.SetActive(false);
                BRArrow.SetActive(false);
                BLArrow.SetActive(false);
                RArrow.SetActive(true);
                RArrow.transform.Rotate(0,1,0);
            }
            else if (phase == Phase.Left)
            {
                RArrow.SetActive(false);
                BRArrow.SetActive(false);
                BLArrow.SetActive(false);
                LArrow.SetActive(true);
                LArrow.transform.Rotate(0,1,0);
            }
            else
            {
                RArrow.SetActive(false);
                LArrow.SetActive(false); 
                BRArrow.SetActive(true);
                BLArrow.SetActive(true);
                BRArrow.transform.Rotate(0,1,0);
                BLArrow.transform.Rotate(0,1,0);
            }
        }
   
    }
}

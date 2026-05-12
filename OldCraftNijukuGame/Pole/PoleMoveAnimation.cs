using UnityEngine;

namespace Kapuro2025Winter.Scripts.Stiring.Pole
{
    public class PoleMoveAnimation : MonoBehaviour
    {
        private Vector3 center = Vector3.zero;
        private Vector3 axis = Vector3.up;
        private float angle = 3f;

        private bool isMoving = false;
        private bool isStart = false;
    

        void Update()
        {
            if (!isMoving) return;
            transform.RotateAround(center,axis,angle);
        
        }

        public float Angle
        {
            set { angle = value; }
        }

        //混ぜる棒が動いているかのプロパティ
        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }
    }
}

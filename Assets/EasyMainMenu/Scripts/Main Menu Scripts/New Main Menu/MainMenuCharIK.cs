using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manoeuvre
{
    public class MainMenuCharIK : MonoBehaviour
    {
        public float degreesPerSecond = 90;
        public bool canRotate;
        public bool enableIK;
        public Transform LookAtTarget;

        Animator anim;
        Quaternion _rot;

        float mainRotationDelta;
        float LookatWeight;

        private void OnEnable()
        {
            if (_rot.eulerAngles == Vector3.zero)
                _rot = transform.rotation;

        }

        private void OnDisable()
        {
            transform.rotation = _rot;
        }

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();

            mainRotationDelta = transform.rotation.eulerAngles.y;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!enableIK)
                return;

            if (!LookAtTarget)
                return;

            anim.SetLookAtPosition(LookAtTarget.position);

            if (mainRotationDelta < _rot.eulerAngles.y + 60 && mainRotationDelta > _rot.eulerAngles.y - 60)
                LookatWeight = Mathf.MoveTowards(LookatWeight, 1, 0.025f);
            else
                LookatWeight = Mathf.MoveTowards(LookatWeight, 0.25f, 0.025f);

            anim.SetLookAtWeight(LookatWeight);
           
        }

        private void OnMouseDrag()
        {
            if (!canRotate)
                return;

            SmoothRotate();
        }

        private void OnMouseDown()
        {
            CancelInvoke("RotateBackToOriginal");
        }

        private void OnMouseUp()
        {
            Invoke("RotateBackToOriginal", 2f);
        }

        void SmoothRotate()
        {
            float rotX = Input.GetAxis("Mouse X") * degreesPerSecond * Mathf.Deg2Rad;

            transform.Rotate(Vector3.up, -rotX);

            mainRotationDelta = transform.eulerAngles.y;
        }

        public void RotateBackToOriginal()
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(LerpRotation());
        }

        IEnumerator LerpRotation()
        {
            float et = 0;
            canRotate = false;

            while (et < 1)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _rot, et / 1);

                et += Time.deltaTime;
                yield return null;
            }

            mainRotationDelta = _rot.eulerAngles.y;

            canRotate = true;
        }

    }
}
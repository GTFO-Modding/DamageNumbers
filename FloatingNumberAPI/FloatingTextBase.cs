using Player;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TMPro;
using UnityEngine;

namespace FloatingNumberAPI
{
    public class FloatingTextBase : MonoBehaviour
    {
        
        public FloatingTextBase(IntPtr intPtr) : base(intPtr) 
        {
            if (hooked) return;
            hooked = true;
            Camera.onPreRender += (Action<Camera>)((cam) => { OnPreRender(); });
        }
        TextMeshPro tmp;

        /// <summary>
        /// The gravity applied to the floating text, is multiplied by Vector3.down
        /// </summary>
        public float Gravity { get; private set; }

        /// <summary>
        /// The remaining lifetime of the text
        /// </summary>
        public float LifeTime { get; private set; }

        /// <summary>
        /// If the text has been setup or not
        /// </summary>
        public bool IsSetup { get; private set; } = false;

        /// <summary>
        /// What direction the text is going to move in
        /// </summary>
        public Vector3 Velocity { get; private set; } = Vector3.zero;

        private IFloatingTextInfo floatingTextInfo;
        private Vector3 internalPos;
        private Camera cam;
        private float baseFov;

        private static bool hooked = false;
        private static readonly List<FloatingTextBase> ActiveTexts = new List<FloatingTextBase>();
        private void OnPreRender()
        {
            for (int i = 0; i < ActiveTexts.Count; i++)
            {
                ActiveTexts[i].UpdatePosition();
            }
        }

        void Awake()
        {
            tmp = GetComponentInChildren<TextMeshPro>();
            cam = CameraManager.GetCurrentCamera();
            ActiveTexts.Add(this);
            baseFov = CellSettingsManager.SettingsData.Video.Fov.Value;
        }

        public virtual void Setup(IFloatingTextInfo info)
        {
            IsSetup = true;
            floatingTextInfo = info;
            Velocity = info.Velocity;
            Gravity = info.Gravity;
            LifeTime = info.LifeTime;
            SetText(info.Text, info);
            internalPos = info.SpawnPosition;
        }

        protected virtual void SetText(string text, IFloatingTextInfo info)
        {
            tmp.SetText(text);
            info.UpdateTextMesh(tmp);
            tmp.ForceMeshUpdate();
        }

        public virtual void UpdatePosition()
        {
            if (!IsSetup) return;

            LifeTime -= Time.deltaTime;

            if (LifeTime <= 0)
            {
                ActiveTexts.Remove(this);
                Destroy(gameObject);
                return;
            }

            //Animate
            Velocity += Gravity * Vector3.down * Time.deltaTime;
            internalPos += Velocity * Time.deltaTime;

            //Calc FOV diff
            float fovDiff = baseFov / cam.fieldOfView;

            //Keep position relative to camera
            Vector3 screenPos = cam.WorldToScreenPoint(internalPos);
            screenPos.z = ((screenPos.z > 0f) ? 1f : -1f) * fovDiff;
            transform.position = cam.ScreenToWorldPoint(screenPos);

            //Rotation
            Vector3 dir = (transform.position - cam.transform.position).normalized;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);

            floatingTextInfo.OnUpdate(tmp, this);
        }
    }
}

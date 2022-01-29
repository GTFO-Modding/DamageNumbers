using FloatingNumberAPI.API;
using Player;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace FloatingNumberAPI.Components
{
    //public class FloatingDamageNumbers : FloatingTextBase
    //{
    //    public FloatingDamageNumbers(IntPtr intPtr) : base(intPtr) {}
    //
    //
    //
    //    //PlayerAgent localPlayer;
    //    //float offset = 2;
    //    //float magnitude = 1.5f;
    //    //float gravity = 5f;
    //    //float lifeTime = 2;
    //    //Vector3 velocity;
    //    //void Awake()
    //    //{
    //    //    tmp = GetComponentInChildren<TextMeshPro>();
    //    //    localPlayer = PlayerManager.GetLocalPlayerAgent();
    //    //
    //    //    velocity = Vector3.Normalize(new Vector3(
    //    //            UnityEngine.Random.Range(-offset, offset),
    //    //            0f,
    //    //            UnityEngine.Random.Range(-offset, offset))
    //    //        ) + (Vector3.up * magnitude);
    //    //}
    //    //
    //    //public void Init(string text)
    //    //{
    //    //    tmp.SetText(text);
    //    //    tmp.ForceMeshUpdate();
    //    //}
    //    //
    //    //void Update()
    //    //{
    //    //    transform.LookAt(2 * transform.position - localPlayer.EyePosition);
    //    //    transform.Translate(velocity * Time.deltaTime, Space.World);
    //    //
    //    //    if (velocity.y > -1)
    //    //    {
    //    //        velocity.y -= gravity * Time.deltaTime;
    //    //    } else
    //    //    {
    //    //        velocity.y = -1;
    //    //    }
    //    //
    //    //    lifeTime -= 1 * Time.deltaTime;
    //    //    //tmp.faceColor.a = lifeTime;
    //    //
    //    //    if (lifeTime < 0)
    //    //    {
    //    //        Destroy(gameObject);
    //    //    }
    //    //}
    //}
}

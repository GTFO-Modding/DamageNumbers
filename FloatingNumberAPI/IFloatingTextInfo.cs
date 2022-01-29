using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace FloatingNumberAPI
{
    public interface IFloatingTextInfo
    {
        /// <summary>
        /// The initial velocity of the text
        /// </summary>
        Vector3 Velocity { get; }

        /// <summary>
        /// The position to spawn the text ats
        /// </summary>
        Vector3 SpawnPosition { get; }

        /// <summary>
        /// The amount of gravity to apply to the text every update
        /// </summary>
        float Gravity { get; }

        /// <summary>
        /// How long the text lasts for
        /// </summary>
        float LifeTime { get; }

        /// <summary>
        /// The text to display
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Custom settings not supported by RichText can be applied here (outlines, etc)
        /// </summary>
        /// <param name="tmp"></param>
        void UpdateTextMesh(TextMeshPro tmp);

        /// <summary>
        /// For any effect that should be applied over the lifetime of the text (fades, scale, etc)
        /// </summary>
        /// <param name="tmp"></param>
        /// <param name="textBase"></param>
        void OnUpdate(TextMeshPro tmp, FloatingTextBase textBase);
    }
}

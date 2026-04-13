#region

using UnityEngine;

#endregion

namespace Imperium.Util;

/// <summary>
///     Layers or animators and hashes of animations names.
/// </summary>
internal static class ImpAnimator
{
    internal static class Metarig
    {
        internal const string Layer_HoldingItemsBothHands = "HoldingItemsBothHands";
        internal static readonly int ShovelReelUp = Animator.StringToHash("ShovelReelUp");
        internal static readonly int HitShovel = Animator.StringToHash("HitShovel");
    }
}

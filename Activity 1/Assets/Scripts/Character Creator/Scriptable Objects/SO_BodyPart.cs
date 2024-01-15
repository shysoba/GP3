using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Body Part", menuName = "Body Part")]
public class SO_BodyPart : ScriptableObject
{
    // body part details
    public string bodyPartName;
    public int bodyPartAnimationID;

    // animation list
    public List<AnimationClip> allBodyPartAnimations = new List<AnimationClip>();
}
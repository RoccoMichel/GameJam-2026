using UnityEngine;

public class FakeAlembicPlayer : MonoBehaviour
{
    public FakeAlembicSource source;

    [SerializeField] private TimeValue startTime;
    [SerializeField] private TimeValue endTime;
    [SerializeField] private Vector3 scale = Vector3.one;
    [SerializeField] private Vector3 offset;

    [System.Serializable]
    public struct TimeValue
    {
        public bool @override;
        public float newTime;
    }

    [Header("Animation Settings")]
    [Tooltip("If animation should start from beginning when looping")]
    public bool straightLooping = true;
    [Tooltip("If animation should reverse back to 0 when looping")]
    public bool pingPongLooping;
    public float speedModifier = 1;


}

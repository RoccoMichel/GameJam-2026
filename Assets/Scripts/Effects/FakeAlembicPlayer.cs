using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class FakeAlembicPlayer : MonoBehaviour
{
    public FakeAlembicSource source;

    [SerializeField] private TimeValue startFrame;
    [SerializeField] private TimeValue endFrame;
    [SerializeField] private Vector3 scale = Vector3.one;
    [SerializeField] private Vector3 offset;

    [System.Serializable]
    public struct TimeValue
    {
        public bool @override;
        public float frame;
    }

    [Header("Animation Settings")]
    [Tooltip("If animation should start from beginning when looping")]
    public bool straightLooping = true;
    [Tooltip("If animation should reverse back to 0 when looping")]
    public bool pingPongLooping;
    public float speedModifier = 1;

    private MeshFilter meshFilter;
    private float time;

    private void Start()
    {
        if (source == null)
        {
            Debug.LogWarning($"FakeAlembicPlayer on {gameObject.name} is missing a source! All actions will fail.");
            Debug.Break();
        }

        GetComponent<MeshRenderer>().material = source.material;
        meshFilter = GetComponent<MeshFilter>();

        transform.localScale = scale;
        transform.localPosition += offset;

        if (startFrame.@override) time = startFrame.frame;
    }

    private void Update()
    {
        if (Mathf.Floor(time) >= source.meshes.Length && !straightLooping && !pingPongLooping) return;

        time += Time.deltaTime * source.frameRate * speedModifier;
        int index = Mathf.FloorToInt(time);

        if ((!endFrame.@override && Mathf.Floor(time) >= source.meshes.Length) || (!startFrame.@override && Mathf.Floor(time) < 0)
            || (endFrame.@override && Mathf.Floor(time) >= endFrame.frame) || (startFrame.@override && Mathf.Floor(time) < startFrame.frame))
        {
            if (straightLooping && !startFrame.@override) time = 0;
            else if (straightLooping && startFrame.@override) time = startFrame.frame;
            else if (pingPongLooping)
            {
                time = Mathf.Clamp(index, 0, source.meshes.Length - 1);
                speedModifier *= -1;
            }
        }

        meshFilter.mesh = source.meshes[Mathf.Clamp(index, 0, source.meshes.Length - 1)];
    }
}

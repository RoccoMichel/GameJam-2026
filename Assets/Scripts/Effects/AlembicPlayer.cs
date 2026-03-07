using System.IO;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

[RequireComponent(typeof(AlembicStreamPlayer))]
public class AlembicPlayer : MonoBehaviour
{
    [Header("Importing .abc settings")]
    [SerializeField] private string abcFileName;
    [SerializeField] private Material material;
    [SerializeField] private TimeValue startTime;
    [SerializeField] private TimeValue endTime;
    [SerializeField] private Vector3 scale = Vector3.one;
    [SerializeField] private Vector3 offset;

    [System.Serializable] public struct TimeValue
    {
        public bool @override;
        public float newTime;
    }

    [Header("Animation Settings")]
    public bool loop = true;
    [Tooltip("If animation should reverse back to 0 when looping")]
    public bool pingPongLooping;
    public float speedModifier = 1;
    private AlembicStreamPlayer ABCplayer;

    private void Awake()
    {
        ABCplayer = GetComponent<AlembicStreamPlayer>();

        // fix alembic file reference in build because unity hates .abc files
        if (string.IsNullOrEmpty(abcFileName)) return;
        string path = Path.Combine(Application.streamingAssetsPath, abcFileName);
        ABCplayer.LoadFromFile(path);

        if (startTime.@override) ABCplayer.StartTime = startTime.newTime;
        if (endTime.@override) ABCplayer.EndTime = endTime.newTime;

        transform.position += offset;
        transform.localScale = scale;
        foreach (MeshRenderer mr in gameObject.GetComponentsInChildren<MeshRenderer>()) mr.material = material;
    }
    private void Start()
    {
        if (speedModifier == 0) Debug.LogWarning("Speed Modifier is 0! No Animation will play.");
        if (pingPongLooping && !loop) Debug.LogWarning("Ping Pong Looping won't work if loop is false!");
    }

    private void Update()
    {
        if (ABCplayer.CurrentTime >= ABCplayer.Duration && !loop) return;

        ABCplayer.CurrentTime += Time.deltaTime * speedModifier;

        if ((ABCplayer.CurrentTime >= ABCplayer.Duration || ABCplayer.CurrentTime <= 0) && loop)
        {
            if (pingPongLooping) speedModifier *= -1;
            else ABCplayer.CurrentTime = 0;
        }
    }
}

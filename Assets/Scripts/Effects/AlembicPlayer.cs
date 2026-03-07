using System.IO;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

[RequireComponent(typeof(AlembicStreamPlayer))]
public class AlembicPlayer : MonoBehaviour
{
    [SerializeField] private bool importAsset = true;
    [Header("Importing .abc settings")]
    [Tooltip("file path if it is deeper nested in 'StreamingAssets'")]
    [SerializeField] private string pathExtension;
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
    [Tooltip("If animation should start from beginning when looping")]
    public bool straightLooping = true;
    [Tooltip("If animation should reverse back to 0 when looping")]
    public bool pingPongLooping;
    public float speedModifier = 1;
    private AlembicStreamPlayer ABCplayer;

    private void ImportAsset()
    {
        if (string.IsNullOrEmpty(abcFileName)) return;
        string path = Path.Combine(Application.streamingAssetsPath, pathExtension, abcFileName);
        ABCplayer.LoadFromFile(path);

        if (startTime.@override) ABCplayer.StartTime = startTime.newTime;
        if (endTime.@override) ABCplayer.EndTime = endTime.newTime;

        transform.position += offset;
        transform.localScale = scale;
        foreach (MeshRenderer mr in gameObject.GetComponentsInChildren<MeshRenderer>()) mr.material = material;
    }

    private void Awake()
    {
        ABCplayer = GetComponent<AlembicStreamPlayer>();

        // fix alembic file reference in build because unity hates .abc files
        if (importAsset) ImportAsset();
    }
    private void Start()
    {
        if (speedModifier == 0) Debug.LogWarning($"Speed Modifier is 0 on {gameObject.name}! No Animation will play.");
        if (straightLooping && pingPongLooping) Debug.Log($"{gameObject.name} can not have both looping modifiers on at once! straightLooping will override.");
    }

    private void Update()
    {
        if (ABCplayer.CurrentTime >= ABCplayer.Duration && !straightLooping) return;

        ABCplayer.CurrentTime += Time.deltaTime * speedModifier;

        if ((ABCplayer.CurrentTime >= ABCplayer.Duration || ABCplayer.CurrentTime <= 0))
        {
            if (straightLooping) ABCplayer.CurrentTime = 0;
            else if (pingPongLooping) speedModifier *= -1;
        }
    }
}

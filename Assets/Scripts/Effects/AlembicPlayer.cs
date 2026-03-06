using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

[RequireComponent(typeof(AlembicStreamPlayer))]
public class AlembicPlayer : MonoBehaviour
{
    public bool loop = true;
    [Tooltip("If animation should reverse back to 0 when looping")]
    public bool pingPongLooping;
    public float speedModifier = 1;
    private AlembicStreamPlayer ABCplayer;

    private void Start()
    {
        if (speedModifier == 0) Debug.LogWarning("Speed Modifier is 0! No Animation will play.");
        if (pingPongLooping && !loop) Debug.LogWarning("Ping Pong Looping won't work if loop is false!");

        ABCplayer = GetComponent<AlembicStreamPlayer>();
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

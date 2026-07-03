using UnityEngine;

[CreateAssetMenu(fileName = "FakeAlembicSource", menuName = "Scriptable Objects/FakeAlembicSource")]
public class FakeAlembicSource : ScriptableObject
{
    public Material material;
    public int frameRate = 24;
    public Mesh[] meshes;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ModulatorType")]
public class ModulationType : ScriptableObject
{
    public Material BeamMaterial;
    public int Voices;
    public int XAngle;
    public bool Reflective;
}

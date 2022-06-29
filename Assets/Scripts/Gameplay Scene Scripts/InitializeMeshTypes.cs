using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeMeshTypes : MonoBehaviour
{
    [SerializeField]
    private FlatUIButtons flatUIButtons;

    [SerializeField]
    private FlatMeshType flatMeshType;

    [SerializeField]
    private CylinderUIButtons cylinderUIButtons;

    [SerializeField]
    private CylinderMeshType cylinderMeshType;

    [SerializeField]
    private int meshTypeLimit;

    [SerializeField]
    private int meshTypeDistance;

    [SerializeField]
    private Transform flatStartPoint;

    [SerializeField]
    private Transform cylinderStartPoint;

    private void Start()
    {
        InitilizeMeshTypes();
    }

    private void InitilizeMeshTypes()
    {
        for (int i = 0; i < meshTypeLimit; i++)
        {
            InitMeshType(flatMeshType, flatUIButtons, flatStartPoint, i, 1);
            InitMeshType(cylinderMeshType, cylinderUIButtons, cylinderStartPoint, i, 11);
        }
    }

    public void InitMeshType(MeshType type, UIButtons uIButtons, Transform startPoint, int i, int loadTextureFrom)
    {
        var meshType = Instantiate(type);
        meshType.name = type.name + i.ToString();

        var collider = meshType.GetComponent<Collider>();

        meshType.transform.SetParent(startPoint, false);
        meshType.transform.position = new Vector3(startPoint.position.x, startPoint.position.y, startPoint.position.z
                                      + (i * meshTypeDistance));

        meshType.LoadMaterialTexture.LoadTexture(i + loadTextureFrom);
        meshType.UIButtons = uIButtons;
    }
}

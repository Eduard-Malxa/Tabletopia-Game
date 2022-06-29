using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeshType : MonoBehaviour
{
    [SerializeField]
    protected LoadMaterialTexture loadMaterialTexture;
    public LoadMaterialTexture LoadMaterialTexture => loadMaterialTexture;

    [SerializeField]
    protected MeshTypeMovement meshTypeMovement;
    public MeshTypeMovement MeshTypeMovement => meshTypeMovement;

    [SerializeField]
    protected UIButtons uIButtons;
    public UIButtons UIButtons { get => uIButtons; set => uIButtons = value; }
}

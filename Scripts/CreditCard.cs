using Godot;
using System;

public partial class CreditCard : RigidBody3D
{
    [Export]
    public NodePath frontVPNodePath, backVPNodePath, meshNodePath;

    private ViewportTexture _frontVPTexture;

    public ViewportTexture ViewportTexture {
        get {
            return _frontVPTexture;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        RenderingServer.FramePostDraw += OnFramePostDraw;
    }

    public void OnFramePostDraw()
    {
        SubViewport frontVP = GetNode<SubViewport>(frontVPNodePath);
        _frontVPTexture = frontVP.GetTexture();

        SubViewport backVP = GetNode<SubViewport>(backVPNodePath);

        MeshInstance3D mesh = GetNode<MeshInstance3D>(meshNodePath);

        mesh.SetSurfaceOverrideMaterial(0, CreateViewportMaterial(_frontVPTexture));
        mesh.SetSurfaceOverrideMaterial(1, CreateViewportMaterial(backVP.GetTexture()));

        RenderingServer.FramePostDraw -= OnFramePostDraw;
    }

    private StandardMaterial3D CreateViewportMaterial(ViewportTexture viewportTexture)
    {
        return new()
        {
            ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded,
            AlbedoTexture = viewportTexture
        };
    }

    public override void _InputEvent(Camera3D camera, InputEvent @event, Vector3 position, Vector3 normal, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left)
            {
                if (mouseButton.Pressed)
                {
                    GetTree().Root.GetNode<UserInterface>("ATM Scene/UserInterface").SpawnCreditCard(this);
                }
            }
        }
    }
}

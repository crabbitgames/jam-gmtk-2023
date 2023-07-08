using Godot;
using System;

public partial class CreditCard : Node3D
{
	[Export]
	public NodePath frontVPNodePath, backVPNodePath, meshNodePath;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RenderingServer.FramePostDraw += OnFramePostDraw;
	}

	public void OnFramePostDraw()
	{
		SubViewport frontVP = GetNode<SubViewport>(frontVPNodePath);
		SubViewport backVP = GetNode<SubViewport>(backVPNodePath);

		MeshInstance3D mesh = GetNode<MeshInstance3D>(meshNodePath);

		mesh.SetSurfaceOverrideMaterial(0, CreateViewportMaterial(frontVP.GetTexture()));
		mesh.SetSurfaceOverrideMaterial(1, CreateViewportMaterial(backVP.GetTexture()));

		RenderingServer.FramePostDraw -= OnFramePostDraw;
	}

	private StandardMaterial3D CreateViewportMaterial(ViewportTexture viewportTexture)
	{
		return new() {
			ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded,
			AlbedoTexture = viewportTexture
		};
	}
}

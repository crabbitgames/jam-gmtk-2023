using Godot;
using System;

public partial class PinDisplay : Node3D
{
	[Export]
	public NodePath viewportNodePath, meshNodePath;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RenderingServer.FramePostDraw += OnFramePostDraw;
	}

	public void OnFramePostDraw()
	{
		SubViewport viewport = GetNode<SubViewport>(viewportNodePath);

		MeshInstance3D mesh = GetNode<MeshInstance3D>(meshNodePath);

		mesh.SetSurfaceOverrideMaterial(
			0,
			new StandardMaterial3D() {
				ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded,
				AlbedoTexture = viewport.GetTexture()
			});

		RenderingServer.FramePostDraw -= OnFramePostDraw;
	}
}

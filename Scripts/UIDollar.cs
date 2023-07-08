using Godot;
using System;
public partial class UIDollar : UIProp
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		_lastViewportPosition = new(
			x: (float)GD.RandRange(0.01d, 0.2d),
			y: (float)GD.RandRange(0.25d, 0.7d)
		);

		MoveToLastViewportPosition();
	}
}

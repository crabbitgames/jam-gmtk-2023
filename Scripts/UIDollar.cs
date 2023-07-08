using Godot;
using System;
using System.Runtime.Serialization;
using System.Xml;

public partial class UIDollar : TextureRect
{
	private bool _followMouse = false;

	private Vector2 _lastViewportPosition;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LayoutMode = 1;

		_lastViewportPosition = new(
			x: (float)GD.RandRange(0.01d, 0.2d),
			y: (float)GD.RandRange(0.2d, 0.7d)
		);

		MoveToLastViewportPosition();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_followMouse)
		{
			Position = GetViewport().GetMousePosition() - PivotOffset;
		}
	}

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mb)
		{
			_followMouse = mb.Pressed;

			if (_followMouse)
			{
				Scale = Vector2.One * 3;
				ZIndex = 1000;

				AnchorLeft = AnchorRight = AnchorTop = AnchorBottom = 0;
				OffsetLeft = OffsetRight = OffsetTop = OffsetBottom = 0;
			}
			else
			{
				Scale = Vector2.One;
				ZIndex = 0;

				Position += PivotOffset / 3.0f;

				Vector2 viewportPos = new(
					x: Position.X / GetViewportRect().Size.X,
					y: Position.Y / GetViewportRect().Size.Y
				);

				Vector2 halfViewportSize = new(
					x: 0.5f * Size.X / GetViewportRect().Size.X,
					y: 0.5f * Size.Y / GetViewportRect().Size.Y
				);
				if (viewportPos.X > 0.01f - halfViewportSize.X &&
					viewportPos.X < 0.2f + halfViewportSize.X &&
					viewportPos.Y > 0.2f - halfViewportSize.Y &&
					viewportPos.Y < 0.7f + halfViewportSize.Y)
				{
					_lastViewportPosition = viewportPos;
				}

				MoveToLastViewportPosition();
			}
		}
	}



	public void MoveToLastViewportPosition()
	{
		AnchorLeft = AnchorRight = _lastViewportPosition.X;
		AnchorTop = AnchorBottom = _lastViewportPosition.Y;
		OffsetLeft = OffsetRight = -PivotOffset.X * 0.5f;
		OffsetTop = OffsetBottom = -PivotOffset.Y * 0.5f;
	}
}

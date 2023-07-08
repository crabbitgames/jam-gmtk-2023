using Godot;
using System;
public partial class UICreditCard : UIProp
{
    // Used to ... kinda .. bridge mouse input between input and guiinput... sort of...
    private bool _firstMouseUp = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        // Set a random position in case the player lets go of the credit card without setting a place for it.
        _lastViewportPosition = new(
            x: (float)GD.RandRange(0.01d, 0.2d),
            y: (float)GD.RandRange(0.25d, 0.7d)
        );
    }

    // This function is used to stop a bug where the credit card isn't correctly selected as
    // no GUIEvent has occured.
    public override void _Input(InputEvent @event)
    {
        if(_firstMouseUp)
        return;

        if(@event is InputEventMouseButton mb) {
            if(!mb.Pressed) {
                // Disable mouse tracking.
                _followMouse = false;

                // Reset position.
                ClickUp();

                // Stop this behaviour from happening again.
                _firstMouseUp = true;
            }
        }
    }

    public void Initialise(ViewportTexture creditCardTexture)
    {
        // Copies the image data from the viewport texture and sets the texture.
        Image img = creditCardTexture.GetImage();
        Texture = ImageTexture.CreateFromImage(img);

        _followMouse = true;

        ClickDown();
    }
}

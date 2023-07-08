using Godot;
using System;
public partial class UICreditCard : UIProp
{
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

    public void Initialise(ViewportTexture creditCardTexture)
    {
        // Copies the image data from the viewport texture and sets the texture.
        Image img = creditCardTexture.GetImage();
        Texture = ImageTexture.CreateFromImage(img);

        AnchorLeft = AnchorRight = AnchorTop = AnchorBottom = 0;
        OffsetLeft = OffsetRight = OffsetTop = OffsetBottom = 0;

        _followMouse = true;
    }
}

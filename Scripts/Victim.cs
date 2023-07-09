using Godot;
using System;

public partial class Victim : Node3D
{
    [Export]
    public NodePath animationPlayerNodePath;

    [Export]
    public PackedScene maleModelPackedScene, femaleModelPackedScene;

    public enum Sex
    {
        Male = 0,
        Female = 1
    };

    // Keeping track of sex for textures and names...
    public Sex sex;
    public string pinNumber, cardNumber;

	// References
	public GameHandler gameHandler;
    private AnimationPlayer _animationPlayer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animationPlayer ??= GetNode<AnimationPlayer>(animationPlayerNodePath);
        _animationPlayer.AnimationFinished += OnAnimationFinished;

        sex = GD.RandRange(0d, 1d) > 0.5d ? Sex.Female : Sex.Male;

        PackedScene modelPackedScene = null;

        switch (sex)
        {
            case Sex.Male:
                modelPackedScene = maleModelPackedScene;
                break;
            case Sex.Female:
                modelPackedScene = femaleModelPackedScene;
                break;
        }

        Node3D model = modelPackedScene.Instantiate<Node3D>();
        model.Position = new(0.0f, 0.65f, 0.0f);
        model.RotationDegrees = new(0.0f, -90.0f, 0.0f);

        AddChild(model);

        pinNumber = GenerateFourDigits();
        cardNumber = GenerateFourDigits() + "  " + GenerateFourDigits() + "  " + GenerateFourDigits() + "  " + GenerateFourDigits();
    }

    public void OnAnimationFinished(StringName animationName)
    {
        switch (animationName)
        {
            case "Anim_WalkUp":
				gameHandler.VictimApproached();
                break;
            case "Anim_WalkAway":
				QueueFree();
                break;
        }
    }

    private string GenerateFourDigits()
    {
        return Mathf.RoundToInt(GD.RandRange(0d, 9999d)).ToString("0000");
    }
}

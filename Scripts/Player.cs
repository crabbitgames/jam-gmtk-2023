using Godot;
using System;

public partial class Player : Node3D
{
	[Export]
	public int startMoney = 200;

	[Export]
	public NodePath userInterfaceNodePath;

	private int _money = 0;
	private float _tilt = 0;

	// References
	private UserInterface _userInterface;
	private Label _moneyAmountLabel;
	private Node3D _cameraController;
	private Camera3D _camera;
	private FastNoiseLite _noise;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Set references.
		_userInterface = GetNode<UserInterface>(userInterfaceNodePath);
		_moneyAmountLabel = _userInterface.GetNode<Label>("Money Amount");
		_cameraController = GetNode<Node3D>("CameraController");
		_camera = _cameraController.GetNode<Camera3D>("Camera");

		_noise = new FastNoiseLite() {
			NoiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth
		};

		// Update money from starting money.
		Money = startMoney;

		// Notify user interface to initialise.
		_userInterface.Initialise(Money);
		_userInterface.OnDownButtonPressed += TiltCamera;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Add bobbing to the camera.
		float t = Time.GetTicksMsec() * 0.005f;
		_camera.Position = new Vector3(
			_noise.GetNoise3D(t, 0, 0),
			_noise.GetNoise3D(0, t, 0),
			_noise.GetNoise3D(0, 0, t)
		) * 0.05f;

		// Rotate the camera based on the tilt.
		_cameraController.RotationDegrees = new(Mathf.Lerp(_cameraController.RotationDegrees.X, _tilt, (float)(delta * 10d)), -90f, 0f);
	}

	public int Money
	{
		get
		{
			return _money;
		}
		set
		{
			_money = value;
			_moneyAmountLabel.Text = "$" + _money.ToString();
		}
	}

	/// <summary>
	/// Tilts the camera down or up.
	/// </summary>
	/// <param name="direction">False = up, True = down</param>
	public void TiltCamera(bool direction) {
		_tilt = direction ? -20f : 0f;
	}
}

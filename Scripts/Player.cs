using Godot;
using System;

public partial class Player : Node3D
{
	[Export]
	public int startMoney = 200;

	[Export]
	public NodePath userInterfaceNodePath;

	private int _money = 0;

	private UserInterface _userInterface;
	private Label _moneyAmountLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Set references.
		_userInterface = GetNode<UserInterface>(userInterfaceNodePath);
		_moneyAmountLabel = _userInterface.GetNode<Label>("Money Amount");

		// Update money from starting money.
		Money = startMoney;

		// Notify user interface to initialise.
		_userInterface.Initialise(Money);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
}

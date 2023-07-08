using Godot;
using System;


public partial class UserInterface : Control
{
	[Export]
	public NodePath moneyHandlerNodePath;

	[Export]
	public PackedScene dollarTemplate;

	private MoneyHandler _moneyHandler = null;

	// Called on Player _Ready().
	public void Initialise(int amount)
	{
		_moneyHandler ??= GetNode<MoneyHandler>(moneyHandlerNodePath);

		int maxDenomination = 4;

		int displayedAmount = 0;
		while (displayedAmount < amount)
		{

			//Pick a random amount.
			MoneyHandler.MoneyAmount addAttemptAmount = MoneyHandler.RandomMoneyAmount(maxDenomination);

			//The random amount is allowed.
			if (displayedAmount + (int)addAttemptAmount <= amount)
			{
				// Add money on.
				displayedAmount += (int)addAttemptAmount;

				// Add new note to screen.
				UIDollar dollar = dollarTemplate.Instantiate<UIDollar>();

				dollar.Rotation = (float)GD.RandRange(0.0d, Math.PI * 2d);

				dollar.Texture = _moneyHandler.GetMoneyTexture(addAttemptAmount);

				AddChild(dollar);
			}
			//The random amount is disallowed, lower the maximum random denomination
			else {
				maxDenomination--;
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

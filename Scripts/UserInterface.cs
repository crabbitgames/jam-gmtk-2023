using Godot;
using System;
using System.Numerics;


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
				TextureRect dollar = dollarTemplate.Instantiate<TextureRect>();

				dollar.Position = new Godot.Vector2(
					x: Mathf.Round((float) GD.RandRange(0d, Size.X)),
					y: Mathf.Round((float) GD.RandRange(0d, Size.Y))
				);

				dollar.Texture = _moneyHandler.GetMoneyTexture(addAttemptAmount);

				AddChild(dollar);
				
				// TODO remove this.
				GD.Print(((int)addAttemptAmount).ToString() + " : " + displayedAmount.ToString());
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

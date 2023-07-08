using Godot;
using System;


public partial class UserInterface : Control
{
	[Export]
	public Texture2D upArrow, rightArrow, downArrow, leftArrow;

	[Export]
	public NodePath
		moneyHandlerNodePath,
		cashControlNodePath,
		plasticControlNodePath,
		downButtonNodePath;

	[Export]
	public PackedScene dollarTemplate, creditCardTemplate;

	private MoneyHandler _moneyHandler = null;
	private Control
		_cashControl = null,
		_plasticControl = null;

	private Button _downButton;
	private bool _downButtonState = false;
	public delegate void OnDownButtonHandler(bool direction);
	public event OnDownButtonHandler OnDownButtonPressed;

	// Called on Player _Ready().
	public void Initialise(int amount)
	{
		_moneyHandler ??= GetNode<MoneyHandler>(moneyHandlerNodePath);

		_cashControl ??= GetNode<Control>(cashControlNodePath);
		_plasticControl ??= GetNode<Control>(plasticControlNodePath);

		_downButton ??= GetNode<Button>(downButtonNodePath);
		_downButton.ButtonUp += OnDownButton;

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

				_cashControl.AddChild(dollar);
			}
			//The random amount is disallowed, lower the maximum random denomination
			else
			{
				maxDenomination--;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnDownButton()
	{
		_downButtonState = !_downButtonState;

		_downButton.Icon = _downButtonState ? upArrow : downArrow;

		OnDownButtonPressed?.Invoke(_downButtonState);
	}

	public void OnCashButton()
	{
		_cashControl.Show();
		_plasticControl.Hide();
	}

	public void OnPlasticButton()
	{
		_cashControl.Hide();
		_plasticControl.Show();
	}

	// Spawns a credit card on the UI and removes the physical credit card from the world.
	public void SpawnCreditCard(CreditCard creditCard) {
		// Switch to the plastic view.
		OnPlasticButton();

		// Spawn the UI credit card.
		UICreditCard uiCreditCard = creditCardTemplate.Instantiate<UICreditCard>();
		uiCreditCard.Initialise(creditCard.ViewportTexture);

		_plasticControl.AddChild(uiCreditCard);

		// Delete the credit card.
		creditCard.QueueFree();
	}
}

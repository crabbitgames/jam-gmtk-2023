using Godot;
using System;

public partial class GameHandler : Node
{
    [Export]
    public PackedScene victimPackedScene, creditCardPackedScene;
    public int day = 0;


	private double _elapsedTime = 0d;
    private Victim _currentVictim = null;
	private Timer _victimSpawnTimer;

    public override void _Ready()
    {
		_victimSpawnTimer ??= GetNode<Timer>("VictimSpawnTimer");
    }

    public override void _Process(double delta)
    {
        // Handle time passing.
		_elapsedTime += delta;

		if(_elapsedTime > 60d) {
			day++;
			_elapsedTime = 0d;
		}
    }

    public void VictimApproached()
    {
		// Put in credit card
		CreditCard creditCard = creditCardPackedScene.Instantiate<CreditCard>();
		AddChild(creditCard);

		// Display pin
    }

    public void OnVictimSpawnTimerEnd()
    {
		if(_currentVictim == null)
		{
			_currentVictim = victimPackedScene.Instantiate<Victim>();
			_currentVictim.gameHandler = this;
			AddChild(_currentVictim);

			_victimSpawnTimer.WaitTime = GD.RandRange(10.0f, 15.0f);
		}
    }
}

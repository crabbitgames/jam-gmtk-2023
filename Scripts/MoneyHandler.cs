using Godot;
using System;

public partial class MoneyHandler : Node
{
	[Export]
	public Texture2D moneyTexture_1, moneyTexture_5, moneyTexture_10, moneyTexture_20, moneyTexture_50;

	public enum MoneyAmount
	{
		One = 1,
		Five = 5,
		Ten = 10,
		Twenty = 20,
		Fifty = 50
	}

	public Texture2D GetMoneyTexture(MoneyAmount amount)
	{
		Texture2D selectedTexture = null;
		switch (amount)
		{
			case MoneyAmount.One:
				selectedTexture = moneyTexture_1;
				break;
			case MoneyAmount.Five:
				selectedTexture = moneyTexture_5;
				break;
			case MoneyAmount.Ten:
				selectedTexture = moneyTexture_10;
				break;
			case MoneyAmount.Twenty:
				selectedTexture = moneyTexture_20;
				break;
			case MoneyAmount.Fifty:
				selectedTexture = moneyTexture_50;
				break;
		}
		return selectedTexture;
	}

	public static MoneyAmount RandomMoneyAmount(int maxIndex)
	{
		return (MoneyAmount)Enum.GetValues<MoneyAmount>().GetValue(
			Mathf.RoundToInt(
				(float)GD.RandRange(0d, (double) Mathf.Clamp(maxIndex, 0, 4))
				)
			);
	}
}

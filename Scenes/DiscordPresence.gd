extends Node


# Called when the node enters the scene tree for the first time.
func _ready():
	discord_sdk.app_id = 1127417770666102895
	# print("Discord working: " + str(discord_sdk.get_is_discord_working()))
	discord_sdk.details = "Robbing victims, making ca$h."
	# discord_sdk.state = "Robbing folks."
	discord_sdk.start_timestamp = int(Time.get_unix_time_from_system())
	discord_sdk.refresh()

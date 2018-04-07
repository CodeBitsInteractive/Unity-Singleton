using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//	Global Game Model
//	@usage This is an example of global 
//  serializable model for game data. Use for 
//	saving the game data.
//
//	Developed by CodeBits Interactive
//	https://cdbits.net/
//=============================================
//=============================================
//	Game State Model
//=============================================
[System.Serializable]
public class gameState{
	// Just Global Game Params
	public bool is_first_run = true; // First run of the game
	public int game_diffucity = 1; // Game Diffucity (0 - easy, 1 - medium, 2 - hard)
	
	// The Player Params
	public string current_location = "MainCutscene"; // Current Player Location
	public float heal = 100F; // Current Player HP
	public float max_heal = 100F; // Max Player HP
	public float armor = 0; // Current Player Armor
	public float max_armor = 100F; // Max Player Armor
	
	// Player's weapons and inventory
	public int current_weapon = 0; // Current Player Weapon
	public List<bool> weapon_list; // Weapon Inventory List
	public List<bool> addons_list; // Player Addons List
	
	// Player's perks
	public int characteristic_points; // Player's available perks points
	public List<int> perks_list; // Player Perks List
	
	// The Player Location Params
	public Vector3 player_position; // Player Position
	public Quaternion player_rotation; // Player Rotation
	
	// Player Currency Params
	public double money = 1000; // Current Player Money
	public double diamonds = 5; // Current Player Diamonds
}
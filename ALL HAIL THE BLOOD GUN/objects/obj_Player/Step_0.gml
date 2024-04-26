// move vector
move_x = 0;
move_y = 0;
	
// movement
if (keyboard_check(ord("W")))
	move_y = -1 * move_speed;
if (keyboard_check(ord("S")))
	move_y = 1 * move_speed;
if (keyboard_check(ord("A")))
	move_x = -1 * move_speed;
if (keyboard_check(ord("D")))
	move_x = 1 * move_speed;
	
if (check_free(move_x, move_y))
{
	// application of movement
	x += move_x;
	y += move_y;
}

// set our position
global.player_x = x
global.player_y = y
global.player_x = x
global.player_y = y

// move speed
move_speed = 5;
move_x = 0;
move_y = 0;

// function to check if our place is free
function check_free(_prop_x, _prop_y)
{
	return !place_meeting(x + _prop_x, y + _prop_y, par_wall);
}
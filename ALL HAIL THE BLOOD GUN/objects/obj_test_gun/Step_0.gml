x = global.player_x;
y = global.player_y;

// rotate the gun towards our mouse at all times
direction = point_direction(x,y,mouse_x,mouse_y);
image_angle = direction;

// calculate shot position
shot_pos_x = x + lengthdir_x(32, direction);
shot_pos_y = y + lengthdir_y(32, direction);

// when we click shoot a bullet
if (!mouse_check_button(0))
{
	c_firerate += 1;
	
	if (c_firerate >= firerate)
	{	
		var b = instance_create_depth(shot_pos_x, shot_pos_y, depth, obj_test_bullet);
		b.direction = direction;
		
		c_firerate = 0;
	}
}
// when we hit something splatter into a bunch of droplets
nx = x + lengthdir_x(30,direction);
ny = y + lengthdir_y(30,direction);

if (place_meeting(x,y, par_wall) or place_meeting(nx,ny,par_wall))
{
	// make our droplets
	for (i = 0; i < droplets; i++)
	{
		instance_create_depth(x,y,depth,obj_test_droplet);
	}	
	
	// then destroy ourselves
	instance_destroy(id);
}
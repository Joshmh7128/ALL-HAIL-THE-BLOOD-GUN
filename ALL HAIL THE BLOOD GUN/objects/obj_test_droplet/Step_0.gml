// if we find a place that is occupied, remove this object and place a stationary droplet
if (!place_free(x,y))
{
	array_push(global.blood_x, x);
	array_push(global.blood_y, y);
	
	/*
	for (ix = 0; ix < size; ix++)
	{	
		for (iy = 0; iy < size; iy++)
		{
			// first check to make sure that this coordinate is not already taken
			if (!array_contains(global.blood_x, x + ix) && !array_contains(global.blood_y, y + iy))
			{			
				array_push(global.blood_x, x + ix);
				array_push(global.blood_y, y + iy);
			}
		}
	}
	*/
	instance_destroy(id);
}

// then apply drag
speed -= drag;

if (speed <= 0)
{
	for (ix = 0; ix < size; ix++)
	{	
		for (iy = 0; iy < size; iy++)
		{
			array_push(global.blood_x, x + ix);
			array_push(global.blood_y, y + iy);
		}
	}
	
	instance_destroy(id);	
}
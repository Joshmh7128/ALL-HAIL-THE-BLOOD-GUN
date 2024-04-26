speed = random_range(5, 20);
drag = 0.05;
direction = random_range(0, 360);
size = 1; // how big this droplet is

// if we are created and the place is already not free then destroy ourselves without a static
if (!place_free(x,y))
{
	instance_destroy(id);
}
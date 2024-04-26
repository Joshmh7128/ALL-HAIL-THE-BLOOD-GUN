draw_set_color(c_red);
// draw a red pixel for every entry in our blood data
for (ix = 0; ix < array_length(global.blood_x); ix++;)
{
	// draw_point(global.blood_x[ix],global.blood_y[ix]);
	draw_rectangle(global.blood_x[ix],global.blood_y[ix],global.blood_x[ix]+2,global.blood_y[ix]+2,false);
}
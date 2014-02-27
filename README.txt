README.TXT

MATT ADLER
260404212
COMP 521 A2

I zipped a folder of my entire project. I also included a unity package of the assignment. Run it through unity select the scene “CannonA2” and hit play.

Confessions / Notes:

The Wind: I use unity’s Mathf.PerlinNoise function to generate the random numbers for the wind. This calculation is done continuously even though the flags displaying the wind strength are discrete. Also note that, even though I have the perlinNoise for the wind in two separate scripts, that it is a static function in Unity and since I am calling with the same parameters (Time.time will be sufficiently close to be considered the same) that the banners (of the appropriate height) and ball will have the same wind effects on them.
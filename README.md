# FPSGame-Project
First person shooter prototype developed in unity editor. Created by Jere Puittinen.

My goal is to learn to make a shooter game prototype using a first person camera. For now, my aim is to create a simple AI for enemies that chase the player and attack back. I also need to create a way for hit detection when firing the gun, which lowers the health value of enemies until the value is below one, which is when the enemy game objects are destroyed. I will also create a main menu where the player can select to either quit the game or start a new one. The game should also go back to the menu when the player's health value goes below one.

I have already made a first person camera, where the camera is attached to the player and moves with the player model. The camera view turns with the computer mouse and moves in the game world with WASD keys. I have created a script for controlling enemy spawning that uses a collider as a trigger for creating enemy game objects. The script has float values for a timer that controls the delay between entering the collider and calling the spawn method. I can also change the interval at which the enemies are spawned. The spawner object and its collider are destroyed after a short delay when triggered as to prevent the player from spawning enemies continuosly by triggering the collider multiple times. The player is able to destroy enemies by walking into them. I made the player and some other objects into prefabs, so I can reuse them in new scenes without having to create a new one each time.

My next goal is to make the player able to jump and destroy enemies by pressing the left mouse button, when a crosshair is aligned with the target enemy.

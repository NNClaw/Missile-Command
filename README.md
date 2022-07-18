# Missile-Command

This is my take on replicating an Atari 2600 game "Missile Command". Although, this replication needs a lot of additional work, code refactoring, game balancing as in its' current state the game is...boring a bit and much more.

The goal of my replica is to survive as long as you can, until the life count drops to zero. The "rockets" (in quotes, because in current state they are blocks) will come from the sky to the ground and player should destroy those rockets with their own. Even though, the original game has a bit refined version of the goal, which is to protect 6 cities for as long as you can, but again, it's an unfinished product.

***Controls***

The main way to navigate and play the game is by using mouse and LMB (Left Mouse Button).

* To navigate a menu, use your mouse (currently, there is no keyboard support);
* In order to shoot your rockets, use LMB and mouse position on the screen;
* There is a pause button available, however the progress does not save between the gameplay and menu screen (at this time, save/load system has not yet been implemented)

***Gameplay conditions***

* The enemy "rockets" will fall down and the speed of spawning will increase every minute (might be changed later);
* The frequency of shooting is currently unlimited (yes, there should be a limit obviously);
* There is a score count for the player. Each destroyed "rocket" is 200 points;
* The game is over, when the life count is depleted and it is done by enemy "rockets" touching the ground.

*Unity version *2020.3.24f1*

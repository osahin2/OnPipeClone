# OnPipeClone


What i learned:
- Some Game Programming Patterns which are Singleton and Object Pooling
- About how to use Blender
- My first clone game so i learned how to finish game and what is polished game.
- Unity Physics and Collision.
- Using Raycast to solve fast objects collision problems.


Here Some Screenshots.


![Start](/Images/OnPipeCloneStart.PNG)


![InGame](/Images/OnPipeCloneInGame.PNG)


There are some performance issues in the game which are collectibles are not low poly and they all have physics. There was another bug in the game when we try to make smaller 
ring, ring was going in cylinders cause Unity Physics cant handle the speed of that. In that situation i solved this problem with Raycasts when i try to make smaller ring, send 
the raycast to cylinders from ring so he can know where to stop from the beginning. There may be another problem in the game when we do nothing and wait for a long time. Our ring 
will move in the Unity Scene so numbers grow and this can be a problem cause Unity Physics goes crazy when you go long distance in scene.

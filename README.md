# OnPipeClone
Onpipe clone game is created for my personal development. In this project i tried to understand endless game logic, used Object Pooling pattern for understand the pattern and 
solving performance issues.I also created coins in Blender so that was the other thing i learned in this game. There are some performance issues in the game which are collectibles 
are not low poly and they all have physics. There was another bug in the game when we try to make smaller ring, ring was going in cylinders cause Unity Physics cant handle the 
speed of that. In that situation i solved this problem with Raycasts when i try to make smaller ring, send the raycast to cylinders from ring so he can know where to stop from the 
beginning. There may be another problem in the game when we do nothing and wait for a long time. Our ring will move in the Unity Scene so numbers grow and this can be a 
problem cause Unity Physics goes crazy when you go long distance in scene,



![Start](/Images/OnPipeCloneStart.PNG)


![InGame](/Images/OnPipeCloneInGame.PNG)


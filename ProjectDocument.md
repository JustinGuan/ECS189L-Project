# Game Basic Information #

## Summary ##

Embers is an immersive and thrilling fantasy action game where you embody a mystical forest spirit that is entrusted with a crucial task of protecting the sacred flame that is the heart of the magical forest. The flame serves as a lifeline of the forest, warding off evil spirits that want to engulf this place with darkness. As the forest spirit, your primary objective is to gather wood around the forest to sustain the flame’s radiant power. However, the further you venture into the forest the more risks occur. The game’s narrative is set in a mystical world on the brink of darkness. The spirit's effort in trying to revitalize the forest by awaiting for the sun’s rays of light. Embers offers an engaging user experience by combining elements of exploration, resource management, and tactical combat. 

## Gameplay Explanation ##

Move with WASD or arrow keys

Space to jump

Shift to sprint

Left click to melee attack

Right click to shoot

Z to deposit wood in flame

Keep the flame alive.  The central flame has health that is continuously decreasing.  You must search the forest to collect wood.  You feed this to the flame to restore its health.  You lose the game if you or the flame die (Not implemented).  To win, you must keep the flame alive for a certain amount of time.

As you search for wood, enemies patrol the forest searching for you.  They will chase you once they see you, so you must stay out of sight or fight.  Your attack options are a melee attack and a magic projectile.  Good luck

# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## Producer

***Producer*** - As producer, I directed group meetings making sure we covered everything we needed to discuss.  We met about once a week and more frequently at the end of the quarter.  To give us direction, I used ChatGPT to generate a development timeline for our game.  We tailored this and used it to divide up the workload.  This timeline was included in our Initial Plan document, and I updated it as we completed tasks.  Later in the quarter, I also wrote a Progress Report for us.  It was cancelled as an assignment, but it turned out to be very helpful, so I wrote it anyway.  We also had one document of ideas and notes, which we created after forming our group: https://docs.google.com/document/d/1bNIOD9RLAmvgB8VPDfi_Uz9kx2FsmbONeRTFGd4-l48/edit?usp=sharing.

Fortunately, we had an amazing group, where everyone was committed to the project and there were no freeloaders; thus, my role as producer was a less important.  My biggest contributions were the enemy AI and the procedural world generation.

***Enemy AI*** – ChatGPT is incredible.  I had no idea how enemy AI works, but through a quarter-long conversation with ChatGPT I was able to figure it out.  I created a two-state system: Patrolling and Chasing.  The enemies use a NavMesh to travel to different destinations on the map.  By default, the enemies patrol by navigating to various patrol points around the map.  When a player gets too close, the enemy switches to chase mode and begins navigating toward the player.  Once the enemy is close enough, it will attack the player.

Early in the game development, I had this behavior working in a small test scene.  However, our world map and playable character were not ready for a long time, so I did not test it further.  Sadly, when we had all the necessary components (the final enemy and player models, the world map, and the enemy spawners), the enemy AI did not carry over.  The attack behavior work as intended, but the patrol and chase behaviors were broken.  I was unable to fix this, but the legends George and Hung were able to swoop in and get it working.  All behaviors are functional in the final game.

***Procedural Map Generation*** – Here also, I had no idea how this worked, but with ChatGPT I was able implement it into our game.  The world is generated according to the WorldGeneration.cs script.  The initial map is simply a flat square.  Then the script uses Perlin noise to adjust the height of each coordinate.  This essentially adds random deformations (hills) to the map.  Aside from the script logic, getting the hills to look natural required a lot of parameter fine-tuning.  The script uses the function GenerateObjects() to spawn objects (trees, mushrooms, etc.) throughout the world.

The final feature I added to the map was a NavMeshSurface for the enemy AI.  This was a little tricky since I the NavMesh could not be baked ahead of time.  The map is not generated until the game starts, so I had to bake it from within WorldGenerator.cs after the game started.  This allowed the NavMesh to conform to the hills of the terrain.

## User Interface

**Describe your user interface and how it relates to gameplay. This can be done via the template.**

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

## Animation and Visuals

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**

# Sub-Roles

## Cross-Platform

**Describe the platforms you targeted for your game release. For each, describe the process and unique actions taken for each platform. What obstacles did you overcome? What was easier than expected?**

## Audio

**List your assets including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.** 

## Gameplay Testing

Unfortunately, I was not able to conduct any play testing.

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**



## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**

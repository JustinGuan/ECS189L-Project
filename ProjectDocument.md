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

Keep the flame alive.  The central flame has health that is continuously decreasing.  You must search the forest to collect wood.  You feed this to the flame to restore its health.  You lose the game if you or the flame die.  To win, you must keep the flame alive for a certain amount of time.

As you search for wood, enemies patrol the forest searching for you.  They will chase you once they see you, so you must stay out of sight or fight.  Your attack options are a melee attack and a magic projectile.  Good luck

# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## Producer - Cenny Rangel

***Producer*** - As producer, I directed group meetings making sure we covered everything we needed to discuss.  We met about once a week and more frequently at the end of the quarter.  To give us direction, I used ChatGPT to generate a development timeline for our game.  We tailored this and used it to divide up the workload.  This timeline was included in our Initial Plan document, and I updated it as we completed tasks.  Later in the quarter, I also wrote a Progress Report for us.  It was cancelled as an assignment, but it turned out to be very helpful, so I wrote it anyway.  We also had one document of ideas and notes, which we created after forming our group: https://docs.google.com/document/d/1bNIOD9RLAmvgB8VPDfi_Uz9kx2FsmbONeRTFGd4-l48/edit?usp=sharing.

Fortunately, we had an amazing group, where everyone was committed to the project and there were no freeloaders; thus, my role as producer was a less important.  My biggest contributions were the enemy AI and the procedural world generation.

***Enemy AI*** – ChatGPT is incredible.  I had no idea how enemy AI works, but through a quarter-long conversation with ChatGPT I was able to figure it out.  I created a two-state system: Patrolling and Chasing.  The enemies use a NavMesh to travel to different destinations on the map.  By default, the enemies patrol by navigating to various patrol points around the map.  When a player gets too close, the enemy switches to chase mode and begins navigating toward the player.  Once the enemy is close enough, it will attack the player.

Early in the game development, I had this behavior working in a small test scene.  However, our world map and playable character were not ready for a long time, so I did not test it further.  Sadly, when we had all the necessary components (the final enemy and player models, the world map, and the enemy spawners), the enemy AI did not carry over.  The attack behavior work as intended, but the patrol and chase behaviors were broken.  I was unable to fix this, but the legends George and Hung were able to swoop in and get it working.  All behaviors are functional in the final game.

***Procedural Map Generation*** – Here also, I had no idea how this worked, but with ChatGPT I was able implement it into our game.  The world is generated according to the WorldGeneration.cs script.  The initial map is simply a flat square.  Then the script uses Perlin noise to adjust the height of each coordinate.  This essentially adds random deformations (hills) to the map.  Aside from the script logic, getting the hills to look natural required a lot of parameter fine-tuning.  The script uses the function GenerateObjects() to spawn objects (trees, mushrooms, etc.) throughout the world.

The final feature I added to the map was a NavMeshSurface for the enemy AI.  This was a little tricky since I the NavMesh could not be baked ahead of time.  The map is not generated until the game starts, so I had to bake it from within WorldGenerator.cs after the game started.  This allowed the NavMesh to conform to the hills of the terrain.

[My conversation with ChatGPT]([https://chat.openai.com/share/fb5411fb-17ab-4050-bfd0-4fa96058e08d](https://chat.openai.com/share/d8702232-02db-43a9-820f-4a9e7ce31e4f))

## User Interface

####Main Menu####
When the player first opens the game, the main menu shows up. It has a visual created by **Trina** that was shifted to the right of the screen, the title, Embers, and the Play and Quit button on the left of the image. 

The title was created on Figma. I wanted to give off the feeling of the glow of *fire* because the core gameplay of our game is to keep the fire alive, which is the heart of the forest, so I used two colors (yellow and orange) to make that glow. The Play button is also a shade of orange to follow the theme, and it is a different color from the Quit button because I want players to click towards *Play* more often than *Quit,* so I made it stand out more. 

When the player clicks *Play*, it takes them to the next scene in Unity’s build settings by [adding one](). When the player clicks *Quit*, the intended behavior is for it to [quit the application]().

[Here is the video I used as reference.](https://www.youtube.com/watch?v=pcyiub1hz20&list=PLFd_7LxN8iPbH5NqiMwIl96IyG6TIDnV0&index=2&t=653s).

#####HUD#####
These are elements that I deemed to be important to show at all times in order to help the player out when navigating through the game and its core gameplay. Here is a [link to the Figma to view the variants of each of the elements!](https://www.figma.com/proto/wX1A7DqmV5fxkBkhqliCo1/Embers-UI-Design?type=design&node-id=33-37&scaling=min-zoom&page-id=0%3A1)

####Player Health Bar####
There were several designs that I went through before deciding on the final health bar that is currently shown in our game. I played around with different ways of outlining the health bar and the heart icon. 

The health bar for the player is composed of three images–the background oat color, the border that surrounds the bar, and the heart itself. The bar was an image within unity, where I changed its *Image Type* to Filled horizontally to the left. There is also a text on how much health the player has out of 100 to prevent any guessing. The background oat color was also created in Figma.

The images were all created on Figma. The heart icon that I used is from a plugin within figma called Feather Icons. With the vector of the heart, I changed its color to red, since a red health bar is well known in games and would not cause any unnecessary confusion. I added a red outline for visibility. 

The intended behavior of the health bar is assigned the [maximum health of the player](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/PlayerHealthBar.cs#L12), and [the current health the player has](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/PlayerHealthBar.cs#L11). That is how the bar decides how much to fill using the [HealthBarFIller() function](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/PlayerHealthBar.cs#L48). For the smooth filling of the health bar, I used [lerp](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/PlayerHealthBar.cs#L50). For updating the text, I made a [TextMeshProGUI variable](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/PlayerHealthBar.cs#L9) to access its text, and appended the [current health to “/100”](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/PlayerHealthBar.cs#L33).

[Here is the video I used as reference.](https://www.youtube.com/watch?v=ZzkIn41DFFo&list=PLFd_7LxN8iPbH5NqiMwIl96IyG6TIDnV0&index=5).

####Wood Counting####
There were also several designs I went through in order to stay consistent with the design of the health bar. 

The wood counting UI is composed of 2 images–the wood icon, and the background oat color. Unfortunately, I could not find the original source of the wood icon, but I modified it by adding more lines through the wood as well as adding an outline similar to all of the other icons used in the HUD all through Figma. The background oat color was also created in Figma.

The behavior of the wood counting UI is that the text goes up as the player collects wood and decreases as the player deposits wood into the fire. That is kept track of by a [variable called branch](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/WoodCollision.cs#L8), and the logic for the [text updating is the same as player health bar](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/WoodCollision.cs#L37). 

The wood is detected through a [collision](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/WoodCollision.cs#L31), and when it collides, it gets [destroyed](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/WoodCollision.cs#L34), and the [branch variable updates](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/WoodCollision.cs#L35). 

[Here is the video I used as reference.](https://www.youtube.com/watch?v=6iSJ_jh6Rdo&list=PLFd_7LxN8iPbH5NqiMwIl96IyG6TIDnV0&index=1).

####Fire Health####
The fire icon is composed of two images–the fire icon and the background color. The icon I used is from [Flaticon](https://www.flaticon.com/free-icon/flame_426833?term=fire&page=1&position=3&origin=search&related_id=426833) and here is its [attribution](<a href="https://www.flaticon.com/free-icons/fire" title="fire icons">Fire icons created by Vectors Market - Flaticon</a>). Similar to all of the other icons, I added an outline around the icon through Figma. The background oat color was also created in Figma.

Originally, the fire health is supposed to look exactly like the player health bar, but with different colors to match the fire and its health shown in a percentage. However, due to time, I could not figure out the bug that prevents the bar from filling correctly, so I changed it to a UI similar to the wood counting. 

The fire’s health [depletes over time](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/FlameHealth.cs#L40), and as the player [adds wood by being near the fire](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/FlameHealth.cs#L43), the UI for the text changes along with the text for the wood. It detects that the player is near the fire through [collision](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/FlameHealth.cs#L74).

The logic for the [text updating is the same as player health bar](https://github.com/JustinGuan/ECS189L-Project/blob/56076fa56acdf6c6d53070cf9485f679dbb26843/Untitled%20Forest%20Game/Assets/Scripts/FlameHealth.cs#L61). 

[Here is the video I used as reference](https://www.youtube.com/watch?v=ZzkIn41DFFo&list=PLFd_7LxN8iPbH5NqiMwIl96IyG6TIDnV0&index=5).

#####Next Steps#####
Due to time constraints, some ideas I had were not able to be implemented. Here is what I would like to accomplish next as the UI developer for this project:

####Tutorial Screen####
I believe having a tutorial screen would be very helpful for new players playing the screen!

I did create a placeholder tutorial scene to be used for the tutorial, and my idea was to have a gif that plays to show the player how the main parts of our game works. For the gif/video playing idea in that scene, I [referenced from](https://www.youtube.com/watch?v=L6-vAtuN0h8&list=PLFd_7LxN8iPbH5NqiMwIl96IyG6TIDnV0&index=3) and [from](https://www.youtube.com/watch?v=VKckKdTdDJA&list=PLFd_7LxN8iPbH5NqiMwIl96IyG6TIDnV0&index=4).

####Update the Main Menu####
I would like to create a visually more appealing button design for the **Play and Quit buttons** and update the logo of our title, Embers. 

####Fixing the bug for the flame health####
Since the original flame health included a bar similar to the player health, I want to spend even more time figuring out how to make it fill properly. I did encounter a bug for updating the text when the player adds wood into the fire, where it would not update, but I was able to solve it.

 I believe the visual of the bar depleting and also the text would give the player a greater sense of urgency in keeping the flame alive. 

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

## Animation and Visuals - Trina Sagar

**List your assets including their sources and licenses.**
List your assets including their sources and licenses:
Skybox asset: https://assetstore.unity.com/packages/2d/textures-materials/sky/tgu-skybox-pack-96433 
Source: Babycake Studio
License agreement
Standard Unity Asset Store EULA : https://unity.com/legal/as-terms 
License type: Extension Asset
File size: 20.6 MB
Latest version: 1.0
Latest release date: Mar 13, 2018
Original Unity version: 2017.3.0 or higher


Projectile asset:
https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325#content 
Source: Unity Technologies


Cinemachine and Timeline are packages that are included in the unity engine


**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**
Besides the projectiles and the skybox, I created the models, animations, and textures for the game. The models and animations were created in Blender and the textures were created in Procreate. The art style is inspired by games such as Hollow Knight and Rain World and is meant to resemble a mystical forest untouched by humanity. It is meant to be serene and calming to look at. The style consists of a mostly dark color palette with glowing accents, such as the patterns under the tall trees and the patterns on the player’s clothing. When designing the models, I made them low-poly and gave them flat shading so that they looked simplistic and sharp. Most of the textures included darker backgrounds accented by brightly colored patterns. Overall, the visuals are meant to be alluring without being too overstimulating to look at. 
When designing the enemies, I wanted them to appear tall and thin in order to appear distinct from the player. I gave them simplistic bodies and masks so that they looked eerie without appearing awkward. The way they move is meant to look unnatural and aggressive, since they are evil spirits. The final boss is meant to resemble the enemies except its mask is more detailed and its body resembles a mermaid. It is meant to look more beautiful than the regular enemies as well as more powerful. It never touches the ground, making it appear otherworldly. 

## Input - Justin Guan

***Input Configuration***

Our game currently supports Windows and we have plans to expand its accessibility by implementing a console version that allows players to use a controller. In the current version, players can control the character's movement using either the WASD keys or the arrow keys, providing flexibility in input options. The character is capable of performing various actions, such as jumping with the space key and sprinting by holding down the shift key while moving.

To enhance the aiming experience, players can zoom in on the camera by holding the left control key, allowing for more precise targeting. The mouse input is utilized for additional actions: the left click button triggers a melee attack with the staff, while the right click button launches a projectile from the staff, adding diversity to the gameplay mechanics.


***Input implementation***

*Movement system*

The basic character movements are done in the [CharacterMovement.cs](https://github.com/JustinGuan/ECS189L-Project/blob/main/Untitled%20Forest%20Game/Assets/Scripts/CharacterMovement.cs) script which was developed by George. The aim left control is a script called [ActiveOnKeypress.cs](https://github.com/JustinGuan/ECS189L-Project/blob/main/Untitled%20Forest%20Game/Assets/Scripts/ActivateOnKeypress.cs) which checks if the left control key is pressed, it then triggers the Cinemachine third-person camera to transition into an aiming mode, providing players with enhanced precision.

*Combat system*

Our combat mechanics in the game encompass both melee attacks and projectile abilities, which have been intuitively mapped to the mouse controls for simplicity and ease of use.

To execute the melee attack, we have implemented the [melee.cs](https://github.com/JustinGuan/ECS189L-Project/blob/main/Untitled%20Forest%20Game/Assets/Scripts/Melee.cs) script. When the player presses the left mouse button, the attack method is triggered. This method checks if any enemies are within the staff's range and deal damage accordingly. By referencing the [Health.cs](https://github.com/JustinGuan/ECS189L-Project/blob/main/Untitled%20Forest%20Game/Assets/Scripts/Health.cs) script, the attack method assesses the enemy's health and destroys them if their health drops to zero or below.

For the projectile attack, we rely on the [ThirdPersonShooterController.cs](https://github.com/JustinGuan/ECS189L-Project/blob/main/Untitled%20Forest%20Game/Assets/Scripts/ThirdPersonShooterController.cs) script. By pressing the right mouse button, players activate the script, enabling them to fire a projectile in the direction of their crosshair. The position of the screen center is determined using a ray cast function, while the crosshair is designed using Unity's canvas to provide visual feedback on the target's location.

To facilitate the projectile mechanics, we utilize an iceball object that Trina implemented. The [Projectile.cs](https://github.com/JustinGuan/ECS189L-Project/blob/main/Untitled%20Forest%20Game/Assets/Scripts/Projectile.cs) script controls the speed and interactions of the projectile. If the projectile collides with an enemy or the ground, it is destroyed. When interacting with an enemy, the [Damage.cs](https://github.com/JustinGuan/ECS189L-Project/blob/main/Untitled%20Forest%20Game/Assets/Scripts/Damage.cs) script calculates and applies the appropriate damage.


## Game Logic - Hung Liu

***General Logic***

Overall, the game mainly runs through a script called SceneManager.cs. This script mainly handles the calling of the SceneManager.LoadSceneAsync() to transition our game into its different scenes (main menu, play menu, victory menu, and game over menu). The main menu, victory menu, and game over menu utilizes OnClick() functions, found within the button component in the inspector view of Unity, to help with the transitions within the game.
Within the level, I implanted a script LevelManager.cs to handle the win and lose conditions of the game. This required the script to keep track of player health, a timer with unknown time (gives a sense of 
difficulty because player doesn’t know how long left to survive), and the health of the fire. Depending on the condition, this script would reference SceneManager.cs to load different scenes.


***Spawning Wood***

An essenntial game mechanic is the feeding of wood into the fire. As such, we need to spawn wood into the game. The code, WoodSpawner.cs, functions by first generating n rings around the center of the map. These rings have a minimum and maximum radius to determine between which two distance from the fire are the woods able to spawn. From there it generates a random value for theta and our distance from the fire, but still within the min and max radius. Theses random values will then act as the new spawn point for our wood. To incentivize the player to stray further away from the fire, more wood is generated in the outer rings. The spawning of the wood also acts similar to our Exercise 3 (Pikmini Spawner) but with a bit more complexity to it.

***Spawning Enemies***
I had also created a system that allows for the spawning of enemies (SpawnEnemy.cs). To keep the code KISS, I simply used the fire as our central point, and created enemy spawners (GameObject) that are located at the vertices of a pentagon. All enemy spawners that are created function the same with a minor tweak to the type of enemy being spawned. These spawners utilize a radius (set at a quarter of the map’s size) that determines where within the circle are these enemies allowed to spawn. In doing so, some regions of the map will spawn two and maybe even three different types of enemies. These enemies are 
also instantiated as a child of the spawner to simplify their behavior in the next section. The spawning of the enemies functions similarly to the Pikmini spawner from exercise 3.

***Enemy AI***

Towards the final weeks of the project, I had begun to work with Cenny, a bit, on the enemy AI, primarily the issue with getting the enemies to patrol properly. The issue that we had found was the NavMeshSurface being generated incorrectly. We had generated the NavMeshSurface pre-runtime with the “World Generator” game object as our surface. This had resulted in the NavMeshSurface becoming a flat plane, and as result, none of our enemies (NavMeshAgent) was being recognized. This issue was fixed simply by re-baking the NavMeshSurface after the terrain has been generated (discovered by Cenny as soon as this issue was pointed out).
For the specific enemy behavior that I had helped Cenny with was the patrol behavior. At that point, Cenny had already written out the complete logic for the patrolling behavior of the enemies. I had simply set up the patrol points where the enemy would patrol. As stated earlier, the enemies are a child of the spawner they come from, which allows the patrol points to be set up easily. As of now, the patrol points are set at the vertices of a square, with the parent as the center. These points are also altered depending on the fire’s radius, preventing enemies from patrolling near the fire. The code, found within PatrolBehavior.cs, can be altered easily to increase or decrease the number of patrol points.

***Future Additions***

Our team had an idea where, as the fire grew weaker, the radius of the fire would drop. As the radius decreases, the enemies would get closer to the fire, threatening the player a lot more. Another idea that failed to make it into the final product was the usage of fairy rings to heal the player as they are within it. This would give the player an effective way to heal the damage they have taken. Unfortunately, neither made it into the game due to time constraints and the solving of other crucial bugs. Because of that, adding this mechanic would make the game more challenging.


# Sub-Roles

## Cross-Platform

**Describe the platforms you targeted for your game release. For each, describe the process and unique actions taken for each platform. What obstacles did you overcome? What was easier than expected?**

## Audio - Justin Guan

***Background Music***

To create an immersive atmosphere throughout the entire game map, a carefully selected [forest sound](https://pixabay.com/sound-effects/forest-with-small-river-birds-and-nature-field-recording-6735/) was chosen as the background music. This choice was made to align with the game's theme and enhance the player's overall experience. The forest sound is integrated into the game by assigning it to an audio source component within the World Generator game object. By utilizing an audio source, the background music plays continuously, ensuring consistent and captivating gameplay.

***Sound Effects***

To enhance specific game elements and interactions, distinct sound effects were carefully implemented. Notably, the campfire, which serves as the main objective in the game, features a unique sound effect. When the player character is within range of the campfire, a captivating [campfire sound](https://pixabay.com/sound-effects/campfire-crackling-fireplace-sound-119594/) immerses the player in the warmth and crackling flames of the campfire itself. Furthermore, an [firing sound effect](https://pixabay.com/sound-effects/fire-magic-6947/) accompanies the release of the ice projectile from the staff. This sound effect provides auditory feedback to the user, signaling the moment when the projectile is being launched.

***Audio Manager***

To streamline and manage the audio elements efficiently, an [Audio Manager script](https://github.com/JustinGuan/ECS189L-Project/blob/main/Untitled%20Forest%20Game/Assets/Scripts/AudioManager.cs) was created. This script acts as a centralized hub for audio control, utilizing the properties defined in the [Sound.cs](https://github.com/JustinGuan/ECS189L-Project/blob/main/Untitled%20Forest%20Game/Assets/Scripts/Sound.cs) script to create instances of audio sources. Through the implementation of the Audio Manager, playing music tracks and sound effects becomes a straightforward task using the provided methods, such as PlayMusicTrack and PlaySoundEffect. Additionally, the StopSoundEffect method allows for the convenient cessation of specific sound effects when required.

***Future Additions***

We plan to implement more music and sound effects in the future as the game is still not fully developed.


## Gameplay Testing - Cenny Rangel

Unfortunately, I was not able to conduct any play testing.

## Narrative Design - Trina Sagar

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 
The bunny-like creatures were the original inhabitants of the forest, but they were threatened by evil spirits. The player is a benevolent spirit sent to protect the forest by keeping a flame alive at the forest’s center until the enemies are vanquished. The player must collect wood to keep the flame alive, and the absence of a map as well as the display of the fire's size is meant to make the game feel more intense.
In the opening cutscene (located in a scene named “Opening”), the player is seen battling an evil spirit in order to protect one of the creatures.
After keeping the flame alive, the player has to fight the final boss. The cutscene that plays before fighting the final boss is located in a scene called “Boss_cutscene”. The cutscenes are meant to be dramatic and motivate the player to protect the forest. 

## Press Kit and Trailer

[Link of the press kit.](https://www.figma.com/proto/bps5q7KrN68TXJorjc8XPU/Embers-Press-Kit?type=design&node-id=0-5&scaling=min-zoom&page-id=0%3A1)

[Link of the trailer.](https://youtu.be/n0eca8oGFrA)

When making the trailer, I wanted to showcase the core gameplay mechanics, so I put in shots of the actual gameplay. I also wanted to showcase the visuals of our game that **Trina** created, so I put in clips she made that showcased that. 

For the press kit, I designed it based on the colors of our main menu and used icons that I utilized in our game such as the wood and heart. I chose those specific screenshots because I believe that one of our strong points is the visuals of the world, and I wanted to showcase the main spots of the fire, wood, and the enemy. 

## Game Feel - Hung Liu

As found within the summary of our game, Embers is set within a mystical forest. As a result, the color schemes we had decided on tend to be a bit more vibrant. The vibrant colors of the player, trees, etc. contrast greatly with the sky and enemies that spawn. Enemies feature a darker color scheme that allows players to easily identify its hostility towards the player. The ominous sky also contrasts with the other environment to indicate that a problem is occurring within the forest. We had also decided to utilize a 3rd person camera to assist the player as they play the game. This effectively increases their FOV, and may assist in noticing enemies coming from behind them.

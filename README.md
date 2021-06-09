# Hotdog Man's Pandemic Pandemonium #

## Summary ##


Hotdog Man's Pandemic Pandemonium is a sandbox walking simulator that you can navigate through beautiful landscape as well as memories in the pandemic year. The darkness and vapidity in the greyscale world reflects the pandemic year sentiment and also echos the assigned game theme "power down". The obstacles in the journey to the lighthouse represents our assidious fight agianst the pandemic and the chromatic characters in a grey scale world highlight the strength we gain from each other. The lighthouse ending expresses our prospect of a "powering up" from the pandemic year.

#### Terrain:
Contributor: Yi Xie
Generated with [Gaia](https://www.procedural-worlds.com/support/)
Modified With Terrain Tool by Unity

#### Online Feature:
Contributor: Xiyu Zhang, Yibo Yan, Yi Xie
> Check out [Synchronization Section](#Synchronization) of Game Logic

#### Scene Setup/Mise-en-Scene:
Contributor: Jenna Zing, Yibo Yan, Yi Xie

#### Third Party Package Usage:
Gaia Pro 2021 - PROCEDURAL WORLDS

PUN 2 - EXIT GAMES

Ultimate Character Controller - OPSIVE

PUN Multiplayer Add-ON - OPSIVE

DreamOS - MICHSKY

POLYGON - Dungeon Pack - SYNTY STUDIOS

POLYGON - City Characters Pack - SYNTY STUDIOS

POLYGON - Nature Pack - SYNTY STUDIOS

POLYGON - Samurai Pack - SYNTY STUDIOS


#### Art Citation
TurboSquid:
>[Picture Frame](https://www.turbosquid.com/3d-models/picture-frame-obj-free/832657)
[Light Tower](https://www.turbosquid.com/3d-models/lighthouse-old-light-model-1689804)

## Gameplay Explanation ##

### Basic Movement
**Movement** - W, A, S, D / Arrow Key

**Adjust Camera Angle** - Mouse

**Jump** - Space

**Attack** - Left Mouse Click

**Zoom-in** - Right Mouse Click

**Call out Cursor** - ESC

### -- Special --
**Generate Light Particle:**
> Keyboard `G` in Boss Area

**Switch Control:**
> You can click on the switch control button to switch between the cursor and character controller.
 
**Notification:**
> You can check out the notification by click on the bell icon at the bottom right.
 
**Widget Library:**
> You can toggle on and off the widget in the widget library


# Main Roles #

**REMARK: Given the nature of our game, we found it'd be much easier to split the roles in terms of different plots in the game. Therefore, while we tried our best to stick to the role division scheme below, every member was involved in the following main roles more or less.**

## User Interface

The game is a walking simulator about pandemic year, I think it is important to make the UI a metaphor of Pandemic Life. 

The User Interface is base on a third party library:
[DreamOS](https://assetstore.unity.com/packages/tools/gui/dreamos-complete-os-ui-188483)


The third party library give me a good template to work on and I did a lot of modification to the UI in order to fit our game.

*Current Quest*: A script that catches player progress and update the current quest to the UI element. 
[ChangeNote.cs](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/UI/ChangeNote.cs)

*Cursor Hider*: A event base script that hide the cursor and keep the cursor stay in the middle of the screen. 
[MouseControl.cs](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/UI/mouseControl.cs)

*MiniGame Button:* In a specific time in the game we want the player play a minigame to unlock a new area. I wrote this script to make the minigame button not interactable before the player get there. The flag of change is a trigger in the front of the minigame area. The icon and the name of the application will be changed base on the progress of the player. [MiniGameButtonController.cs](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/UI/MiniGameButtonController.cs)

*Splash Screen:* A series of Splash Screen that on the Launch of the game. 
[SplashFadeOut.cs](https://github.com/YiLoganXIe/ECS189L-Final/blob/main/PartyTime/Assets/Script/UI/SplashFadeOut.cs)

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

Since according to our game theme, we are constructing a magical world, some of the game objects do not follow the standard physics model. These objects include light particles, the boss monster, and the trigger stones. Also, to achieve different magical effects, we explore with colliders and rigidbody by going through documents and tutorials, since this part is not discussed in detail in class. 

- Light Particles: The light particles are not affected by gravity in this world. They are floating in the air. They are also intangible so that the players can collide with them to collect. When a player drops a light particle, the particle is placed in the middle of the air right before the player.
    - No gravity. Intangible: This is done by deleting the rigidbody of the light particles.
    - [Particle Dropping with Floating](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/Player/PlayerController.cs#L46)

- The Boss Monster: The boss monster will be moving slowly in the sky free from gravity. The boss will also absorb game objects up to the sky once it is right above the objects. The movement of these objects to the sky is accomplished using linear interpolation taught in class. While these absorbed game objects are influenced by gravity, the absorbing effect is strong enough to defeat the gravity. After being absorbed to the sky, these game objects will be hovering and colliding with each other under the boss. After the boss is destroyed, these game objects will fall back to the ground under gravity.
    - [Boss Movement in the Sky](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/Boss/BossMovement.cs)
    - Object Detection: This is done by adding to the boss a long vertical capsule collider that extends from the sky to the terrian with its "is Triggered" field checked.
    - [Boss Absorbing Behavior](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/Boss/BossAbsorbingController.cs) 

- Trigger stones: The trigger stones are floating with bumping up and down. 
    - [Trigger Stones Floating Movement](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/Trigger/FloatStoneTrigger.cs)

## Animation and Visuals

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**


## Input

**Describe the default input configuration.**
Since [Game] is currently PC-only, we derive user input from the mouse and keyboard.  The default input configuration used by the Input Manager is as follows:

| **Input Axes** | **Key Mappings** |
| -------- | -------- |
| Character Movement | w, a, s, d and arrow keys |
| Character Jump | space bar |
| Character Spawn Particle | g |
| Call out Cursor | ESC |
| Adjust Camera Angle | Mouse |
| Character Attack | Left Mouse Click |
| Camera Zoom-In | Right Mouse Click |
| Desktop Interaction | Left Mouse Click / Mouse Movement |

## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**

Contributor: All Team Members

### Synchronization 

- Player Synchronization

  All players' movements, activities are managed by a 3rd-party library --- `UltimatePlayerController`. So the internal state is directly managed by that library. Player spwan script can be found [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/main/PartyTime/Assets/Opsive/UltimateCharacterController/Add-Ons/Multiplayer/PhotonPUN/Scripts/Game/SingleCharacterSpawnManager.cs).
  
- Player Spawn in Different Places Mechanism

  Since the internal spawn logic is managed by `UltimateCharacterController` directly, so in order to be able to spawn four players into different places, we have to do some modification on the source code of that libarary. The modification can be found [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/90eab14bda76c0a9bb77e9674824ac20007f7e10/PartyTime/Assets/Opsive/UltimateCharacterController/Add-Ons/Multiplayer/PhotonPUN/Scripts/Game/SpawnManagerBase.cs#L211). What we did is when the `OnPlayerEnterRoom` function is triggered, we calculate how many players in the room and set the spawn point to the position we pre-defined.
  
- Light Particle Destroy Synchronization

  The destroy of light particle need to be synchronized across all clients. This is done another 3rd-party library called `Photon` or sometimes called `Pun`. Each light particle is attached with a `PhotonView`, which will tell the pun to manage the state.
  
- Suckable Light Particle Synchronization

  The placement of suckable light particle is also synchronized across all clients. The detail can be found [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/Player/PlayerController.cs#L31).
  
  
> There are actually lots of other synchronizations going on in this project, but they are too complicated and verbose to explicitly list them all out here.
  
---
  

- Player will be spawned in different places

  There are four spawn places in the game, when four players enter the room, they will be four different places to find each other. The logic implementation is [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/90eab14bda76c0a9bb77e9674824ac20007f7e10/PartyTime/Assets/Opsive/UltimateCharacterController/Add-Ons/Multiplayer/PhotonPUN/Scripts/Game/SpawnManagerBase.cs#L211).
  
- Light Partcile will be destroyed(collected) when player collide with them
  
  Basically, a collider is attached to each light particle, and destroy on collision. The implementation can be find [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/UpdateParticleCounter.cs#L30).
  
- Players can place the suckable light particle to destory the boss

  The place operation is basically generate the prefab at a specific location (right ahead of the player). The implementation can be found in this [file](https://github.com/YiLoganXIe/ECS189L-Final/blob/main/PartyTime/Assets/Script/Player/PlayerController.cs). `Pun` is used to broadcast RPC message.
  
- Find Others & Destroy Rock & Post-Processing

  In the level one, players are supposed to find each other first, and find the rock to destroy it. After they destroying it, blockage to level 2 will be removed and post-processing effect will also be removed. All detailed implementation can be found [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/main/PartyTime/Assets/Script/Trigger/RockDestroyTriggerScript.cs).
  
- Cinematic Camera Transition when Entering the Game Level 2

  A cinematic camera transition will be employed when one of player successfully entered the second region of the map. The camera will lerp to the position of boss to let player see the boss. The detailed implemenatation can be found [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/main/PartyTime/Assets/Script/Boss/LookAtBoss.cs).
  
- Boss Generation

  Boss will only be generated when at least one of player entered the second area of the map. The conditional generation is implemented [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/Boss/LookAtBoss.cs#L79). The RPC messaga sending with `Pun` is used.
  
- Boss Destroy

  Players can place the light particle to let it be suck up by the Boss and kill it. The logic of destroying boss and counting the light particle pulled by Boss can be found [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/main/PartyTime/Assets/Script/Boss/BossAbsorbingController.cs).
  
- Floating Stone when Entering 3rd Level

  When players step onto floating rock, it will be move a bit down to simulate the gravity feel. The detailed implementation can be found [here](https://github.com/YiLoganXIe/ECS189L-Final/blob/main/PartyTime/Assets/Script/Trigger/FloatStoneTrigger.cs).

- MiniGame

  Player need to play a minigame before the enter the final level. It is a Math base mini game that player need to click on button `7!` whenever the counter counts down to a number that contains 7 or a multiplier of 7. [CountDownController.cs](https://github.com/YiLoganXIe/ECS189L-Final/blob/52bc7b8655c190c325dd9c836e4398694c4f58b3/PartyTime/Assets/Script/PushTheButton/CountDownController.cs)
    
- Ending

  When player get to the top of the light tower, camera pans the landscape player visited. Afterwards, the ending credit and splash scene will play, before looping player back to the beginning launcher so they can replay.
    
Our project is a relative complex project, so there are several other logic parts out there. We just think it might be to lengthy to include them all. 

# Sub-Roles

## Audio

**List your assets including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.** 
Currently, we use the built-in sound systems from the libraries [DreamOS](https://assetstore.unity.com/packages/tools/gui/dreamos-complete-os-ui-188483) and [Ultimate Character Controller](https://opsive.com/assets/ultimate-character-controller/). Player will hear sounds when interacting with the built-in interface and from their character running.

## Gameplay Testing

This is a walking simulator. The game mechanism is the major part.Gameplay testing is not our major focus here. However, the game is still thoroughly tested by all our team members by playing it through to make sure it flows smoothly and no critical bugs here.

## Narrative Design

The Game will feel like people navigating the landscape as well as navigating the reflection about pandemic year from the developers.

**Game Flow**
On the Game Design Part, the game starts from darkness chaos world, and player need to find a way to calm down.

They will defeat a boss which represent all the factors that messed up our pandemic year.

Then Player need need to solve a puzzle, and navigate through some pictures that tell happy memories about pandemic year.

**Text**
The text is created by Yi Xie, edited by Rachel Heleva and Matt Simons(Life Saver). 

The text of the game is mainly focus on telling pandemic story of Yi. We hide shards into small scenes that setup base on the content of the shard story. It is a little bit like the story telling of Dark Soul. Player can look into it if they are interested. They serve for a meta concept of the game.

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**
[PressKit](https://t3crowbarmaster.itch.io/hotdog-mans-pandemic-pandemonium)
[Game Trailer]()

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**



## Game Feel

*Splash Screen:* Splash Screen can set up the tone of the game. It not only tells people that our game is well made, but also tells some information about the game for the player. A series of Splash Screen that display during the Launch and Ending of the game. [SplashFadeOut.cs](https://github.com/YiLoganXIe/ECS189L-Final/blob/main/PartyTime/Assets/Script/UI/SplashFadeOut.cs)

*Post Processing:*
    Post Processing can set the tone of the game. Our game is base on a cold night, and there are different visual effects base on the progress of the player.
    
*Boss Cinematic:*
    We want the players notice that there is a boss fight in our game. So we pan the camera face to the boss when the boss spawn in the scene.

*Floating Stone:* Some of the stone are set to floating in the scene. We want to let the player notice there is something worth to pay attetion. The floating stone will falling down after the interact with the player so it will feel cool.


    

    
    

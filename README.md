## Documentation

---
### Base Architecture
In this game, I used Scriptable Object architecure I made for myself. You can find it from [here](https://github.com/mtaha3249/SO-Architecture).

---
### How scene progression work?
The game progression actually handles by the SO-Event.

<img width="264" alt="Screenshot 2021-12-25 at 7 42 52 PM" src="https://user-images.githubusercontent.com/38559882/147387449-014b6b57-55b1-4a62-97d5-212610a886b0.png">
To change the game state of player, I Raise the event.

```c#
_gameState.Raise(GameState.MainMenu);
_gameState.Raise(GameState.Gameplay);
_gameState.Raise(GameState.LevelComplete);
_gameState.Raise(GameState.LevelFail);
```

---
### Events Callbacks
There is a gameObject __Game Listeners__ it handles all callbacks for all events in the game.

<img width="270" alt="Screenshot 2021-12-25 at 7 52 29 PM" src="https://user-images.githubusercontent.com/38559882/147387666-8d3bf1b9-e280-4786-b8af-8c02b3f02b26.png">

There is another script which handles the callbacks of the events __GameEventListener.cs__. You can see the script attached to the player prefab and asteroids prefabs.

<img width="270" alt="Screenshot 2021-12-25 at 8 04 34 PM" src="https://user-images.githubusercontent.com/38559882/147387891-c6fee6ac-fc8b-422a-9e4e-43d5e5d96a8b.png">

To Handle callbacks most of the classes implement __IGenericCallback.cs__ and bind that event to the listeners.

---
### Managers
This project has 3 Main Managers.
1. Level Manager.
2. Pool Manager.
3. Asteroids Spawner.

---
#### Level Manager
This manager handles all the game progression state like Reset position and rotation of player.

---
#### Pool Manager
This Manager and module was made my me. You can fetch and read the documentation from [here](https://github.com/mtaha3249/Generic-Pool).

---
#### Asteroids Spawner
This Manager spawn asteroids and increase spawn amount or after given wave time, using Pool Manager and spawn them outside the camera view.

---
### External Plugins
There are 2 External plugins I used in the game.
1. __Control Freak 2__ for Input.
2. __Procedural UI__ for UI.

Export few things from other packages like, particles from Epic Toon and free models for Player and Asteroids.

---
### Physics Callbacks
I had 2 scripts from made for collision and trigger callbacks.
1. TriggerEvent.cs
2. CollisionEvent.cs

These scripts are abstract classes and need to implement abstract function. These scripts filter by tag and layerMask.

---
### How to check easily?
I suggest you can read scripts attached to gameObject, I mentioned below.
1. Player.
2. Asteroids Prefabs.
3. Asteroids Spawner.
4. Lives in the canvas.
5. Score in the canvas.

I hope this makes it easy to read and debug.

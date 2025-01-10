# IdlePrototype

## Overview

This project showcases a Unity game designed to highlight my skills in **C# programming**, **game architecture**, and the implementation of advanced patterns such as **State Machine**, **Abstract Factory**, and **Service Locator**. The game includes systems for player movement, progression, resource management, and a robust game loop, all designed with scalability and maintainability in mind.

---

## Project Structure

- **IdlePrototype/Assets/_Project/**: Root directory containing the Unity project files.
- **IdlePrototype/Assets/_Project/Scripts**: Game logic and architecture scripts.
- **IdlePrototype/Assets/Resources**: Key game assets
- **IdlePrototype/Assets/ThirdPartyContent**: Plugins, asset packs, etc.

---

## Game Controls

- **W, A, S, D**: Movement keys for player navigation.

---

## Game Cycle

1. **GameRunner**: Ensures the presence of a `GameEntryPointBootstrap`. If absent, it adds a `Bootstrapper` prefab to the scene.

2. **GameEntryPointBootstrap**:
   - Creates the `Game` instance.
   - Initializes `GameStateMachine`.
   - Enters `BootstrapState`.

3. **BootstrapState**:
   - Registers all services.
   - Starts the game from the `InitialScene`.
   - Asynchronously loads the next scene.

4. **LoadProgressState**:
   - Loads existing progress or initializes new `PlayerProgress`.
   - Prepares the `Main` scene for loading.

5. **LoadLevelState**:
   - Initializes UI (`UIFactory`).
   - Initializes game world (`GameFactory`).
   - Notifies progress readers.

6. **GameLoopState**:
   - Core gameplay state.

---

## Systems and Features

### Loot System

1. **LootSpawner**:
   - Subscribes to `BuildingProduce.Happened` event in `Start()`.

2. **BuildingProduce**:
   - Generates resources and triggers the `Happened` event.

3. **Spawn Loot**:
   - `LootSpawner` reacts to `Happened` and invokes `SpawnLoot()`.

4. **Loot Creation**:
   - `GameFactory` instantiates `LootComponent`.

5. **Loot Details**:
   - `Loot` class: Defines the size and properties of loot.
   - `LootPiece` component: Manages loot behavior on the stage.

---

### Player Progression

- **PersistentProgressService**:
  - Centralized reference for player's progress.

- **PlayerProgress**:
  - Stores user data relevant to gameplay progression.

---

## Patterns Used

- **State Machine**:
  - `GameStateMachine`: Manages game states (`BootstrapState`, `LoadProgressState`, `LoadLevelState`, `GameLoopState`).

- **Abstract Factory**:
  - `GameFactory`: Handles object creation in the game scene.
  - `UIFactory`: Handles UI creation in the game scene.

- **Service Locator**:
  - `AllServices`: Maps services to their implementations for easy access.

---

## Key Highlights

- **Asynchronous scene loading** for smooth transitions.
- **Modular architecture** designed for scalability.
- Integration of modern **programming patterns** for clean and maintainable code.

---

## Commit Descriptions
| Name      | Description                                                    |
|-----------|----------------------------------------------------------------|
| init	    | Start a project/task						             		 |
| build     | Project build or changes in external dependencies              |
| sec       | Security and vulnerability fixes                               |
| ci        | CI configuration and script updates                            |
| docs      | Documentation updates                                          |
| feat      | Adding new functionality                                       |
| fix       | Bug fixes                                                      |
| perf      | Changes aimed at improving performance                         |
| refactor  | Code refactoring without fixing bugs or adding new features    |
| revert    | Reverting previous commits                                     |
| style     | Code style fixes (tabs, indents, dots, commas, etc.)           |
| test      | Adding tests                                                   |							 |
| chore     | Changes to the build process or auxiliary tools and libraries  |

# CLAUDE.md

This file provides guidance to Claude Code when working with code in this repository.

## Project Overview

This is an s&box game project (Source 2 engine, C#). The game is a multiplayer zombie survival game ("Goated Zombies") using the s&box scene system with GameObjects and Components.

## s&box Documentation Reference

**CRITICAL: Always read `sbox-docs.md` in the project root BEFORE writing or modifying any s&box code.**

This file contains 2,800+ lines of official Facepunch documentation fetched directly from sbox.game/dev/doc. It covers:

- Scene system, GameObjects, Prefabs
- All 129 built-in component types
- Component lifecycle (OnAwake, OnStart, OnUpdate, OnFixedUpdate, OnEnabled, OnDisabled, OnDestroy)
- Component querying (`Components.Get<T>()`, `GetComponent<T>()`, `GetAll<T>()`, `FindMode` flags)
- Scene tracing / raycasting (`Scene.Trace.Ray(startPos, endPos).Run()`)
- Transform API (`WorldPosition`, `WorldRotation`, `LocalPosition`, `LocalRotation`)
- Math types (Vector3, Rotation, Angles, Transform)
- Input system (`Input.Pressed()`, `Input.Down()`, `Input.Released()`, `Input.AnalogMove`)
- Networking (Sync, RPC, Lobbies, Ownership, NetworkSpawn)
- UI system (Razor panels, PanelComponent, styling)
- PlayerController component and its event system
- Console variables and commands
- Code generation and attributes
- Editor tools, widgets, and apps

### How to use the docs

1. **Before implementing any feature**: Search `sbox-docs.md` for the relevant API. Use Grep to find method signatures, patterns, and examples.
2. **When unsure about an API**: Check the docs first, don't guess. Wrong API usage has caused bugs in this project before.
3. **Key sections to check often**:
   - "Scene Tracing" — for raycast syntax (`Scene.Trace.Ray(startPos, endPos)` takes two Vector3s, NOT a Ray object)
   - "Components" / "Querying Components" — for correct `Get<T>()` / `GetAll<T>()` patterns
   - "Component Methods" — for lifecycle method signatures
   - "Player Controller" — for built-in PlayerController events and architecture
   - "Code Basics (Cheat Sheet)" — for quick reference on common operations

### Known API patterns (verified from docs)

```csharp
// Component access - both forms work
go.GetComponent<T>();
go.Components.Get<T>();
go.Components.Get<T>( FindMode.Disabled | FindMode.InAncestors );
go.Components.GetAll<T>();
go.Components.GetAll();  // no type param is valid

// Raycasting - takes two Vector3 positions, NOT a Ray
Scene.Trace.Ray( startPos, endPos ).Run();
Scene.Trace.Ray( startPos, endPos ).WithoutTags( "player" ).Run();
Scene.Trace.Sphere( radius, startPos, endPos ).Run();

// Transforms
GameObject.WorldPosition = new Vector3( x, y, z );
GameObject.LocalPosition = new Vector3( x, y, z );
GameObject.WorldRotation = rotation;

// Input
Input.Pressed( "use" )    // this frame only
Input.Down( "jump" )      // held
Input.Released( "attack1" )

// Creating GameObjects at runtime
var go = new GameObject( true, "MyObject" );  // (enabled, name)
go.SetParent( parentObject );
var renderer = go.Components.Create<ModelRenderer>();  // NOT AddComponent!
go.Destroy();  // cleanup

// Prefab spawning
var go = MyPrefab.Clone( position );
go.NetworkSpawn();  // if networked

// FindMode flags (for component queries)
FindMode.EverythingInSelfAndDescendants
FindMode.EverythingInSelfAndAncestors
FindMode.Disabled | FindMode.InAncestors
```

### Critical API gotchas
- **`Components.Create<T>()`** to add components at runtime — NOT `AddComponent<T>()` (which is a shorthand on GameObject but `Components.Create` is the verified pattern)
- **`new GameObject( true, "Name" )`** — constructor takes (bool enabled, string name)
- **`Scene.Trace.Ray( startPos, endPos )`** — two Vector3 positions, NOT a Ray object + distance
- **Prop component** controls physics props — disabling it may affect the ModelRenderer. When holding weapons, prefer creating a separate viewmodel GameObject rather than fighting physics

## Project Structure

- `/Code/` — Game C# code (Components)
- `/Assets/` — Scenes and assets
- `/Assets/goated zombies.scene` — Main gameplay scene (600+ GameObjects)
- `/Libraries/` — External libraries (fish.scc character controller, etc.)
- `/ProjectSettings/` — Input config, collision settings
- `sbox-docs.md` — Official s&box documentation reference

## Key Scene Objects

- **Player Controller** — Uses built-in `Sandbox.PlayerController` with first-person camera, body rendering, walk/swim/ladder movement modes
- **Camera** — Child of Player Controller, CameraComponent with ZNear=10, FOV=60

## MCP Tools

This project has an MCP server (Ozmium) running in the s&box editor. Use `mcp__sbox__*` tools to:
- Query scene hierarchy and inspect objects
- Add/remove components and set properties
- Find GameObjects by name, tag, or component
- Save scenes
- Read editor logs

**Always use MCP tools to modify the scene rather than editing .scene files by hand.**

## Code Conventions

- Uses `global using Sandbox;` — no need to prefix Sandbox namespace
- Components inherit from `Component`
- Use `[Property]` attribute for inspector-exposed fields
- Use `Input.Pressed("action")` for input (actions defined in `/ProjectSettings/Input.config`)
- Available input actions: Forward, Backward, Left, Right, Jump, Run, Walk, Duck, Attack1, Attack2, Reload, Use, View, Flashlight, Slot1-Slot9

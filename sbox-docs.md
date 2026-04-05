# S&box Official Documentation

> Fetched from sbox.game/dev/doc via Jina Reader, April 2026

## Table of Contents
- [First Steps](#page-first-steps)
- [Reporting Errors](#page-reporting-errors)
- [FAQ](#page-faq)
- [Monetization](#page-monetization)
- [GameObject](#page-gameobject)
- [GameObjectSystem](#page-gameobjectsystem)
- [Scenes](#page-scenes)
- [Scene Tracing](#page-scene-tracing)
- [Scene Metadata](#page-scene-metadata)
- [Components](#page-components)
- [Component Methods](#page-component-methods)
- [Component Interfaces](#page-component-interfaces)
- [Execution Order](#page-execution-order)
- [Async](#page-async)
- [Events](#page-events)
- [Prefabs](#page-prefabs)
- [UI System](#page-ui-system)
- [Styling Panels](#page-styling-panels)
- [Style Properties](#page-style-properties)
- [HudPainter](#page-hudpainter)
- [VirtualGrid](#page-virtualgrid)
- [Localization](#page-localization)
- [Networking & Multiplayer](#page-networking--multiplayer)
- [Networked Objects](#page-networked-objects)
- [Sync Properties](#page-sync-properties)
- [RPC Messages](#page-rpc-messages)
- [Ownership](#page-ownership)
- [Network Events](#page-network-events)
- [Network Visibility](#page-network-visibility)
- [Network Helper](#page-network-helper)
- [Custom Snapshot Data](#page-custom-snapshot-data)
- [Testing Multiplayer](#page-testing-multiplayer)
- [Dedicated Servers](#page-dedicated-servers)
- [Shader Graph Properties](#page-shader-graph-properties)
- [VR](#page-vr)
- [Game Exporting](#page-game-exporting)
- [Assets: First Time Setup](#page-assets-first-time-setup)
- [Assets: Ready-to-Use Assets](#page-ready-to-use-assets)
- [HammerMesh Component](#page-hammermesh-component)
- [Development](#page-development)
- [Game Project](#page-game-project)
- [Addon Project](#page-addon-project)
- [Code Basics (Cheat Sheet)](#page-code-basics)
- [Console Variables](#page-console-variables)
- [Math Types](#page-math-types)
- [API Whitelist](#page-api-whitelist)
- [Player Controller](#page-player-controller)
- [ISceneStartup](#page-iscenestartup)
- [Services (Stats)](#page-services)
- [Cloud Assets](#page-cloud-assets)
- [GameResource Extensions](#page-gameresource-extensions)
- [ActionGraph](#page-actiongraph)
- [Intro to ActionGraphs](#page-intro-to-actiongraphs)
- [ActionGraph Examples](#page-actiongraph-examples)
- [Shaders](#page-shaders)
- [Shading Model](#page-shading-model)
- [Dynamic Reflections](#page-dynamic-reflections)
- [G-Buffer](#page-g-buffer)
- [Movie Maker: Getting Started](#page-movie-maker-getting-started)
- [Movie Maker: Editor Map](#page-movie-maker-editor-map)
- [Movie Maker: Skeletal Animation](#page-movie-maker-skeletal-animation)
- [Movie Maker: Playback API](#page-movie-maker-api)
- [Editor Shortcuts](#page-editor-shortcuts)
- [Editor Tools](#page-editor-tools)
- [Component Editor Tools](#page-component-editor-tools)
- [Editor Widgets](#page-editor-widgets)
- [Custom Editors](#page-custom-editors)
- [Editor Apps](#page-editor-apps)

---

## Page: First Steps {#page-first-steps}
> Source: https://sbox.game/dev/doc/about/getting-started/first-steps/

An engine can seem huge and complicated when you don't know it. The s&box engine is relatively simple.

Here's how we think you should get started exploring the engine.

### Getting the s&box editor

The s&box editor and game are available to everyone through the developer preview, you can obtain it here.

To install the s&box editor click here or:

1. Open Steam
2. Click on the Library tab
3. Search for s&box
4. Install both the game and the editor apps

You can either launch through Steam, use a shortcut to sbox-dev.exe or open your .sbproj file directly.

### Play Testbed

Start s&box (not the editor) and find the game called `testbed`, this game is used by us to exhibit and test certain engine features, to make sure they work and keep working.

When you enter the game you'll find a menu of scenes. Each scene tests a different engine feature.

Click a scene to enter it, have a play around, press escape to return to the main menu. You can hold escape to completely leave any game.

So here's what you just saw. The menu uses our UI system, which is like HTML with c# inside. It's basically blazor, if you've ever heard of that.

When you clicked on a title, you entered a Scene. Our engine is Scene based, rather than map based like the regular Source Engine. Scenes are json files on disk, and are very fast to load and switch between - just like you experienced.

You probably saw a bunch of cool stuff. Here's some else cool, you can download the source for that game here, which includes all the scenes. Once you download it just open the .sbproj file to open it in the s&box editor, then explore the different scenes in the Asset Browser.

You can edit the scenes and play with them locally to get a feel of how things work.

### Create a New Project

The best way to learn is to do. Open the s&box editor and on the welcome screen, choose New Project. Create a Minimal Game project.

#### Creating Game Objects

Once open you have an empty scene. You can experiment by creating GameObjects by right clicking the tree on the left, and selecting an object type to create.

#### Creating Components

After that, try to **make a GameObject** that you can control by creating a custom component by selecting **Add Component** on the GameObject inspector and typing in a name. The file should open in Visual Studio.

#### Player Input

Use the Input section of this site to figure out how to read keys, and change the `WorldPosition` depending on which keys are being pressed.

After that, maybe control the position of the camera too, either by parenting it to your object, or by setting the position directly using `Scene.Camera.WorldPosition`.

Congratulations - you just learned the basics of GameObjects and Components. You're a game developer now.

### Ask questions

If you don't understand something, please ask on the forums or on Discord in the beginner's channel.

The more you ask questions, the more we'll realise that something is confusing, and the more likely we'll be to create documentation or make it simpler.

We can only know if we're doing something shit if you tell us. Please tell us what we're doing wrong.

---

## Page: Reporting Errors {#page-reporting-errors}
> Source: https://sbox.game/dev/doc/about/getting-started/reporting-errors/

Errors will happen. Here's how to make a useful error report.

### Issue Tracker

We use github to track issues. [Our issue repository is here](https://github.com/Facepunch/sbox-issues/issues). When issues are submitted, they're triaged and assigned [priorities on our project here](https://github.com/orgs/Facepunch/projects/24).

### Error Logs

It's often really helpful to us to have your log to work out what's going on. If you get any errors, these will be printed in the log file along with their stack trace, and any other messages.

They're written to the `logs` folder inside your s&box installation directory, the quickest way to get to this is by right-clicking s&box in Steam.

The last 10 are kept, the latest one is always called `Log.log`. Find the right one for the session and attach this to your issue on GitHub.

An error message on its own is usually not very useful. If you can provide a stack trace too, by either uploading the log or copy/pasting from the console (by clicking the error in the in-game console), it'll make things much easier to diagnose.

Please **don't** upload screenshots of the console, they aren't useful.

### Hard Crashes

When you get a hard crash (the game or editor exits on its own) then the crash log is generally automatically generated and uploaded to our backend.

While these crash logs are useful in revealing the problem, sometimes it's not obvious. If you have found a way to reproduce the crash, that's really useful, please make an issue. If the crash needs a specific asset to cause the crash, please provide that too.

Finally, please provide your 64-bit steamid (steamID64); this lets us look up your crash dumps very easily. You can use websites [like](https://steamid.io/) [these](https://www.steamidfinder.com/) to find it.

### Common Errors

You NEED the following, if you don't have the following this is likely why:

- Windows 10 and above - Windows 7 will not work
- At least 4GB of RAM, 8GB for the editor.
- A graphics card that isn't older than 12 years

#### Steam Not Found

This tends to happen when you've got a Steam emulator on your system from pirating games, naughty.

#### Failed to initialize Vulkan

Graphics cards have supported Vulkan since 2012. If you don't have one, get one.

If you are on a laptop, make sure your drivers are up to date for your integrated and dedicated graphics.

#### Proton Errors

If something is working on Windows but doesn't work on Proton, tell the Proton team:
https://github.com/ValveSoftware/Proton/issues/4940

---

## Page: FAQ {#page-faq}
> Source: https://sbox.game/dev/doc/about/faq/

**When will it be released?**
We're not in a rush (obviously). I think we'll know when it's ready to be released, and it's probably best not to speculate.

**How much will it cost?**
We don't know. We haven't decided yet.

**Are you using workshop?**
No. We're trying to make a system where you don't have to install addons. sbox.game functions similarly to how users don't need to install videos from YouTube—they simply access what they need.

**How can I get access to the developer preview?**
Users should visit https://sbox.game/give-me-that, log in, and click the button to add it to their Steam account.

**Why not Unity? Why not UE?**
We're sentimental about the Source engine. Source2 offers a modern renderer with contemporary development tools like ModelDoc and Animgraph. We acknowledge choosing this path despite acknowledging it would have been cheaper and easier using alternative engines.

**Will creators be able to monetize?**
Yes. It's integral. Creators can develop cosmetic items for in-game sales with revenue sharing, or create games and receive compensation through the playfund system.

**So is it an engine, or a platform?**
It's everything.

---

## Page: Monetization {#page-monetization}
> Source: https://sbox.game/dev/doc/about/getting-started/monetization/

An important part of s&box being a modern game platform is allowing the developers that use it to make money from it.

Our plans around monetization are evolving over time, nothing is set in stone. We want to give developers all the opportunities to make money possible without harming player experience.

### The Play Fund

Every day we create a pool of money. That pool is distributed among the games and maps with the most players.

#### What is it, like most players? Most hours?

It's based roughly on clamped individual player hours. The exact algorithm we use is kept secret. We will need tweak the algorithm over time to encourage and discourage specific behaviours.

#### How do I enable it?

Open your map or game package on sbox.game and go to `Configure > Edit Features`. You'll then see an option to include it.

#### How do I know how much money I made?

The daily estimates are visible on your Organization's Page, under `Monetization`.

#### Am I allowed to enable it for my Package? What are the rules?

If you're using copyrighted material in your package - then **no**. If you're repackaging someone else's work with no real creativity of your own, then **no**.

If you're using stuff from sbox.game where author permission is implicit, stuff you created, stuff with an open license, then yes.

#### When are payments made?

Payments are made towards the middle of every month, for the previous month. For example, in the middle of March we'll pay out what you made in February.

You will need to have at least $100 pending to receive a payment. You also need to have set up your payment information in your profile settings.

#### Can I subdivide payments to my team?

Yes! You can split your payments between your different team members. They don't need to be members of the organization.

#### Is it just maps and games?

Yes, for now. It would be awesome for a map creator to be able to say "okay I used these models so I'm gonna give this org 10%", but if we do that it's something that will come later. This is the first step.

#### Where's the pool from? How much is it?

I'd like to get more transparent about the pool in the future and show the figure somewhere. For now, while we're finding out feet, it's not public.

Garry's Mod's revenue is funding the development of s&box. So the fund is from Garry's Mod for now. Our hope is that one day s&box will be able to stand on its own two feet, and the fund will grow with its success.

---

## Page: GameObject {#page-gameobject}
> Source: https://sbox.game/dev/doc/scene/gameobject/

A `GameObject` represents an object in the scene world. It contains a few different elements.

### Transform

Represents where the GameObject is in the scene. Its positioning, its rotation and its scale.

If it has a parent then its transform is held relative to them, so when their parent moves, so does the child.

Here's how you can interact with them in code:

```csharp
// Set world position
GameObject.WorldPosition = new Vector3( 100, 100, 100 );

// Set position relative to parent
GameObject.LocalPosition = new Vector3( 100, 100, 100 );

// Set world transform
GameObject.WorldTransform = new Transform( Vector3.Zero, new Angles( 90, 90, 180 ), 2.0f )
```

### Tags

The GameObject's tags are used for multiple things. They're used to group physics objects to decide what should collide with each other. They can be used by cameras to decide which objects should and shouldn't render. And they can be used by programmers to do whatever they want.

```csharp
if ( GameObject.Tags.Has( "enemy" ) )
{
	GameObject.Destroy();
}

GameObject.Tags.Add( "enemy" );
GameObject.Tags.Set( "enemy", isEnemy );
GameObject.Tags.Remove( "enemy" );
```

Tags are inherited. If a parent has the tag, then so does the child. The only way to remove the tag from the child is to remove it from the parent.

### Children

GameObject children are available via `GameObject.Children`. This is just a list of GameObjects.

### Components

GameObjects implement functionality using Components.

---

## Page: GameObjectSystem {#page-gameobjectsystem}
> Source: https://sbox.game/dev/doc/scene/gameobjectsystem/

A scene can contain systems that need to do work in specific places during the frame.

For example, one of these systems is the `SceneAnimationSystem,` which finds all `SkinnedModelRenderer` components and works out all of their animations in parallel. This is faster than doing it in Update() and avoids weird out of order problems. It gives us a single point in the frame where we know for sure that all of the bone positions are up to date.

### Implementation

To create your own system, you just define a new class that derives from `GameObjectSystem`.

```csharp
public class MyGameSystem : GameObjectSystem
{
	public MyGameSystem( Scene scene ) : base( scene )
	{
		Listen( Stage.PhysicsStep, 10, DoSomething, "DoingSomething" );
	}

	void DoSomething()
	{
		Log.Info( "Did something!" );
  
        var allThings = Scene.GetAllComponents<MyThing>();
	}
}
```

When a scene is created, a copy of every defined `GameObjectSystem` is created and added to it, you don't need to do anything else.

### Access

You can access a GameObjectSystem in a number of ways. One way is using `Scene.Get<MyGameSystem>()`

You can also inherit from `GameObjectSystem<T>` which adds a `static T Current` property to your system.

```csharp
public class MyGameSystem : GameObjectSystem<MyGameSystem>
{
  	public MyGameSystem( Scene scene ) : base( scene )
	{
	}
 
    public void MyMethod()
    {
        Log.Info( "Hello, World!" );
    }
}
```

Then you can use them like:

```csharp
MyGameSystem.Current.MyMethod();
```

### Stages & Order

Stages are based around certain events. For example, the PhysicsStep stage is called when ticking the physics, during FixedUpdate.

The order defines where to call your method during that event. -1 would call it before, +1 would call it after. You will be defining the systems, so getting them in an order you can live with is your business.

### Configuration

GameObjectSystems have two methods of configuration. You can either globally configure them, or edit them per scene.

In Project Settings, hit Systems — and you can configure them there.

Any property marked with `[Property]` will be configurable in project settings and saved.

---

## Page: Scenes {#page-scenes}
> Source: https://sbox.game/dev/doc/scene/scenes/

A scene is a collection of GameObjects.

### Getting the Current Scene

To get the current scene you can use the `Scene` accessor on any GameObject, Component, or Panel. You can also access it via the static `Game.ActiveScene`.

### Loading a New Scene

To load a new Scene, you can do any of the following:

```csharp
// Replaces the current scene with the specified scene
Scene.Load( myNewScene );
Scene.LoadFromFile( "scenes/minimal.scene" );

// Additively loads the specified scene on top of the current scene
var load = new SceneLoadOptions();
load.SetScene( myNewScene );
load.IsAdditive = true;
Scene.Load( load );
```

### Directory

All scenes have a GameObject Directory. This holds all the GameObjects in the scene indexed by guid, and enforces that every object's guid is unique.

This also allows fast object lookups if you know the guid of the object.

```csharp
var obj = Scene.Directory.FindByGuid( guid );
```

### GetAll / Get

The scene also holds a fast lookup index of every component. This allows you to quickly get every component of a certain type without having to keep your own lists, or iterate the entire scene.

```csharp
// Tint all models a random colour
foreach ( var model in Scene.GetAll<ModelRenderer>() )
{
	model.Tint = Color.Random;
}

// Grab your singleton
var game = Scene.Get<GameManager>();
```

---

## Page: Scene Tracing {#page-scene-tracing}
> Source: https://sbox.game/dev/doc/scene/scenes/tracing/

Scene tracing uses the `Scene.Trace` builder pattern API. At a minimum, traces have a shape, start, and end.

Features:
- Ray, sphere, and box trace shapes supported
- Tag-based filtering with `WithoutTags()`
- Collision rule integration via `WithCollisionRules(tag)`
- Hitbox detection option with `UseHitboxes()`

**Basic ray trace:**

```csharp
SceneTraceResult tr = Scene.Trace.Ray( startPos, endPos ).Run();
```

**With collision rules:**

```csharp
var tr = Scene.Trace
	.Ray( startPos, endPos )
 	.WithCollisionRules( "bullet" )
 	.Run();
```

**Sphere trace:**

```csharp
var tr = Scene.Trace
	.Sphere( 32.0f, startPos, endPos )
	.WithoutTags( "player" )
	.Run();
```

**Box trace:**

```csharp
var tr = Scene.Trace
	.Ray( start, end )
	.Size( new BBox( -5, 5 ) )
	.UseHitboxes( true )
	.Run();
```

---

## Page: Scene Metadata {#page-scene-metadata}
> Source: https://sbox.game/dev/doc/scene/scenes/scene-metadata/

The `ISceneMetadata` interface enables components to store accessible metadata without loading scenes or cloning prefabs through `SceneFile` or `PrefabFile` objects.

A component can implement `ISceneMetadata` and provide a `GetMetadata()` method that returns a `Dictionary<string, string>` of key-value pairs for serialization.

Scene metadata can be read using `ResourceLibrary.GetAll<SceneFile>()` and grouping by metadata values. Similarly, prefab metadata can be retrieved and used for logic like enemy spawning with weighted probability.

---

## Page: Components {#page-components}
> Source: https://sbox.game/dev/doc/scene/components/ and https://sbox.game/dev/doc/code/components/

A Component is added to a GameObject to provide functionality. This functionality can vary wildly.

You could add a component that renders a model at the GameObject's position. Or you could add a component that created a physics object at the GameObject's position.

You can create your own components too. This is how games are programmed. For example, you could write a component that moved an object forward when the Forward key is held down.

### Adding Components

To add a component to a GameObject in editor, select the GameObject and then click on Add Component in the inspector.

To add a component to a GameObject in code:

```csharp
var modelRenderer = go.AddComponent<ModelRenderer>();

modelRenderer.Model = Model.Load( "models/dev/box.vmdl" );
modelRenderer.Tint = Color.Red;
```

You can also GetOrAddComponent if you want it to exist if it doesn't already.

```csharp
var modelRenderer = go.GetOrAddComponent<ModelRenderer>();

modelRenderer.Model = Model.Load( "models/dev/box.vmdl" );
modelRenderer.Tint = Color.Green;
```

### Querying Components

You can query a GameObject for components in multiple different ways.

```csharp
var x = go.GetComponents<ModelRenderer>();

var x = go.GetComponent<ModelRenderer>();

var x = go.GetComponentInChildren<ModelRenderer>();

var x = go.GetComponentsInChildren<ModelRenderer>();

var x = go.Components.GetComponentsInParent<ModelRenderer>();

var x = go.Components.GetComponentInParent<ModelRenderer>();
```

### Specialized Queries

If you're wanting to go even more granular:

```csharp
var x = go.Components.Get<ModelRenderer>( FindMode.Disabled | FindMode.InAncestors );

var x = go.Components.GetAll<ModelRenderer>( FindMode.Enabled | FindMode.InAncestors | FindMode.InSelf );

var x = go.Components.GetAll();
```

### Component References

You can get component references as variables in two main ways.

```csharp
[Property] ModelRenderer BodyRenderer { get; set; }

[RequireComponent] ModelRenderer BodyRenderer { get; set; }
```

### Removing Components

To remove a component from a GameObject, you call DestroyComponent(). You cannot reuse this component - at this point it is destroyed forever and you should stop using it.

```csharp
var depthOfField = GetComponent<DepthOfField>();
depthOfField.Destroy();
```

### Destroying GameObject from Component

`DestroyGameObject()`, nice and easy. You can also use `GameObject.Destroy()` if you want.

---

## Page: Component Methods {#page-component-methods}
> Source: https://sbox.game/dev/doc/scene/components/component-methods/

When creating a component there are a number of methods you can override and implement.

Note that for the component be enabled, its GameObject and all of their ancestors need to be enabled too. The GameObject will be considered disabled if one of its ancestor GameObjects is not enabled.

### OnLoad (async)

This is called after deserialization and is meant for a place for the component to "load". When loading a scene, the loading screen will stay open and the game won't start until all components `OnLoad` tasks are complete.

If your component is doing something special, such as generating a procedural level, you can override this on your component to do this in the loadscreen.

```csharp
protected override async Task OnLoad()
{
	LoadingScreen.Title = "Loading Something..";
	await Task.DelayRealtimeSeconds( 1.0f );
}
```

Internally this is where the Map component downloads and loads the map.

### OnValidate

Called whenever a property is changed in the editor, and after deserialization.

A good place to enforce property limits etc.

### OnAwake

Called once when the component is created, but only if our parent GameObject is enabled. This is called after deserialization and loading.

### OnStart

Called when the component is enabled for the first time. Should always get called before the first `OnFixedUpdate`.

### OnEnabled

Called when the component is enabled.

### OnUpdate

Called every frame.

### OnPreRender

> This method is not called on dedicated servers.

Called every frame, right before rendering is about to take place.

This is called after animation bones have been calculated, so it usually a good place to do things that count on that.

### OnFixedUpdate

Called every fixed timestep.

In general, it's wise to use a fixed update for things like player movement (the built in Character Controller does this). This reduces the amount of traces a client is doing every frame, and if your client is too performant, the move deltas per frame can be so small that they create problems.

### OnDisabled

Called when the component is disabled.

### OnDestroy

Called when the component is destroyed.

---

## Page: Component Interfaces {#page-component-interfaces}
> Source: https://sbox.game/dev/doc/scene/components/component-interfaces/

### ExecuteInEditor

Allows components to run specific lifecycle methods during editor mode, including OnAwake, OnEnabled, OnDisabled, OnUpdate, and OnFixedUpdate.

### ICollisionListener

Enables components to respond to physics collisions through three methods: OnCollisionStart, OnCollisionUpdate, and OnCollisionStop.

### ITriggerListener

Permits components to react to trigger interactions via OnTriggerEnter and OnTriggerExit methods.

### IDamageable

A helper interface marking components that can receive damage through the OnDamage method. Damage can be fired via raycasting and received by implementing the interface.

### INetworkListener and INetworkSpawn

Interfaces for handling network events in multiplayer scenarios.

---

## Page: Execution Order {#page-execution-order}
> Source: https://sbox.game/dev/doc/scene/components/execution-order/

You should not rely on the order in which the same callback methods get invoked for different GameObjects, it is not predictable. For greater control over execution sequencing, developers should utilize a GameObjectSystem instead.

Component methods execute simultaneously across all components. The documentation includes a flowchart illustrating the execution order for scenes.

---

## Page: Async {#page-async}
> Source: https://sbox.game/dev/doc/scene/components/async/

By default, async tasks in s&box run on the main thread. They operate a lot like coroutines, making them the perfect replacement.

### Using Async

To make a method asynchronous:

```csharp
async Task PrintSomething( float waitSeconds, string message )
{
	await Task.DelaySeconds( waitSeconds );
	Log.Info( message );
}
```

Components have a special Task property with some extra helper functions (like `DelayRealtimeSeconds`).

Since a task is async, you can await it:

```csharp
async Task LerpSize( float seconds, Vector3 to, Easing.Function easer )
{
	TimeSince timeSince = 0;
	Vector3 from = WorldScale;
 
	while ( timeSince < seconds )
	{
		var size = Vector3.Lerp( from, to, easer( timeSince / seconds ) );
		WorldScale = size;
		await Task.Frame(); 
	}
}
 
await LerpSize( 3.0f, Vector3.One * 3.3f, Easing.BounceOut );
await LerpSize( 1.0f, Vector3.One * 4.0f, Easing.EaseInOut );
await LerpSize( 1.0f, Vector3.One * 3.0f, Easing.EaseInOut );
```

### Multiple Async

Tasks can coordinate and execute multiple operations simultaneously. This resembles multithreading but operates differently:

```csharp
async Task DoMultipleThings()
{
	Task taskOne = PrintSomething( 2.0f, "One" );
	Task taskTwo = PrintSomething( 3.0f, "Two" );

	await Task.WhenAll( taskOne, taskTwo );
}
```

### Returning Values

Async Tasks can return values:

```csharp
async Task<string> GetKanyeQuote()
{
	string kanyeQuote = await Http.RequestStringAsync( "https://api.kanye.rest/" );

	kanyeQuote = kanyeQuote.Replace( "music", "poosic" );

	return kanyeQuote;
}

async Task PrintKanyeQuote()
{
	string quote = await GetKanyeQuote();
	Log.Info( $"KANYE SAID: {quote}" );
}
```

### Cooperating with synchronous code

Calling async functions from regular functions:

```csharp
protected override void OnEnabled()
{
	_ = DoMultipleThings();
}
```

To use the value from synchronous code:

```csharp
protected override void OnEnabled()
{
    GetKanyeQuote().ContinueWith( task => Log.Info( $"Kanye: {task.Result}" ) );
}
```

Alternative approach:

```csharp
Task<string> getQuoteTask;

protected override void OnEnabled()
{
	getQuoteTask = GetKanyeQuote();
}

protected override void OnUpdate()
{
	if ( getQuoteTask is not null && getQuoteTask.IsCompletedSuccessfully )
	{
		Log.Info( $"Kanye: {getQuoteTask.Result}" );
		getQuoteTask = null;
	}
}
```

### Being Responsible

Something to be thinking of is what happens when your GameObject is destroyed or disabled while you're waiting.

When implementing things yourself you should be considerate of this.. the async method isn't guaranteed to stop just because the GameObject or Component is gone.

We do somewhat handle this internally, when awaiting a method in Component.Task we will automatically cancel the task if the GameObject turns invalid.

### Common Errors

A common async error is letting tasks stack up.

For example, if you have a system where a user presses space, it waits a second, then shoots a button.. you need to handle a user pressing that button multiple times during that second. You need to handle the user dying during that second.

Maybe you want to not launch a new async task if the user firing task is running. Maybe you want to cancel the firing task and start it again (use a `CancellationToken` maybe).

---

## Page: Events {#page-events}
> Source: https://sbox.game/dev/doc/code/events/

You can broadcast and listen to events in your scene using interfaces.

These events aren't sent over the network. They are sent to **active** Components and GameObjectSystems in your scene.

### Event Interface

An event class is just a regular interface. You don't need to do anything else.

```csharp
public interface IPlayerEvent
{
	void OnSpawned( Player player );
}

Scene.RunEvent<IPlayerEvent>( x => x.OnSpawned( playerThatSpawned ) );
```

You can, however, derive from `ISceneEvent<T>`. This gives you a bit nicer syntax. Internally this is just calling Scene.Run on the active scene.

```csharp
public interface IPlayerEvent : ISceneEvent<IPlayerEvent>
{
	void OnSpawned( Player player );
}
IPlayerEvent.Post( x => x.OnSpawned( playerThatSpawned ) );
```

You can also post events to specific GameObjects with `PostToGameObject`, instead of every object in the scene.

```csharp
IPlayerEvent.PostToGameObject( player.GameObject, x => x.OnSpawned( player ) );
```

If your event interface has many events and you don't want to have to implement them all, you can define defaults.

```csharp
public interface IPlayerEvent : ISceneEvent<IPlayerEvent>
{
	void OnSpawned( Player player ) { }

	void OnJump( Player player ) { }
	void OnLand( Player player, float distance, Vector3 velocity ) { }
	void OnTakeDamage( Player player, float damage ) { }
	void OnDied( Player player ) { }
	void OnWeaponAdded( Player player, BaseWeapon weapon ) { }
	void OnWeaponDropped( Player player, BaseWeapon weapon ) { }

	void OnCameraMove( Player player, ref Angles angles ) { }
	void OnCameraSetup( Player player, CameraComponent camera ) { }
	void OnCameraPostSetup( Player player, CameraComponent camera ) { }
}
```

### Broadcasting

Scene.RunEvent is the entry point to broadcast an event. This isn't limited to interfaces.. here's some interesting stuff you can do.

```csharp
Scene.RunEvent<SkinnedModelRenderer>( x => x.Tint = Color.Red );

float damage = 100.0f;
Scene.RunEvent<IPlayerDamageMesser>( x => x.ModifyDamage( player, damageinfo, ref damage ) );

List<Vector3> damagePoints = new ();
Scene.RunEvent<IDamageProvider>( x => x.GetDamagePoint( damagePoints ) );
```

### Listening

To listen, you just implement the interface you want to use. This could be an interface you have created, or could be one of the built in event classes.

```csharp
public class MyComponent : Component, ISceneLoadingEvents
{
	void ISceneLoadingEvents.AfterLoad( Scene scene )
	{
		
	}
}

public class CameraWeapon : BaseWeapon, IPlayerEvent
{
	void IPlayerEvent.OnCameraMove( Player player, ref Angles angles )
	{
        if ( Input.Down( "attack2" ) )
		{
			angles = default;
		}
	}
}
```

These events aren't available on Panels yet.

---

## Page: Prefabs {#page-prefabs}
> Source: https://sbox.game/dev/doc/scene/prefabs/

*Note: This page returned empty content via Jina Reader. The prefab system allows you to save GameObjects as reusable templates (.prefab files) that can be instantiated at runtime or placed in scenes.*

---

## Page: UI System {#page-ui-system}
> Source: https://sbox.game/dev/doc/systems/ui/

The s&box UI system centers on **Panels**, which are C# classes that support parent-child hierarchies. The system utilizes a stylesheet and flex system for layout and rendering and can be implemented either through code or Razor files with HTML/CSS syntax.

Panels can be instantiated directly in code by setting the Parent property, or declared in Razor files using tag syntax like `<MyPanel />`.

A `PanelComponent` serves as the UI root, attached to GameObjects with ScreenPanel or WorldPanel components. The `OnTreeFirstBuilt()` method initializes child panels.

By default, ScreenPanels will rescale all UI based on a 1080p target height automatically. This behavior can be disabled or adjusted to target desktop resolution through component settings.

---

## Page: Styling Panels {#page-styling-panels}
> Source: https://sbox.game/dev/doc/systems/ui/styling-panels/

### Using Stylesheets

It is common to have a file system like this:
- Health.razor
- Health.razor.scss

If you do this, the scss file is automatically included by your Health.razor panel.

If you want to specify a different location for the loaded Stylesheet, you can add this to your Panel class:

```csharp
[StyleSheet("main.scss")]
```

You can also import a stylesheet from within another stylesheet like so:

```scss
@import "buttons.scss";
```

### Styling Directly

There are a few ways to style your Panels without a stylesheet. It's really recommended that you use a stylesheet to keep things organized, but there are also valid reasons to use the following methods.

#### Styling the Element

You can directly style any element just like you can in HTML, but can inject C# when necessary:

```html
<label style="color: red">DANGER!</label>
<div class="progress-bar">
  <div class="fill" style="width: @(Progress * 100f)%"></div>
</div>
```

#### Style Block

Before or after your `<root>` element, you can add a `<style>` block that is read just like a Stylesheet:

```html
<style>
  MyPanel {
    width: 100%;
    height: 100%;
  }
  .hp { color: red; }
  .armor { color: blue; }
</style>
```

#### Styling in Code

You can modify a Panel's Style directly via `Tick()` or `OnUpdate()`:

```csharp
myPanel.Style.Width = Length.Percent(Progress * 100f);
```

---

## Page: Style Properties {#page-style-properties}
> Source: https://sbox.game/dev/doc/systems/ui/styling-panels/style-properties/

We try to keep as close to standard web styles as possible - but not every property is implemented.

### Common Types

| Type | Description | Examples |
|------|-------------|----------|
| Float | Standard floating-point number | `flex-grow: 2.5;` |
| String | Text with or without quotes | `font-family: Poppins;`, `content: "Back to Menu";` |
| int | Standard integer | `font-weight: 600;` |
| Color | Supports alpha channel | `color: #fff;`, `#ffffffaa;`, `rgba(red, 0.5);` |
| Length | Pixel or relative dimension | `left: 10px;`, `10%;`, `10em;`, `10vw;`, `mask-angle: 10deg;` |

### Custom Style Properties

- `aspect-ratio` (Float)
- `background-image-tint` (Color)
- `border-image-tint` (Color)
- `mask-scope` (default/filter)
- `sound-in` and `sound-out` (String - for audio on hover/click)
- `text-stroke` (Length, Color)

### Supported Standard Properties

Comprehensive table covering 100+ properties including:
- **Flexbox:** `flex-direction`, `flex-grow`, `flex-wrap`, `justify-content`, `align-items`
- **Animation:** `animation-duration`, `animation-timing-function`, `animation-name`
- **Background:** `background-color`, `background-image`, `linear-gradient`, `radial-gradient`, `conic-gradient`
- **Border:** Various `border-*` properties with radius support
- **Filters:** `backdrop-filter`, `filter` with blur, saturate, contrast, brightness, grayscale, sepia, hue-rotate, invert
- **Text:** `font-family`, `font-size`, `font-weight`, `text-align`, `text-decoration`, `text-transform`
- **Effects:** `box-shadow`, `text-shadow`, `opacity`, `transform`
- **Layout:** `display` (flex default/none), `position`, `overflow`, `z-index`

**Notable differences:**
- `display`: Everything is flex by default
- `font-family`: Specify a single font, based on the name of the font itself, not the filename
- `position`: Uses Yoga layout (see yogalayout.com)

### Custom Pseudo-Classes

Two s&box-specific pseudo-classes:
- `:intro` - Removed when element is created; transitions originate from this state
- `:outro` - Added when Panel.Delete() is called; panel waits for transitions before deletion

```scss
MyPanel {
	transition: all 2s ease-out;
	transform: scale( 1 );
	&:intro {
		transform: scale( 0 );
	}
	&:outro {
		transform: scale( 2 );
	}
}
```

---

## Page: HudPainter {#page-hudpainter}
> Source: https://sbox.game/dev/doc/systems/ui/hudpainter/

Each camera has a HudPainter that can be used to draw onto the HUD. You do this every frame, in any Update function.

This is more efficient than using actual UI panels because there's no layout, stylesheets or interactivity.. you're doing all that stuff yourself.

If your UI is relatively simple, you can do it this way to keep things easy.

```csharp
protected override void OnUpdate()
{
	if ( Scene.Camera is null )
		return;

	var hud = Scene.Camera.Hud;

	hud.DrawRect( new Rect( 300, 300, 10, 10 ), Color.White );

	hud.DrawLine( new Vector2( 100, 100 ), new Vector2( 200, 200 ), 10, Color.White );

	hud.DrawText( new TextRendering.Scope( "Hello!", Color.Red, 32 ), Screen.Width * 0.5f );
}
```

---

## Page: VirtualGrid {#page-virtualgrid}
> Source: https://sbox.game/dev/doc/systems/ui/virtualgrid/

VirtualGrid is a Panel that allows you to create a grid of items virtually by rendering only visible items rather than all items simultaneously. For example, with 1 million items, it renders only the visible few and dynamically updates when scrolling.

### Razor Implementation Example

```razor
<VirtualGrid Items=@Items ItemSize=@(120)>
    <Item Context="item">
        @if (item is Package entry)
        {
            <SpawnButton Icon="@entry.Thumb" Action="@(() => Spawn(entry))"></SpawnButton>
        }
    </Item>
</VirtualGrid>

@code
{
    public Package[] Items{ get; set; }
}
```

### Key Properties

- **Items**: Accepts any `IEnumerable<T>`
- **ItemSize**: A `Vector2` value that scales proportionally to fit the parent container while maintaining aspect ratio
- **Item**: Defines cell contents with the class "cell"

### Important Considerations

- VirtualGrid requires explicit sizing (width and height 100% recommended)
- All items must be uniform in size
- Use gap styles on the VirtualGrid element for spacing between items

---

## Page: Localization {#page-localization}
> Source: https://sbox.game/dev/doc/systems/ui/localization/

The localization system in s&box enables multi-language support through tokens. When displaying text such as `Hello World`, you should instead use a localization token like `#menu.helloworld`.

### Tokens

Strings prefixed with `#` are recognized as localization tokens that automatically resolve to the appropriate language value when used in UI labels.

### File Structure

Localization files follow the pattern `Localization/[language-code]/sandbox.json` containing key-value pairs:

```json
{
  "menu.helloworld": "Hello World",
  "spawnmenu.props": "Models",
  "spawnmenu.tools": "Tools",
  "spawnmenu.cloud": "sbox.game"
}
```

Usage in UI: `<label>#spawnmenu.props</label>`

### Supported Languages

The system supports 31 languages including Arabic (ar), English (en), Japanese (ja), Russian (ru), Spanish variants (es, es-419), Simplified/Traditional Chinese (zh-cn, zh-tw), and others including a Pirate variant (en-pt).

---

## Page: Networking & Multiplayer {#page-networking--multiplayer}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/

The networking system in s&box is purposefully simple and easy. The initial aim isn't to provide a bullet proof server-authoritative networking system, but a system that is really easy to use and understand.

### Create a new lobby

```csharp
Networking.CreateLobby( new LobbyConfig()
{
  MaxPlayers = 8,
  Privacy = LobbyPrivacy.Public,
  Name = "My Lobby Name"
} );
```

### List all available lobbies

```csharp
list = await Networking.QueryLobbies();
```

### Join an existing lobby

```csharp
Networking.Connect( lobbyId );
```

### Networked GameObjects

Enable via Network Mode setting in GameObject properties. Make a GameObject networked by changing its Network Mode to Network Object.

### Destroy Networked GameObject

```csharp
go.Destroy();
```

### Instantiating a Networked GameObject

```csharp
var go = PlayerPrefab.Clone( SpawnPoint.Transform.World );
go.NetworkSpawn();
```

### RPCs

```csharp
[Rpc.Broadcast]
public void OnJump()
{
	Log.Info( $"{this} Has Jumped!" );
}
```

---

## Page: Networked Objects {#page-networked-objects}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/networked-objects/

### Instantiating a Networked Object

Any game object can become a networked object by simply calling `NetworkSpawn()` on it. It will then be sent to all clients and can have Sync Properties and RPC Messages.

Every networked game object can have an owner, which determines who sends updates about its position, rotation and scale, as well as who can perform certain actions on it.

### Network Mode

All game objects can be one of three network modes. This mode determines whether other clients see it, or how they receive new information about it.

| Mode | Behaviour |
| --- | --- |
| `NetworkMode.Never` | This game object is never networked to others |
| `NetworkMode.Object` | The game object will be sent to other clients as its own networked object which can have synchronized properties and RPCs |
| `NetworkMode.Snapshot` _(default)_ | The host will send this game object as part of the initial scene snapshot when a client joins the game |

The network mode can also be changed for an object in the Scene from the Inspector view.

### Interpolation

By default the transform of all networked objects is interpolated smoothly for other clients.

### Disabling Interpolation

You can disable interpolation in one of two ways. Either by code, or using the inspector.

```csharp
Network.DisableInterpolation();
```

### Clearing Interpolation

Sometimes you want to clear any interpolation for an object. You can do that with `Network.ClearInterpolation()`. If you are the owner of the object, other clients will be told to clear interpolation when they next receive a transform update from us.

One use case for this would be to set the position of the object and have the position updated immediately for everybody without interpolation (teleporting.)

```csharp
Transform.Position = Vector3.Zero;
Network.ClearInterpolation();
```

### Refreshing a Networked Object

Once you call `NetworkSpawn()` on a game object, any further changes to its components or hierarchy will not be networked. Only information about the components or children that existed when you network spawned it will be sent to other clients.

If you add new components, change the enabled state of a component, add new children or change the hierarchy of a networked object significantly, you can send a refresh update to other clients by calling `Network.Refresh()` on the networked game object or from one of its components.

By default, only the host can send refresh updates for networked objects. If you'd like to enable the owner to also send refresh updates you can do so by changing the connection permissions for a client.

---

## Page: Sync Properties {#page-sync-properties}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/sync-properties/

### Sync Attribute

Adding the `[Sync]` attribute to a property on a Component will have its latest value sent to other players each time it changes.

```csharp
public class MyComponent : Component
{
  [Sync] public int Kills { get; set; }
}
```

These properties are controlled by the owner of the object, therefore only the owner of the object can change them.

### Supported Types

`[Sync]` properties support unmanaged types, and string. You can't synchronize every class with them, but any value type including structs will be fine. `int`, `bool`, `Vector3`, `float` are all examples of valid types. We also support serializing specific classes such as `GameObject`, `Component`, `GameResource`.

### Detecting Changes

You can detect changes to a `[Sync]` property by also applying the `[Change]` attribute to it. With this attribute you can specify the name of a callback method that will be invoked when the value of the property has changed.

Right now the `[Change]` attribute will not invoke the callback when a collection has changed. The callback will only be invoked when the property itself is assigned to something different.

```csharp
public class MyComponent : Component 
{
  [Sync, Change( "OnIsRunningChanged" )] public bool IsRunning { get; set; }
  
  private void OnIsRunningChanged( bool oldValue, bool newValue )
  {
    
  }
}
```

### Sync Flags

You can customize the behaviour of a synchronized property with `SyncFlags`.

| Flag | Description |
| --- | --- |
| `SyncFlags.Query` | Enables Query Mode for the property. See the Query Mode section below. |
| `SyncFlags.FromHost` | The host has ownership over the value, instead of the owner of the networked object. Only the host may change the value. |
| `SyncFlags.Interpolate` | The value of the property will be interpolated for other clients. The value is interpolated over a few ticks. |

### Collections

Sometimes you want to network collections such as an entire list or a dictionary. We provide special classes to do that.

```csharp
public enum AmmoCount
{
  Pistol,
  Rifle
}

public class MyComponent : Component 
{
  [Sync] public NetList<int> List { get; set; } = new();
  [Sync] public NetDictionary<AmmoCount,int> Dictionary { get; set; } = new();
}
```

You can initialize each in the declaration with `new()` or you can initialize the lists elsewhere, so long as you're doing so on the Owner of the network object. It doesn't matter if they are `null` for anyone else because they'll get created when they are networked if they need to be.

You can use `NetList<T>` and `NetDictionary<K,V>` like their regular counterparts. They contain indexers, `Add`, `Remove` and other methods you'd expect.

`NetList` and `NetDictionary` do not currently support the `[Property]` attribute.

### Query Mode

By default the properties are automatically marked dirty when set, via codegen magic.. meaning that when you set a property, if it's different we'll send the updated value to everyone.

```csharp
[Sync]
public Vector3 Velocity
{
	get;
	set;
}
```

No Query Mode needed. The only way to change Velocity is via the setter, which when called will mark it as changed using invisible codegen magic on the setter.

```csharp
Vector3 _velocity;
[Sync]
public Vector3 Velocity
{
	get => _velocity;
	set => _velocity = value;
}
```

Again - no Query Mode needed. The only way we're setting _velocity is via the setter - so it can never get out of date.

```csharp
Vector3 _velocity;
[Sync( SyncFlags.Query )]
public Vector3 Velocity
{
	get => _velocity;
	set => _velocity = value;
}

void SetVelocity( Vector3 val)
{
    _velocity = val;
}
```

Query Mode needed - because when you call `SetVelocity` it changes `_velocity` and the network system doesn't know that the `Velocity` value has changed. This could be avoided in that case by setting `Velocity` instead of `_velocity`.

With `SyncFlags.Query`, the variable is instead checked for changes every network update, and sent if changed. This is marginally slower than non-query mode, but it means that you can sync special stuff like this.

---

## Page: RPC Messages {#page-rpc-messages}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/rpc-messages/

Components can contain RPCs. An RPC is a function that when called, is called remotely too.

Supported RPC arguments are the exact same as Sync properties.

### Example

Imagine your game has a button, and you want it to make a bing noise when it's pressed. You could have a function like this.

```csharp
void OnPressed()
{
	Sound.FromWorld( "bing", WorldPosition );
}
```

The problem here is, that sound is only played on the host, or on the client where OnPressed is called. You want everyone to hear that sound. So you instead do something like this.

```csharp
void OnPressed()
{
	PlayOpenEffects();
}

[Rpc.Broadcast]
public void PlayOpenEffects()
{
	Sound.FromWorld( "bing", WorldPosition );
}
```

The attribute `[Rpc.Broadcast]` makes it so when you call that function, it broadcasts a network message to everyone to call that function too.

### Static RPC

Static methods can be RPCs, too. A static RPC does not need to exist on a Component but can exist as a method on any static class.

```csharp
[Rpc.Broadcast]
public static void PlaySoundAllClients( string soundName, Vector3 position )
{
	Sound.Play( soundName, position );
}
```

### Rpc.Owner

Unlike `[Rpc.Broadcast]` which calls the function for everybody, you can use `[Rpc.Owner]` instead which means that the function will only be called for the `Owner` of the networked object or the host if the object has no owner.

### Rpc.Host

Similarly to Rpc.Owner, adding this will mean the function is only called on the Host.

### Flags

When defining an RPC, you can define a number of flags.

```csharp
[Rpc.Broadcast( NetFlags.Unreliable | NetFlag.OwnerOnly )]
public static void PlaySoundAllClients( string soundName, Vector3 position )
{
  
}
```

| Name | Description |
| --- | --- |
| `NetFlags.Unreliable` | Message will be sent unreliably. It may not arrive and it may be received out of order. But chances are that it will arrive on time and everything will be fine. This is good for sending position updates, or spawning effects. This is the fastest way to send a message. It is also the cheapest. |
| `NetFlags.Reliable` | This is the default, so you don't need to specify this. Message will be sent reliably. Multiple attempts will be made until the recipient has received it. Use this for things like chat messages, or important events. This is the slowest way to send a message. It is also the most expensive. |
| `NetFlags.SendImmediate` | Message will not be grouped up with other messages, and will be sent immediately. This is most useful for things like streaming voice data, where packets need to stream in real-time, rather than arriving with a bunch of other packets. |
| `NetFlags.DiscardOnDelay` | Message will be dropped if it can't be sent quickly. Only applicable to unreliable messages. |
| `NetFlag.HostOnly` | This RPC can only be called from the Host. |
| `NetFlag.OwnerOnly` | This RPC can only be called from the owner of the object it's being called on. |

### Arguments

You can pass arguments to the RPC like any other method, and they'll get passed magically.

```csharp
void OnPressed()
{
	PlayOpenEffects( "bing", WorldPosition );
}

[Rpc.Broadcast]
public void PlayOpenEffects( string soundName, Vector3 position )
{
	Sound.FromWorld( soundName, position );
}
```

### Filtering

You can filter the recipients of a Broadcast RPC. This allows you to exclude specific connections from receiving the RPC, or only include specific connections.

```csharp
using ( Rpc.FilterExclude( c => c.DisplayName == "Harry" ) )
{
	PlayOpenEffects( "bing", WorldPosition );
}

using ( Rpc.FilterInclude( c => c.DisplayName == "Garry" ) )
{
	PlayOpenEffects( "bing", WorldPosition );
}
```

### Caller Information

You can check which connection called the method using the `Rpc.Caller` class.

```csharp
void OnPressed()
{
	PlayOpenEffects( "bing", WorldPosition );
}

[Rpc.Broadcast]
public void PlayOpenEffects( string soundName, Vector3 position )
{
	if ( !Rpc.Caller.IsHost ) return;

	Log.Info( $"{Rpc.Caller.DisplayName} with the steamid {Rpc.Caller.SteamId} played open effects!" );
	Sound.FromWorld( soundName, position );
}
```

---

## Page: Ownership {#page-ownership}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/ownership/

Networked GameObjects can be owned by a connection. When a connection owns an object, that client simulates and controls its position and variables. Unowned objects are simulated by the host.

### Owner Transfer Control

Objects can have different transfer behaviors via `SetOwnerTransfer()`:

- **Takeover**: Anyone can change ownership
- **Fixed** (default): Only the host can modify ownership
- **Request**: Changes require host approval

### Checking Ownership Status

Developers can retrieve the owner ID, but more practically, they check the `IsProxy` property to determine if another client or server is simulating the object.

### Taking and Dropping Ownership

Objects can be claimed via `TakeOwnership()` (useful for pickup mechanics or vehicle control) and released with `DropOwnership()`, returning simulation to the host.

### Spawning Behavior

Scene-placed networked objects default to host ownership, while objects created via `GameObject.NetworkSpawn()` are owned by the creating client.

### Disconnection Handling

The `SetOrphanedMode()` method controls what happens when an owner disconnects:

- **Destroy** (default): Object is removed
- **Host**: Host takes ownership
- **Random**: Another client takes ownership
- **ClearOwner**: Host simulates without ownership

---

## Page: Network Events {#page-network-events}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/network-events/

Games can respond to players joining and leaving by implementing specific component interfaces.

### INetworkListener

Provides three optional methods:
- `OnConnected` - triggered when a client connects and begins handshaking
- `OnDisconnected` - triggered when a client disconnects from the server
- `OnActive` - called after handshake completion, before the loading screen closes

### INetworkSpawn

Provides:
- `OnNetworkSpawn` - responds when an object spawns on the network

### Example

A `GameNetworkManager` component implements `INetworkListener`. When players join via `OnActive`, the example clones a player prefab, assigns the connection as owner, and retrieves a `NameTagPanel` component to display the player's name.

---

## Page: Network Visibility {#page-network-visibility}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/network-visibility/

Network Visibility determines whether a networked object should be visible for a specific player (Connection). This system controls which clients receive ongoing network updates like Sync Vars and Transform changes.

### Always Transmit Flag

By default, this setting is enabled, meaning all networked objects always transmit to all Connections unless explicitly disabled. When active, objects never get culled.

### INetworkVisible Interface

Developers can implement this interface on a component attached to a networked object's root GameObject to customize visibility logic:

```csharp
public class MyVisibilityComponent : Component, INetworkVisible
{
    public bool IsVisibleToConnection( Connection connection, in BBox worldBounds )
    {
        // Your visibility logic here
        return true;
    }
}
```

The method receives the target connection and the object's world-space bounding box for distance or frustum checks.

### Hammer PVS Integration

On Hammer maps with compiled VIS data, the engine automatically uses PVS (Potentially Visible Set) when no custom INetworkVisible component exists.

### Culling Behavior

When an object is culled from a connection:
- Sync Var and Transform updates stop transmitting
- The object still spawns on the client but becomes disabled locally
- RPCs continue to be delivered

---

## Page: Network Helper {#page-network-helper}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/network-helper/

The Network Helper is a specialized component designed to streamline multiplayer game development. It helps with common multiplayer requirements and can be used as an example to code your own network component.

### Automatic Server Creation

When the `StartServer` property is enabled, the system automatically establishes a server upon scene loading, provided the network system isn't already active from joining an existing server.

### Player Spawning System

The component manages player object creation when clients connect. Developers typically define a player GameObject as a prefab and assign it to the `PlayerPrefab` property. The system supports optional spawn points - players spawn randomly at designated locations, or at the NetworkHelper object's position if none are specified.

### Player Control Logic

Player objects should implement conditional control logic. Components should check the `IsProxy` property: "If we're a proxy then don't do any controls because this client isn't controlling us!"

### Technical Implementation

The Network Helper operates by implementing `Component.INetworkListener`. When a client connection activates on the server, the interface triggers creation of a PlayerPrefab instance, assigns ownership to the connecting client, and synchronizes this information across the network.

---

## Page: Custom Snapshot Data {#page-custom-snapshot-data}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/custom-snapshot-data/

Developers can add additional data to the network snapshot that gets sent to clients when they join a server. For example, voxel world data serialization.

### Implementation

Requires the `Component.INetworkSnapshot` interface.

**Writing Data:** Components override the `WriteSnapshot` method to serialize custom data using a ByteStream writer. Record the byte array length first, then write the array itself.

**Reading Data:** The `ReadSnapshot` method allows components to deserialize incoming snapshot data. Developers can return a Task to pause the loading screen while data processes. Read the length value first, then reconstruct the byte array, with subsequent loading handled in the `OnLoad` lifecycle method.

---

## Page: Testing Multiplayer {#page-testing-multiplayer}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/testing-multiplayer/

The number one best way to test multiplayer is to have someone join your game.. but that's obviously not always possible.

### New Instance

You can spawn another game instance to join your current session. Click the network status icon in the header bar and select "Join via new instance." This launches a second instance that automatically joins the active game.

### Iterating

Development work can continue on the primary instance while another client remains connected. Code modifications are automatically propagated to all connected clients, including instances belonging to other players who may be testing the game.

### Reconnect

The `reconnect` command enables users to restore connections when needed.

### Manual Joining

You can launch a separate instance and manually connect to your local editor session by executing `connect local` in the console.

---

## Page: Dedicated Servers {#page-dedicated-servers}
> Source: https://sbox.game/dev/doc/systems/networking-multiplayer/dedicated-servers/

### Installation

Install the s&box Dedicated Server via SteamCMD:

```bash
./steamcmd +login anonymous +app_update 1892930 validate +quit
```

Users can optionally use the `-beta staging` flag for the staging branch.

### Server Operation

Once installed in the default `steamcmd/steamapps/common/Dedicated Server` directory, servers launch via batch files. An example demonstrates loading a specific game package with a map and custom hostname.

### Configuration Parameters

The server accepts command-line switches including:
- `+game` for specifying game and map packages
- `+hostname` for server display name
- `+net_game_server_token` for persistent Steam ID association (post-release feature)

You can pass a path to a `.sbproj` file to load a local project on a Dedicated Server with hotloading capability.

---

## Page: Shader Graph Properties {#page-shader-graph-properties}
> Source: https://sbox.game/dev/doc/code/properties/ (redirects to Shader Graph)

### Blend Mode

Controls transparency handling:
- **Opaque** - Always full opacity, no support for transparency.
- **Masked** - No transparency support, but will use dithering with anything in-between 0 and 1.
- **Translucent** - Full transparency support, allows you to see-through any opacities between 0 and 1.

### Shading Model

Determines lighting interaction:
- **Lit** - Light will affect the material and can support Emissions, Normals, Roughness, Metalness, and Ambient Occlusion.
- **Unlit** - The material will output the raw colour to the screen regardless of the lighting in the Scene.

### Domain

Specifies shader application scope:
- **Surface** - Used for any materials that will be used on 3D Objects placed within your scene. Can have a set Shading Model.
- **Post Process** - Used for post-processing materials that will be applied to the screen. Uses Screen Coordinates instead of Texture Coordinates.

---

## Page: VR {#page-vr}
> Source: https://sbox.game/dev/doc/systems/vr/

### Key Components

Three main VR components:
- **VR Anchor** locks the player's playspace to a GameObject
- **VR Tracked Object** moves a GameObject to match a tracked device position
- **VR Hand** synchronizes hand models with controller skeletal data

### Setup Requirements

A basic VR setup should have both Left and Right eye targets in order to display properly inside the headset. The camera must be configured with both eye targets.

Head tracking requires a VR Tracked Object component on the camera, set up with the Head pose source.

### Controller Input

The API enables developers to read controller inputs and trigger haptic feedback, including accessing grip and trigger values and controlling vibration through parameters like duration, frequency, and amplitude.

### Detection

Developers can determine VR runtime status:

```csharp
if ( Game.IsRunningInVR )
{
    // VR-specific logic
}
```

A functional example is available in the sbox-scenestaging repository's "test.vr" scene.

---

## Page: Game Exporting {#page-game-exporting}
> Source: https://sbox.game/dev/doc/systems/game-exporting/

The s&box platform allows developers to export games as standalone executables for distribution on storefronts like Steam. Exported games operate without typical platform restrictions and can access standalone-exclusive APIs.

### Export Process

1. Access the Export Wizard via Project menu -> "Export..."
2. Configure optional elements: icon, splash screen, and Steam App ID (if applicable)
3. Proceed through the wizard and await project compilation
4. Locate the executable in your selected output folder

Your project's executable will be in the folder you selected. You can click 'Open Folder' to open the folder containing your game.

### Key Restriction

Your game must be put on Steam (it can be on other storefronts too, but Steam is a hard requirement).

### Important Caveat

You can export your game, but you shouldn't distribute your exported game just yet. Additional steps or approvals may be necessary before public release.

---

## Page: Assets: First Time Setup {#page-assets-first-time-setup}
> Source: https://sbox.game/dev/doc/editor/assets/

Guide for initial setup when creating clothing assets in S&box.

### First time Opening S&box

Open S&box and create a new project using the 'Addon' template. Enable "Show Base Content" in the asset browser. You will **never** need to add or adjust files within the core folders.

### Grabbing Citizen Files

File location: `Steam\steamapps\common\sbox\addons\citizen\Assets\models\citizen`

Reference file: `citizen_REF.fbx` provides citizen mesh and simple rig for building clothing around the citizen model.

### Grabbing Human Files

Files: `citizen_human_male_REF` and `citizen_human_female_REF`
Location: `sbox\game\addons\citizen\Assets\models\citizen_human`
Use case: Human versions of clothing.

---

## Page: Ready-to-Use Assets {#page-ready-to-use-assets}
> Source: https://sbox.game/dev/doc/assets/ready-to-use-assets/

Pages documenting some of the assets provided to developers. This is a navigation hub linking to:

- **First-Person Weapons** - `/dev/doc/assets/ready-to-use-assets/first-person-weapons/`
- **Citizen Characters** - `/dev/doc/assets/ready-to-use-assets/citizen-characters/`

---

## Page: HammerMesh Component {#page-hammermesh-component}
> Source: https://sbox.game/dev/doc/scene/components/reference/

The HammerMesh component is automatically added to game objects in Hammer that are connected to a mesh. It cannot be manually added; instead, users must tie a Mesh to a GameObject through Hammer's interface.

During map compilation, geometry converts into a model that loads at runtime on this component. By default, it procedurally generates both a ModelRenderer and ModelCollider using the compiled model.

Configuration options include:
- Standard ModelRenderer properties (optional - disable via right-click)
- Standard ModelCollider properties (optional - disable via right-click)

---

## Pages Not Found / Empty

The following URLs were attempted but returned 404, empty content, or required authentication:

- `https://sbox.game/dev/doc/scene/overview/` - Not Found
- `https://sbox.game/dev/doc/scene/gameobject/tags/` - Redirected to GameObject page
- `https://sbox.game/dev/doc/scene/gameobject/flags/` - Redirected to GameObject page
- `https://sbox.game/dev/doc/scene/components/overview/` - Empty
- `https://sbox.game/dev/doc/scene/scenes/loading/` - Empty
- `https://sbox.game/dev/doc/scene/transform/` - Not Found
- `https://sbox.game/dev/doc/scene/prefabs/` - Empty
- `https://sbox.game/dev/doc/code/overview/` - Not Found
- `https://sbox.game/dev/doc/code/attributes/` - Empty
- `https://sbox.game/dev/doc/code/hotloading/` - Empty
- `https://sbox.game/dev/doc/code/codegen/` - Empty
- `https://sbox.game/dev/doc/systems/input/` - Empty (multiple attempts)
- `https://sbox.game/dev/doc/systems/physics/` - Not Found
- `https://sbox.game/dev/doc/systems/physics/traces/` - Not Found
- `https://sbox.game/dev/doc/systems/physics/rigidbody/` - Not attempted (similar paths 404)
- `https://sbox.game/dev/doc/systems/physics/colliders/` - Not attempted (similar paths 404)
- `https://sbox.game/dev/doc/systems/networking/` - Empty (correct path is /networking-multiplayer/)
- `https://sbox.game/dev/doc/systems/networking/rpcs/` - Not Found
- `https://sbox.game/dev/doc/systems/networking/sync/` - Redirected to Async page
- `https://sbox.game/dev/doc/systems/networking/lobbies/` - Not Found
- `https://sbox.game/dev/doc/systems/networking/ownership/` - Empty
- `https://sbox.game/dev/doc/systems/audio/` - Not Found
- `https://sbox.game/dev/doc/systems/ui/razor/` - Empty (correct path is /razor-panels/)
- `https://sbox.game/dev/doc/systems/ui/panels/` - Not attempted
- `https://sbox.game/dev/doc/systems/ui/styling/` - Not attempted (correct path is /styling-panels/)
- `https://sbox.game/dev/doc/systems/rendering/` - Not Found
- `https://sbox.game/dev/doc/systems/rendering/camera/` - Not Found
- `https://sbox.game/dev/doc/systems/rendering/models/` - Not Found
- `https://sbox.game/dev/doc/systems/rendering/materials/` - Empty
- `https://sbox.game/dev/doc/systems/rendering/particles/` - Empty
- `https://sbox.game/dev/doc/systems/rendering/lighting/` - Empty
- `https://sbox.game/dev/doc/editor/overview/` - Empty
- `https://sbox.game/dev/doc/editor/hammer/` - Empty
- `https://sbox.game/dev/doc/editor/modeldoc/` - Empty
- `https://sbox.game/dev/doc/publishing/` - Not Found
- `https://sbox.game/dev/doc/publishing/overview/` - Rate limited
- `https://sbox.game/dev/doc/systems/shader-graph/` - Empty
- `https://sbox.game/dev/doc/systems/networking-multiplayer/http-requests/` - Empty
- `https://sbox.game/dev/doc/systems/networking-multiplayer/websockets/` - Empty
- `https://sbox.game/dev/doc/systems/networking-multiplayer/connection-permissions/` - Empty
- `https://sbox.game/dev/doc/systems/terrain/` - Empty
- `https://sbox.game/dev/doc/systems/clutter/` - Empty
- `https://sbox.game/dev/doc/dev-preview-first-steps/` - Empty
- `https://sbox.game/dev/doc/code/advanced-topics/` - Empty
- `https://sbox.game/dev/doc/code/advanced-topics/code-generation/` - Empty
- `https://sbox.game/dev/doc/code/libraries/` - Empty
- `https://sbox.game/dev/doc/systems/assetsresources/` - Empty
- `https://sbox.game/dev/doc/systems/movie-maker/` - Empty
- `https://sbox.game/dev/doc/systems/movie-maker/motion-editing/` - Empty
- `https://sbox.game/dev/doc/systems/ui/razor-panels/` - Empty
- `https://sbox.game/dev/doc/systems/shaders/gpu-instancing/` - Empty
- `https://sbox.game/dev/doc/systems/shaders/classes/depth/` - Empty
- `https://sbox.game/dev/doc/systems/shaders/classes/screen-space-tracing/` - Empty
- `https://sbox.game/dev/doc/systems/shaders/classes/texture-sheets/` - Empty
- `https://sbox.game/dev/doc/systems/shaders/classes/motion/` - Empty
- `https://sbox.game/dev/doc/systems/shaders/classes/bindless-api/` - Empty
- `https://sbox.game/dev/doc/systems/actiongraph/custom-nodes/action-resources/` - Empty
- `https://sbox.game/dev/doc/public-documentation/` - Empty
- `https://sbox.game/dev/doc/frequently-asked-questions/` - Not Found

---

## Page: Development {#page-development}
> Source: https://sbox.game/dev/doc/about/getting-started/development/

Creating a game in s&box is easy, but you probably want to know how to do things in the right order.

### Project Creation

Developers begin by launching the s&box Game Editor, where a project wizard guides them through establishing a new game project via the "New Game Project" button.

### Core Architectural Concepts

The platform uses three interconnected layers:

1. **Scenes** - Your game world containing all renderable and updatable elements that can be saved and loaded.

2. **GameObjects** - World entities possessing position, rotation, and scale properties, organized hierarchically so child objects move relative to parents.

3. **Components** - Modular functionality providers attached to GameObjects. A GameObject might have a `ModelRender` component which would render a model. It might also have a `BoxCollider` component which would make it solid.

Game developers ultimately create games by programming new Components and configuring scenes with GameObjects and Components.

---

## Page: Game Project {#page-game-project}
> Source: https://sbox.game/dev/doc/about/getting-started/project-types/game-project/

A game project is the foundational project type in s&box - it contains a game.

### Startup

Game projects typically define a startup scene in project settings that loads first. Developers have flexibility: they can create intro or menu scenes that load the main game scene upon user selection, or jump straight into the game without these intermediary scenes.

### Maps

When games load maps from a main menu, the startup scene doesn't execute. This creates a challenge: none of your game stuff is in that map. The solution involves creating a `GameObjectSystem` that spawns necessary UI and game manager elements.

```csharp
public sealed class MyGameManager : GameObjectSystem<MyGameManager>, ISceneStartup
{
    public void OnHostInitialize( SceneLoadOptions options )
    {
        options.IsAdditive = true;
        // Load an engine scene additively alongside the map
    }
}
```

### Metadata

The system supports project metadata configuration for publishing and identification.

---

## Page: Addon Project {#page-addon-project}
> Source: https://sbox.game/dev/doc/about/getting-started/project-types/addon-project/

An addon project adds to a Game Project. It functions as a supplementary development environment rather than a standalone product. The addon itself is never published directly; instead, creators develop and publish individual assets through it.

### Key Functionality

The primary purpose of addon projects is enabling creators to use the components and assets from a target game to create assets for that game. These assets can include maps, models, materials, or custom resources defined by the target game.

### Current Limitations

You can't make addon projects that contain code yet - but you can use ActionGraph. This indicates that while coding capabilities are restricted, alternative scripting methods remain available.

### Game Target Selection

Project settings allow developers to designate a target game. If you change this game then you must restart the editor for the changes to apply.

### Publishing Workflow

To distribute creations, developers locate their assets in the asset browser and initiate publication from there. Once published, individual assets receive dedicated pages on sbox.game where creators can configure distribution details and properties.

---

## Page: Code Basics (Cheat Sheet) {#page-code-basics}
> Source: https://sbox.game/dev/doc/code/code-basics/

This reference guide provides quick code snippets organized by category for developers working with the s&box engine.

### Debugging

```csharp
Log.Info( $"Hello {username}" );
```

Screen drawing via `DebugOverlay.ScreenText()`, and assertion checking with `Assert.NotNull()`.

### Transforms

Position manipulation examples for retrieving and setting GameObject positions in world and local space using properties like `WorldPosition` and `LocalPosition`.

### GameObjects

Finding objects by name or GUID, creating new instances, destroying them, controlling visibility, cloning, tagging, iterating children, and validity checking:

```csharp
Scene.Directory.FindByName( "MyObject" );
go.IsValid();
```

### Components

Component operations include:

```csharp
AddComponent<MyComponent>();
GetComponent<MyComponent>();
GetOrAddComponent<MyComponent>();
```

Also supports enabling/disabling, retrieving the parent GameObject, iterating all components, checking validity, and retrieving all active instances of a type.

---

## Page: Console Variables {#page-console-variables}
> Source: https://sbox.game/dev/doc/code/code-basics/console-variables/

### Console Commands

Console commands are just static methods with an attribute. The basic syntax uses the `[ConCmd]` attribute:

```csharp
[ConCmd]
public static void MyCommand( string arg1, int arg2 )
{
    Log.Info( $"Command called with {arg1} and {arg2}" );
}
```

The backend will try its best to convert the arguments from strings to the type you specify.

### Server-Specific Commands

Restrict commands to server execution by adding `ConVarFlags.Server`:

```csharp
[ConCmd( ConVarFlags.Server )]
public static void ServerCommand( Connection sender, string message )
{
    // sender tells you who ran the command
}
```

If you make the first parameter of the method have the `Connection` type, you'll be able to tell who actually ran the command.

### Console Variables

Variables are created using the `[ConVar]` attribute as properties:

```csharp
[ConVar( ConVarFlags.Saved )]
public static float MyVariable { get; set; } = 1.0f;

[ConVar( ConVarFlags.Replicated )]
public static int ReplicatedVar { get; set; } = 0;

[ConVar( ConVarFlags.UserInfo )]
public static string PlayerName { get; set; } = "Player";

[ConVar( ConVarFlags.Hidden )]
public static bool DebugMode { get; set; } = false;
```

### Game Settings Integration

You can use `ConVarFlags.GameSetting` to expose a setting to the game's creation screen, useful for configuring a game. Optional `Range` attributes constrain values.

---

## Page: Math Types {#page-math-types}
> Source: https://sbox.game/dev/doc/code/code-basics/math-types/

### Vectors

A **Vector** is just a set of numbers that describe a position or a direction in space. The engine provides `Vector2`, `Vector3`, `Vector4`, and their integer variants. Supports arithmetic operations, calculating length and distance, and interpolation between vectors.

### Rotation & Angles

A **Rotation** tells you which way something is facing in 3D. It's technically a Quaternion. **Angles** use pitch, yaw, roll and are easier to write manually but susceptible to gimbal lock, making `Rotation` preferable for code.

```csharp
// Convert between formats
Rotation rot = new Angles( 0, 90, 0 ).ToRotation();
Angles angles = rot.Angles();

// Extract directional vectors
Vector3 forward = rot.Forward;
Vector3 right = rot.Right;
Vector3 up = rot.Up;

// Interpolation
Rotation result = Rotation.Lerp( rotA, rotB, 0.5f );
```

### Transform

A **Transform** holds three things: a **Position (Vector3)**, a **Rotation (Rotation)**, and a **Scale (Vector3)**. Transforms appear as either `GameObject.WorldTransform` or `GameObject.LocalTransform`.

```csharp
// Converting between local and world space
Vector3 worldPos = transform.PointToWorld( localPos );
Vector3 localPos = transform.PointToLocal( worldPos );
```

---

## Page: API Whitelist {#page-api-whitelist}
> Source: https://sbox.game/dev/doc/code/code-basics/api-whitelist/

The API whitelist system restricts access to potentially dangerous classes and functions. We prevent access to classes and functions that could be used maliciously by whitelisting what can be used.

### Access Restrictions

Code failing whitelist checks won't load during gameplay, though editor code is exempt from restrictions. Non-compliant code generates compiler errors: `SB1000 Whitelist Error`.

### Publishing Requirements

Developers creating standalone games can disable the whitelist, but doing so prevents platform publication.

### Common API Alternatives

| Blocked API | Safe Replacement |
|---|---|
| `Console.Log` | `Log.Info` |
| `System.IO.*` | Custom Filesystem API |

### Reporting Issues

- **False Positives**: Submit requests to the GitHub issue tracker requesting API additions with supporting reasoning.
- **Security Vulnerabilities**: Report confidentially through Facepunch's security process -- never publicly disclosed.

---

## Page: Player Controller {#page-player-controller}
> Source: https://sbox.game/dev/doc/scene/components/reference/player-controller/

The `PlayerController` component enables first and third-person player control. It's designed to be simple and easy to use, getting you running around within seconds - with no programming.

### Physics-Based Architecture

The component functions as a specialized `RigidBody` integrated into the physics system, maintaining standard properties like velocity and mass while adding enhanced controllability.

### Built-In Input System

The controller includes optional input handling that allows GameObject owners to control movement via mouse, keyboard, or game controller. Alternatively, developers can manually control behavior by adjusting `EyeAngles` and `WishVelocity` during `OnFixedUpdate`.

### Camera System

Features both first-person and third-person camera modes togglable through configuration. This component is optional and can be disabled via the camera tab.

### Animation Support

Includes an optional animator for objects with Citizen compatible AnimGraph animations.

### Event System

The `PlayerController` broadcasts events through the `IEvents` interface, enabling sibling components to respond to gameplay moments:

- `OnEyeAngles()` - Eye angle adjustments
- `PostCameraSetup()` - Camera setup completion
- `OnJumped()` - Player jumping
- `OnLanded()` - Landing detection with fall distance tracking
- Interaction callbacks for pressable components

---

## Page: ISceneStartup {#page-iscenestartup}
> Source: https://sbox.game/dev/doc/scene/components/events/iscenestartup/

The `ISceneStartup` interface enables developers to respond to scene initialization events across three contexts: in editor when pressing play, during game loading, and when joining servers.

### Interface Methods

**OnHostPreInitialize**: Executes before scene loading when the scene remains empty. This typically applies only to `GameObjectSystems` since no Components exist yet.

**OnHostInitialize**: Runs after the host loads the scene, allowing access to GameObjects and Components. This is a good place for the host to spawn common things.

**OnClientInitialize**: Fires after scene loading on both host and client (excluding dedicated servers). Mark spawned objects as non-networked to prevent unintended replication.

### Key Terminology

"Host" refers to the computer in charge of the game. This could be the player in a singleplayer game, a dedicated server host, or a host in a lobby.

### Example

```csharp
public sealed class GameManager : GameObjectSystem<GameManager>, ISceneStartup
{
    public void OnHostInitialize( SceneLoadOptions options )
    {
        options.IsAdditive = true;
        // Spawn an engine scene additively and create a lobby
    }
}
```

---

## Page: Services (Stats) {#page-services}
> Source: https://sbox.game/dev/doc/systems/services/

Your game can record stats for each player that plays it.

### Recording Stats

Two primary recording methods:

**Increment Method** - For counting occurrences:
```csharp
Sandbox.Services.Stats.Increment( "zombies-killed", 1 );
```

**Set Value Method** - For direct value assignment:
```csharp
Sandbox.Services.Stats.SetValue( "win-time", SecondsTaken );
```

Stats are batched and sent when ready, so you can call the API frequently without performance concerns.

### Reading Stats

Retrieve statistics in three ways:

- **Global stats**: Aggregate data across all players
- **Local player stats**: Individual player metrics
- **Specific player stats**: Historical data for particular users

Queries return objects with `Sum` and `Players` properties for aggregate values and player counts.

### Example Use Cases

Zombie kills, completion times, distance traveled, collected items, weapon-specific metrics.

---

## Page: Cloud Assets {#page-cloud-assets}
> Source: https://sbox.game/dev/doc/systems/assetsresources/cloud-assets/

A large selection of Assets (Textures, Models, Sounds, etc.) are available to use on sbox.game without requiring manual file management.

### Cloud Browser

The Cloud Browser enables instant asset integration into scenes. Assets download automatically and cache locally for efficient reuse.

### Code Property References

Expose variables as Component or GameResource Properties to reference both local and cloud assets:

```csharp
public Model MyModel { get; set; }
```

### Direct Code References via Ident

The `Cloud.*` functions allow direct asset access using multiple notation formats:

```csharp
Cloud.Model( "facepunch.w_usp" );        // Dot notation
Cloud.Model( "facepunch/w_usp" );        // Slash notation
Cloud.Model( "https://sbox.game/facepunch/w_usp" );  // Direct URL
```

Assets download during compilation and ship with packages, meaning later modifications don't auto-update existing packages.

### Runtime Mounting

For dynamic scenarios (spawnable props, procedural generation), fetch and mount packages asynchronously:

```csharp
var package = await Package.Fetch( "facepunch.w_usp" );
await package.MountAsync();
// Access primary assets via metadata
```

---

## Page: GameResource Extensions {#page-gameresource-extensions}
> Source: https://sbox.game/dev/doc/systems/assetsresources/gameresource-extensions/

ResourceExtensions allow developers to append additional data to existing GameResources without modifying the original class or assets, useful for augmenting resources like Clothing, Surfaces, and Models.

### Creating Extensions

```csharp
[GameResource("Clothing Extension", "extcloth", "Extra Clothing Stuff", Category = "Citizen", Icon = "iron")]
public class ClothingExtension : ResourceExtension<Clothing, ClothingExtension>
{
    public string CustomName { get; set; }
    public string CustomCategory { get; set; } = "Other";
    public int CostToUnlock { get; set; } = 100;
}
```

The first generic type parameter specifies the resource you are extending.

### Asset Extension Setup

Extensions are configured through asset creation, with an "Extends" tab for specifying target assets. A "Default" option enables the extension as the fallback for assets lacking a specific extension.

### Access Methods

- **`FindForResourceOrDefault()`** - Returns the marked default if no specific extension exists.
- **`FindForResource()`** - Returns null when no extension is present (requires null-checking).
- **`FindAllForResource()`** - Retrieves all extensions targeting a resource, excluding defaults unless they also target the resource.
- **`FindDefault()`** - Retrieves the default extension without requiring a resource parameter.

---

## Page: ActionGraph {#page-actiongraph}
> Source: https://sbox.game/dev/doc/systems/actiongraph/

ActionGraphs let you throw together little scripts using nodes instead of code. It is a visual scripting system allowing developers to create functionality using nodes.

### Documentation Structure

- **Intro to ActionGraphs** - Foundational concepts
- **Component Actions** - Action-related components
- **Using With C#** - Integration with C# code
- **Variables** - Variable management
- **Custom Nodes** - Creating custom node types
- **Examples** - Practical demonstrations

An example graphic demonstrates jumping and falling with gravity mechanics built through node-based visual scripting.

---

## Page: Intro to ActionGraphs {#page-intro-to-actiongraphs}
> Source: https://sbox.game/dev/doc/systems/actiongraph/intro-to-actiongraphs/

Each node describes an action or expression, and links between nodes carry values or signals.

### Node Types

- **Root Node**: The entry point, containing one signal output socket and optional value sockets for parameters.
- **Expression Nodes** (green): Perform calculations without state changes; evaluate when their outputs are needed.
- **Action Nodes** (blue): Execute when receiving signals; typically have input/output signal sockets.

### Node Creation

Right-click in empty space or drag from an output socket to access creation menus filtered by value type.

### Links and Connections

Links transport either signals (between white arrow-shaped sockets) or values (other connections). The system prevents cyclic connections -- loops must use dedicated control flow nodes:

- **If** - Conditional branching
- **While** - Loop while condition is true
- **For Each** - Iterate over a collection
- **For Range** - Iterate over a numeric range

Variables and reroute nodes help manage visual complexity.

### Configuration

Constant values can be set directly in the Properties panel when a node is selected, rather than requiring separate input nodes.

### Getting Started

Users can create ActionGraphs through built-in component actions, the Component Editor, or by adding Delegate-type properties in C#.

---

## Page: ActionGraph Examples {#page-actiongraph-examples}
> Source: https://sbox.game/dev/doc/systems/actiongraph/examples/

Practical examples for common ActionGraph tasks with copyable clipboard data.

### Example 1: Loop Through Child Objects

Uses For Each on the list of child objects, firing the Body signal on each child.

```
actiongraph:H4sIAAAAAAAACp1UW0+DMBR+X7L/0OArQ5huGb4ZTcyi0SVeXoxZCpyxKrSkLWbLwn+3pbjVMc3w6XBu3/nOhW76PYScW0IT5wI5l7EkjDpubXzBnOAoA6E8r2/Gds8So2sNoY0RyjHV+aOJuzU8rQvQkCIGCl4KkkXvEEtnF/EsgF9jiVXUFkeZZ0yQmoVKHozGvjs4G0+c74DKfFTuYQJhm0DBWQFcrjUHq/rMmEndjl1/LpvER0yTiK28G5zDqa087HWik6iy66SrJckSDnRHuFvD536nhtV4Wg0vGAccLztOOgiHnQoH7cIZSz1CF8yqPKVFKfcnrAjmWG6nhU42QaUi/OqfQwuGk8PUtWgO947QD+twG4F+nOycgygza7H2OUnM9QEZ3YDaMOEfMPaSgJY5cP1b/QZlR0csWVs41szngqQUZ8eCeJ84K+EwlGpM7Dz+sYjqyYDVMYhBg1hvo9+rvgCTVPEacwQAAA==
```

### Example 2: Move With Velocity

Adds a velocity, multiplied by the time since the last frame, to the position of this game object.

```
actiongraph:H4sIAAAAAAAACr1U30+DMBBB+X7L/gfDMENjGmG8mJsZodIlzL8YsBaqpQkvabnFZ9r/b0s3qDALzR3govet9333t3W37PcMwbxCOzXPDvIi4Iti0cuMCUATCBDLheXpWtjsSq73cGcZWLcJxLeP9sVUY5psMSkgWQQztV8hJ+AYjbpYnHhmkl4ADcarAEeYZYSjPQgQPxl5gDRzz4N6pn511nN6v0meUZJDyjcxA454pM8rF6OxLvg98ADgOyYd9BVJ4pm/uv+mQQVjYZdCcAsxeCE3LjLvpHfpd9E4a9LLf0ltqOiq5SP80xZ4TWK31Bv/xvn+rd+D6juWP2iqeVhWTzAZx3K2PRoHlekFL0olTJeUohXYME8HSsaCHfhdq96jedJVwlCUbs/tVu1OvLbdX5Y4IZtxew2jYprYOJbKACYkQ19IVzjVIVrnXdRxLfCdPCHGhnlctILnsh/Qtwu/akN4vxpfxvKSQiVstU9SHJwdUtpPaK1Adxm+C0WbST2CC1jBBE4zWP6AWQTvUqEc9YQ2K3jUVFL2ua/PQDzUqCWsRtBJuzKFAyKum39t9Ai3avLMHCAAA
```

---

## Page: Shaders {#page-shaders}
> Source: https://sbox.game/dev/doc/systems/shaders/

Developers can create shaders through code or the ShaderGraph.

### Shader Types

1. **Material Shaders** - For rendering objects in world space
2. **Post Processing Shaders** - Full screen effects
3. **Compute Shaders** - GPU compute operations

### Creation and Compilation

Shaders are created via the Asset Browser using HLSL in a VFX wrapper combining vertex and pixel stages together. The system automatically recompiles and hotloads changes upon saving.

### Recommended Development Setup

Use VSCode paired with the Slang Extension for an optimal editing experience. This provides a full IDE experience with intellisense showing available functions and properties.

### VSCode Configuration

When opening a project folder in VSCode, the system prompts to install the Slang extension and automatically configures:
- File associations
- Workspace flavor
- Search paths

Ensure the language mode is set to **Slang** and the workspace flavor is set to **vfx** for proper functionality.

---

## Page: Shading Model {#page-shading-model}
> Source: https://sbox.game/dev/doc/systems/shaders/shading-model/

Shading models are the final step in shader processing that applies lighting and atmospheric effects to materials.

The core workflow: **Input Interpolants > Material Definition > Output Shading Model**

### ShadingModelStandard

The default implementation:

```hlsl
Material m = Material::From( i );
return ShadingModelStandard::Shade( m );
```

The `Shade()` method consumes the material set from `m` and does the standard lighting from it, returning a `float4` output.

### Custom Implementation

For custom art styles, segment lighting into **Direct/Indirect** categories, subdivided into **Specular/Diffuse** types.

A toon shader example demonstrates:
- Direct lighting loop processing dynamic lights
- Indirect lighting handling ambient occlusion and environment mapping
- Debug view support (`DepthNormal`, `ToolVis`)
- Atmospheric fog application as a final composite step

This modular design allows customized lighting while maintaining compatibility with the engine's rendering pipeline and debug systems.

---

## Page: Dynamic Reflections {#page-dynamic-reflections}
> Source: https://sbox.game/dev/doc/systems/shaders/classes/dynamic-reflections/

The Dynamic Reflections system enables compositing specular reflections onto objects, supporting both Screen Space Reflections (SSR) and future raytraced solutions.

### Direct Sampling

```hlsl
float3 DynamicReflections::Sample( float4 ScreenPosition, float Roughness )
```

### Integration with Shading Model

On the standard shading model, reflections are composited with the correct BRDF, respecting the reflection value from Metalness and Roughness.

### Custom Implementation

Set the reflection texture globally through commands:

```csharp
commands.SetGlobal( "ReflectionColorIndex", PlanarReflection.ColorIndex );
```

**Roughness Parameter**: Values from `0.0f` to `1.0f` determine mip level selection for variable blurriness based on material roughness.

### Disabling Reflections

Override and disable indirect specular reflections entirely:

```csharp
commands.SetGlobal( "ReflectionColorIndex", Texture.Black.ColorIndex );
```

---

## Page: G-Buffer {#page-g-buffer}
> Source: https://sbox.game/dev/doc/systems/shaders/classes/g-buffer/

The G-Buffer captures essential object data during the depth pre-pass phase. Materials writing to the depth pre-pass also write to a G-Buffer if using the standard ShadingModel.

### Key Data Stored

Minimal information about the object before the lighting pass, like Normals and Roughness. This data supports post-processing effects including ambient occlusion and screen-space reflections.

### Sampling API

```hlsl
float3 Normals::Sample( int2 ScreenPosition )
float3 Roughness::Sample( int2 ScreenPosition )
```

### Fallback Behavior

If the object in that texel does not write to the G-buffer, then it reconstructs normal maps from it.

### Requirement

To enable G-Buffer functionality with the standard shading model, ensure a `Depth();` mode is enabled in your material configuration.

---

## Page: Movie Maker: Getting Started {#page-movie-maker-getting-started}
> Source: https://sbox.game/dev/doc/systems/movie-maker/getting-started/

### Core Components

**Movie Editor**: Accessible via the View menu and can be docked as a tab in the lower panel.

**Movie Player**: A required scene component that decides which objects in the scene should be animated by a particular movie, and controls the current playback position. Players can be created via an on-screen button or through the Movie Maker dropdown menu.

**Movie Resources**: Films can be embedded within a Movie Player or saved as separate `.movie` asset files for reuse. The File menu provides options for saving and loading.

### Track Types

1. **Reference Tracks**: Connect to GameObjects or Components in the scene.
2. **Property Tracks**: Represent a property somewhere in the scene and describe how it should animate (e.g., camera position, light color, or text).
3. **Sequence Tracks**: Import segments from other movies to organize larger projects.

Tracks can be created by dragging from the hierarchy or inspector into the track list, or via right-click context menus. Sequence tracks are automatically generated when importing movies.

### Learning Path

Progress through Keyframe Editing, Motion Editing, Recording, Sequences, and API integration for more advanced workflows.

---

## Page: Movie Maker: Editor Map {#page-movie-maker-editor-map}
> Source: https://sbox.game/dev/doc/systems/movie-maker/editor-map/

### Toolbar

Controls organized into five categories:

**Project**: Create, open, or import movies and manage movie player selection with modification history tracking.

**Playback**: Recording, playback controls, looping, speed adjustment, and synchronizing all movie players in the scene.

**Edit Modes**: Switch to keyframe editor mode for simpler animations, or motion editing for finer control.

**Snapping**: Object snap, frame snap, and frame rate settings.

### Track List

Displays project navigation showing parent and current movies, plus track types: Game Object, Component, Property, and Sequence tracks.

### Track Controls

Expand/collapse, lock/unlock, selection, and modification toggles.

### Timeline

Interactive workspace with keyboard shortcuts:
- **Shift** - Smooth preview
- **Scroll** - Vertical navigation
- **Shift+Scroll** / **Middle-Click+Drag** - Horizontal panning
- **Ctrl+Scroll** - Zoom
- **Alt+Scroll** - Frame scrubbing

### Regions & Markers

Scrub bar, tracks, sequence blocks, playhead markers, preview markers, frame ticks, and loop start/end indicators.

The interface supports nested movie editing through sequence tracks and provides visual feedback through gizmos in the scene view.

---

## Page: Movie Maker: Skeletal Animation {#page-movie-maker-skeletal-animation}
> Source: https://sbox.game/dev/doc/systems/movie-maker/skeletal-animation/

Animating skinned characters in the editor using Movie Maker. The skeletal animation tools activate when you have at least one `SkinnedModelRenderer` in your movie with a `Bones` sub-track. Work in Motion Editor mode.

### Animation Base Approaches

**Importing Sequences**: Load animations directly from models by selecting a time range, right-clicking on the track, choosing "Import Anim Sequence," and applying the change. Root motion is included by default.

**AnimGraph Generation**: Create animations through a multi-step process:
1. Generate rotations matching movement direction via "Rotate with Motion"
2. Convert motion into "AnimGraph Parameters"
3. Bake parameters into bone tracks via "AnimGraph Parameters to Bones"

**Gameplay Recording**: Bone data can be captured during play mode if you pre-create the necessary tracks.

**Legacy Upgrades**: Animations using the older "Create Bone Objects" method can be modernized through a context menu upgrade option.

### Pose Manipulation

Drag bones to trigger inverse kinematics chains. Hold **E** to rotate bones in place. **Shift-click** locks/unlocks bones to constrain specific areas -- useful for finger posing by locking the hand.

This approach works best when locking ancestor bones rather than descendants.

---

## Page: Movie Maker: Playback API {#page-movie-maker-api}
> Source: https://sbox.game/dev/doc/systems/movie-maker/api/

The Playback API enables developers to define and control animations through C# using tracks and clips.

### Tracks

Tracks represent bindable elements -- GameObjects, Components, or properties:

```csharp
MovieClip.RootGameObject( "Camera" );  // Reference track
```

Property tracks support two animation methods: constant values across time ranges and sampled data. You can provide constant values between 0s and 2s, then a different constant value from 2s to 5s.

### MovieClips

Aggregate multiple tracks for synchronized playback:

```csharp
clip.GetReference( "Camera" );
clip.GetProperty<float>( "FieldOfView" );
```

Clips can be serialized to JSON format.

### Binding and Playback

The **TrackBinder** system connects tracks to actual scene objects:

```csharp
target.IsBound;  // Check binding status
```

Multiple binder instances enable the same clip to control different objects across scenarios.

**MoviePlayer** is a component that manages playback, combining a clip, binder, and time position:

```csharp
var resource = ResourceLibrary.Get<MovieResource>( "path/to/movie" );
```

The player supports automatic target creation with the `CreateTargets` property.

---

## Page: Editor Shortcuts {#page-editor-shortcuts}
> Source: https://sbox.game/dev/doc/editor/

### Selection

| Action | Input |
|--------|-------|
| Select | LMB |
| Add to Selection | Shift + LMB |
| Remove from Selection | Ctrl + LMB |
| Invert Selection | Ctrl + I |
| Box Select | LMB + Drag |
| Add to Box Selection | Shift + LMB + Drag |
| Remove Box Selection | Ctrl + LMB + Drag |
| Lasso Selection | Alt + Shift + LMB |
| Remove Lasso Selection | Alt + Ctrl + LMB |

---

## Page: Editor Tools {#page-editor-tools}
> Source: https://sbox.game/dev/doc/editor/editor-tools/

Custom editor tools extend the engine's functionality within editor projects.

### Basic Structure

```csharp
[EditorTool]
[Title( "My Tool" )]
[Icon( "my_icon" )]
[Shortcut( "mytool.activate", "T" )]
public class MyTool : EditorTool
{
    public override void OnEnabled()
    {
        // Initialization
    }

    public override void OnDisabled()
    {
        // Cleanup
    }

    public override void OnUpdate()
    {
        // Per-frame logic
    }
}
```

### Core Methods

- **`OnEnabled()`** - Initialization when tool is activated
- **`OnDisabled()`** - Cleanup when tool is deactivated
- **`OnUpdate()`** - Per-frame update logic

### Scene Access

Tools interact with the scene through a `Scene` member, enabling trace operations and gizmo rendering for visual feedback.

### Selection Control

The `AllowGameObjectSelection` property manages whether users can click to select GameObjects. Useful for specialized tools requiring full input control.

### UI Overlay System

Create interface elements using `WidgetWindow` and attach them to `SceneOverlay`. The `AddOverlay()` method ensures proper cleanup when tools are deactivated.

---

## Page: Component Editor Tools {#page-component-editor-tools}
> Source: https://sbox.game/dev/doc/editor/editor-tools/component-editor-tools/

Component Editor Tools remain active whenever a specific Component is selected. They typically generate UI elements within the scene view and can intercept user input.

### Implementation

```csharp
public class MyEditorTool : EditorTool<MyComponent>
{
    public override void OnEnabled()
    {
    }

    public override void OnUpdate()
    {
    }

    public override void OnDisabled()
    {
    }

    public override void OnSelectionChanged()
    {
        var target = GetSelectedComponent<MyComponent>();
    }
}
```

### Key Methods

- **`OnSelectionChanged()`** - Invoked after the tool is instantiated and registered. May be called again if the selection changes to a different component instance.
- The tool automatically undergoes cleanup and destruction when the selection no longer includes the corresponding component type.

---

## Page: Editor Widgets {#page-editor-widgets}
> Source: https://sbox.game/dev/doc/editor/editor-widgets/

Editor UI is built entirely out of Widgets, which differ from in-game UI panels.

### Root Widgets

When a Widget lacks a parent, it functions as a root widget that creates an operating system window.

### Widget Structure

Each Widget contains a Layout that can hold child Widgets or sub-Layouts, and can be styled using CSS similar to in-game panels.

### Implementation Patterns

**1. Basic Widgets**: Created by extending the `Widget` class with customizable layouts, spacing, margins, and styling.

**2. Dockable Widgets**: Using the `[Dock]` attribute enables widgets to be docked within `DockWindow` containers and toggled through the View menu.

**3. Asset Editors**: The `[EditorForAssetType]` attribute allows widgets to open automatically when users double-click specified assets. Requires inheriting from `Window` and implementing `IAssetEditor` interface, which provides the `AssetOpen()` method.

---

## Page: Custom Editors {#page-custom-editors}
> Source: https://sbox.game/dev/doc/editor/custom-editors/

Create custom editors for classes, structs, and assets using two approaches.

### Custom ControlWidgets

Create specialized editors for specific data types -- e.g., a Gradient Editor so you can visually see what the Gradient looks like instead of editing it as if it were a Struct with a list of Colours.

```csharp
[CustomEditor( typeof( MyClass ) )]
public class MyClassWidget : ControlObjectWidget
{
    public override bool SupportsMultiEdit => true;

    public MyClassWidget( SerializedProperty property ) : base( property )
    {
        var color = SerializedObject.TryGetProperty( "Color" );
        var name = SerializedObject.TryGetProperty( "Name" );

        Layout = Layout.Row();
        // Arrange properties in a row
    }
}
```

### Custom InspectorWidgets

Replace the entire inspector interface, useful for Editor Tools and Assets:

```csharp
[Inspector]
[CanEdit( "asset:char" )]
public class MyInspector : Widget, IAssetInspector
{
    // Custom header with CSS-like styling
    // ControlSheet for property management
    // LoadResource<T>() for asset loading
    // EditorEvent.Hotload for hot reload support
}
```

Both approaches support attribute-based filtering for targeted editor application.

---

## Page: Editor Apps {#page-editor-apps}
> Source: https://sbox.game/dev/doc/editor/editor-apps/

Editor Apps are apps that run in the editor. They generally have their own window and are sometimes used to modify specific asset types. ShaderGraph, Material Editor, and Model Editor are examples.

### Creation

```csharp
[EditorApp( "Example App", "pregnant_woman", "Inspect Butts" )]
public class MyEditorApp : Window
{
    public MyEditorApp()
    {
        WindowTitle = "Hello";
        MinimumSize = new Vector2( 300, 500 );
    }
}
```

Once created, the application becomes accessible through the App sidebar and the main Apps menu within the editor.

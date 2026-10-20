# User Manual

## Architecture

### Game Logic

- The game logic is concentrated in the `GameManager` object located under the `Game` parent in the `Scene` view.
- `GameManager` acts as a parent class that assigns her children specific tasks. These children are components of the `GameManager` GameObject. 

### Events

How does the UI send information about button presses to the Game?

- Instead of UnityEvents, the project uses ScriptableObjects as Events, which are kinda the same but offer more modularity. Which means that they're just the same functionally and I just wanted to use them just cause they're cool.
- To use an Event, create a ScriptableObject by right-clicking anywhere in the `Assets` folder tree and creating a new file under `ScriptableObjects > Events > Event`.
  - There are also different events depending on what value you want to return! If one doesn't exist, it hasn't been made manually yet.
- A script that wants to reference this Event has to hold a reference to it and then later `Invoke()` it themselves.
- Scripts that want to listen to this specific Event must have an `EventListener` attached to them that references the Event and a UnityEvent containing the components that need to respond.
- So: it's a more complicated UnityEvent. I promise you it's pretty cool for bigger projects.
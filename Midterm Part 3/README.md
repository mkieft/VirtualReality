# Midterm-Part-3: Headset Construction

## Setting Up a Github Repo
It is a requirement for this project for every team to have a github repository. In class, we went over this portion of the assignment, but just in case you missed it here is a quick recap.

First, make sure everyone on your team has a Github account. One person will need to "own" the repository, but don't worry, all of the contributions you make are tracked by the profile of who commits them. When creating the repository, before you hit the big green button to finalize creation, make sure you add a *.gitignore* file for Unity from the gitignore dropdown. After the repository is created, go into its settings and add the rest of your team as collaborators.

Now that you have a repo created, using whatever tool you prefer ([Github Desktop Client](https://desktop.github.com/) *recommended*, [Git Kracken](https://www.gitkraken.com/), [Git Bash](https://git-scm.com/downloads)), clone the project on to your computer. Once you have cloned the project, create a new Unity project with Unity Hub in the cloned folder. From there, follow the instructions of your chosen tool to make your first commit and push the code to github.

## Importing the Qualisys Library
The manufacture of the motion capture system we will be using is [Qualisys](https://www.qualisys.com/). You can check out their website here. They also have a [Unity plugin](https://www.qualisys.com/software/unity/) that will make our lives easy. You can install [qualisys plugin](http://www.qualisys.com/download/Qualisys-Real-Time-Streaming.unitypackage) by downloading the package, then opening up your Unity project and in the top menu selecting **Assets->Import Package->Custom Package** and selecting the downloaded file. If you look in the Assets section of the project in Unity, you should now see a project folder.

## Connecting to the Tracking System
I've created some simple scripts that are going to make it easy to connect to the Qualisys Track Manager (QTM), they are in this repo and called QTMConnector.cs and QTMObject.cs; download these scripts and add them to your project.
- **QTMConnector**: Manages connection with the QTM server, and connects automatically on start.
- **QTMObject**: Using the object name supplied, applies transforms calculated by the QTM system to the tracked object

Create an empty object named *Connection Manager* and attach the `QTMConnector` script to it so that we can connect to the system. Once the script is attached, set the `host` property in the editor to `192.168.1.100` (this is the ip of the QTM server). Now let's create another game object to test the system, any primitive shape will do, and attach the `QTMObject` script to it. Before we can test it out though, you will need to record an object in the system with me. *I will give you instructions on how to do this in person.* Once your object is recorded, enter the name you chose into the `Object Name` field of the QTMObject component on your test object. The last step before we can test is to connect to the right Wifi. The network name is **The Grid** and I will give you the password in person. Once you are connected, hit the play button and move your physical object, if the shape moves and rotates in the app then everything is working!

## Connecting Our Headset
The logic for computing our camera position and location is going to require us to use a parent object like we did in assignment 3, and a transform proxy for the QTM data. In the end we will need to fuse the data of the phone accelerometer with the motion capture data to get make it comfortable. But first let's see what it looks like without any help from the phone's IMU.

Create an empty game object and name it `Camera Parent`, then add the Main Camera to the camera parent. Attach the `QTMObject` to the Camera Parant, and give the QTMObject component the name of your headset as it was registered with the QTM system.  Make sure you add some objects to the scene so you can get a reference for how you are moving around. Try it out!... It's quite terrible isn't it? That's because the accelerometer is conflicting with QTM transform data.

## Making Our Tracking Better
To fix the errors in the rotation component of our tracking, we will need to find the difference between the target rotation (QTM's data) and the local more responsive rotation (the Phone's IMU). Once we have this difference, we will interoplate between the two over time.

First let's create a transform proxy. Remove the QTMObject from the camera parent, then create a new empty object at the scene level called `Camera Proxy`. Attach the QTMObject script to `Camera Proxy` and give it the ID that your headset was registered to in QTM. Now create a script called `CameraTransformCorrection` and add it to the `Camera Parent`, we will use this script to correct the rotation issues. In the CameraTransformCorrection script add the following properties to the class:
```
public GameObject mainCamera;
public GameObject cameraProxy;
public float lerpSpeed; // speed at which to interpolate between the proxy and main camera
```
Then in the Update function, add the following:
```
// Calculate the difference in rotation between the proxy rotation and current main camera rotation
Quaternion correction = cameraProxy.transform.rotation * Quaternion.Inverse(mainCamera.transform.localRotation);
// Set the rotation value of this camera to be a portion of the offset, over time this will correct slowly
transform.rotation = Quaternion.Lerp(transform.rotation, correction, lerpSpeed);
// copy the transform position
transform.position = cameraProxy.transform.position;
```
Then in the editor, attach the `Camera Proxy` object and the `Main Camera` to the `CameraTransformCorrection` component on the parent. Adjust the Lerp speed to what feels good to you. My phone seems to function best with `0.02` as the speed.

## Drawing in Mid Air
We are one script, and one tracked object away from having a simple drawing tool put together, yay! Before you can complete this section, you will need to attach markers to your controller and register it with me in the QTM system.

First, let's create our tracked object. Create an empty game object and name it `Pen`, then attach the `QTMObject` script and provide it with the name you gave your controller when you registered it. Let's also add two child objects, one cube and one sphere. Name the cube `Body` and the sphere `Point` and position them so that in VR it will seem like you could paint with the tip as you move the controller.

Now let's create a prefab for our drawings. Create a new empty game object and add a `Line Renderer` component to it. We will also want to tweak a couple of values of `LineRenderer` to make things look nice; Set `Positions->Size` to `0` so that we don't have any extra points, and in the Graph looking thing that says `Width`, change the value at the top left of the axis from `1` to `0.05` (this actually lets you set the line thickness for each point). Now take the Stroke object from the inspector and drag it into Assets to create the prefab.

All we need now is a script to add strokes to the scene and passes new points to them. Add a new script to the pen point and name it `PenStroke`. Add the following class variables:
```
private GameObject currentStroke; // Holds the current stroke being drawn
public GameObject strokePrefab; // Reference to the stroke prefab we created before
private bool isDrawing = false;
public float minDistance = 0.01f; // Distance required to move the pen before a point will be added
```
Now in the `Update` method we will need to add our code for creating strokes and adding points to them. First let's create an `if` statement that will set `isDrawing` to true and create a new stroke when the button is pressed.
```
// Check if button is down
if (Input.GetButtonDown("Fire1")) {
    isDrawing = true;
    // Create new stroke
    currentStroke = Instantiate(strokePrefab);
}
```
Below that, still inside the Update method we need to add code for adding points to the stroke during the main loop. We will also add some code that will prevent points from being added when we haven't moved far enough from the last point.
```
// While the pen is drawing
if (isDrawing) {
    // Get a reference to the line renderer
    LineRenderer line = currentStroke.GetComponent<LineRenderer>();

    // Automatically add a point if there are no positions
    if (line.positionCount == 0) {
        line.positionCount += 1;
        line.SetPosition(line.positionCount - 1, transform.position);
    } else {
        // Get a reference to the last point added
        Vector3 lastPosition = line.GetPosition(line.positionCount - 1);
        // compute a vector that points from the last position to the current position
        Vector3 distanceVec = transform.position - lastPosition;
        
        // Add a point if the current position is far enough away
        if (distanceVec.sqrMagnitude > minDistance * minDistance) {
            line.positionCount += 1;
            line.SetPosition(line.positionCount - 1, transform.position);
        }
    }
}
```
Test it out, and you should be drawing in VR!

## Submitting
Submit a link to your github repo on canvas, and demonstrate to me the final product either in person or with a video submitted on canvas.

## Bonus
Neither of these are required for this assignment, just suggestions for if you want to take this further.
- Add another type of brush, maybe one that's flat, or a tube **Up to 4pts**
- Add a color selector, must support at least 5 colors **2 pts**

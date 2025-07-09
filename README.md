<p align="center">
https://github.com/user-attachments/assets/14de8223-caf6-4336-89c3-41bcafd1e4d3](https://github-production-user-asset-6210df.s3.amazonaws.com/139680225/464216339-14de8223-caf6-4336-89c3-41bcafd1e4d3.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250709%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250709T134617Z&X-Amz-Expires=300&X-Amz-Signature=fae2de00678fc3c3966bdb613d499c696640011b68900145fa91f8adf7a951ee&X-Amz-SignedHeaders=host)
</p>


Built for FMOD v 2.03(**). 
The aim of UniMod is to make it easier for sound production and game development students to collaborate on projects by decoupling the programming and sound design and music composition aspects of the game audio.

It uses scriptable objects to create sound containers that the developer can add to the scene and create logic for which the sound or music composer can then populate. The Play FMod sound can be used to trigger single fmod events and the Play FMOD multiple sound can be used when more than one sound is needed, such as for a player character. Both scripts integrate the most common FMOD trigger types, including PlayOneshot with parameter. Each script can apply multiple parameters from the FMOD event. 
A benefit with this setup is that the developer doesn’t need to learn FMOD api calls. 

While the intention of this tool is for education, it can be used for any type of game development collaboration














** If you're running an earlier version (FMOD 2.02) you'll get an error because of "RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObjectPosition)" where a game object is not allowed to be used as the argument is set to Transform. Changing this only means that you can't use a different gameobject for position and the Play FMOD script will use the gameobject it's attached to (as is the default setting). 

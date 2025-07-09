<p align="center">
<img width="412" height="412" alt="Image" src="https://github.com/user-attachments/assets/2d75fbcd-658b-4160-80d4-2cf4c4e042bc" />
</p>


Built for FMOD v 2.03(** see below). 

The aim of UniMod is to make it easier for sound production and game development students to collaborate on projects by decoupling the programming and sound design and music composition aspects of the game audio.

It uses scriptable objects to create sound containers that the developer can add to the scene and create logic for which the sound or music composer can then populate. The Play FMod sound can be used to trigger single fmod events and the Play FMOD multiple sound can be used when more than one sound is needed, such as for a player character. Both scripts integrate the most common FMOD trigger types, including PlayOneshot with parameter. Each script can apply multiple parameters from the FMOD event. A benefit with this setup is that the developer doesn’t need to learn FMOD api calls. While the intention of this tool is for education, it can be used for any type of game development collaboration

The below demo shows the basic use of these scripts. See the PDF in the project for more details. 

https://github.com/user-attachments/assets/fc7da060-132f-4194-a1fa-1969e33c03ec



















** If you're running an earlier version (FMOD 2.02) you'll get an error because of "RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObjectPosition)" where a game object is not allowed to be used as the argument is set to Transform. Changing this only means that you can't use a different gameobject for position and the Play FMOD script will use the gameobject it's attached to (as is the default setting). 

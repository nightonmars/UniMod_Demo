Built for FMOD v 2.03(**). 




![UniMod logo](https://github.com/user-attachments/assets/d26c152c-1022-4219-8587-8f3c9afdde52)

















** If you're running an earlier version you'll get an error because of "RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObjectPosition)" where a game object is not allowed to be used as the argument is set to Transform. Changing this only means
that you can't use a different gameobject for position and the Play FMOD script will use the gameobject it's attached to (as is the default setting). 

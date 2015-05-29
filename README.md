SupermanCamera
==============

The SupermanCamera for Unity is two controllers in one. When you're altitude is 
more than 50 meters above the ground the camera floats. You can translate in 
along all three axes with WASDQE and rotate with the mouse when holding down the 
right mouse button. When you are 50 meters or less above the ground gravity 
affects your y-position such that you will walk along the ground with an
eye-height of 1.5 meters. To return to flying mode just take-off by holding down
the E key. Take offs give you a little extra juice to ensure you can escape
gravity.

The SupermanCamera also contains a heads-up-display (HUD) indicating your 
current heading direction, field-of-view, and altitude.

Getting Started
---------------

Add the SupermanCamera prefab to your scene. If the heads up display is desired,
create a Canvas object and add the SupermanCameraHUD as a child of the canvas
and make sure the SupermanCameraHUD component has a reference to your 
SupermanCamera.

Caveats
-------
WASD will affect active UI elements. If a user uses the mouse to change the 
camera's zoom the WASD keys will change the zoom. To disable open your input 
manager and add a NullHorizontal and NullVertical control that map to Joystick 
11 or some other axes you know will not be moved. Then set your EventSystem to
listen to these.


License
-------
This software is licensed under the BSD 3-Clause License

Copyright (c) 2015, Roger Lew (rogerlew@gmail.com)
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this 
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, 
   this list of conditions and the following disclaimer in the documentation 
   and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its contributors 
   may be used to endorse or promote products derived from this software without 
   specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR 
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (
INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


Stick Figure Sprites
--------------------

The stick figure sprites were created especially for this project by Nicholas 
Woods and are licensed under CreativeCommons Attribution-ShareAlike (CC BY-SA).
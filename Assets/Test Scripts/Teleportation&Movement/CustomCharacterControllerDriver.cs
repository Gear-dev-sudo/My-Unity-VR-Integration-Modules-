using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//inspired by https://blog.csdn.net/qq_46044366/article/details/127271639?spm=1001.2101.3001.6661.1&utm_medium=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1-127271639-blog-127226446.pc_relevant_3mothn_strategy_and_data_recovery&depth_1-utm_source=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1-127271639-blog-127226446.pc_relevant_3mothn_strategy_and_data_recovery&utm_relevant_index=1
//ability to update the camera position in each frame, rather than during only position change
public class CustomCharacterControllerDriver : CharacterControllerDriver
{
    void FixedUpdate()
    {
        UpdateCharacterController();
    }
}

﻿using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[RequiresEntityConversion]
public class AddPlayerDataProxy : MonoBehaviour, IConvertGameObjectToEntity
{
    public float playerSpeed = 200.0f;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var mouseInput = new SingletonMouseInput
        {
            RightClickDown = false,
            LeftClickDown = false,
            RightClickUp = false,
            LeftClickUp = false,
            InitialMouseClickPosition = float3.zero,
            CurrentMousePosition = float3.zero,
            InitialMouseRaycastPosition = float3.zero,
            CurrentMouseRaycastPosition = float3.zero
        };
        dstManager.AddComponentData(entity, mouseInput);

        var keyboardInput = new SingletonKeyboardInput
        {
            SpaceBar = false,
            HorizontalMovement = 0.0f,
            VerticalMovement = 0.0f
        };
        dstManager.AddComponentData(entity, keyboardInput);

        var movementSpeed = new MovementSpeed
        {
            Value = playerSpeed
        };
        dstManager.AddComponentData(entity, movementSpeed);

        var copyTransform = new Unity.Transforms.CopyTransformToGameObject { };
        dstManager.AddComponentData(entity, copyTransform);

        var player = new TagPlayer
        {
        };
        dstManager.AddComponentData(entity, player);
    }
}

﻿using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(KeyboardInputSystem))]
public class UnitSpawnSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    struct UnitSpawnJob : IJobForEachWithEntity<SpawnUnit>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition;

        public void Execute(Entity entity, int index, ref SpawnUnit spawner)
        {
            var instance = CommandBuffer.Instantiate(spawner.Prefab);

            CommandBuffer.SetComponent(instance, new Translation { Value = MouseRayPosition });
            CommandBuffer.AddComponent(instance, new Selectable { SelectSize = 25.0f });
            CommandBuffer.AddComponent(instance, new Target { Destination = float3.zero });
            CommandBuffer.AddComponent(instance, new MovementSpeed { Value = 25.0f });
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();
        var keyboard = GetSingleton<SingletonKeyboardInput>();

        if ( keyboard.SpaceBar )
        {
            keyboard.SpaceBar = false;
            SetSingleton<SingletonKeyboardInput>(keyboard);

            var unitJob = new UnitSpawnJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                MouseRayPosition = mouse.CurrentMouseRaycastPosition
            }.ScheduleSingle(this, inputDeps);

            m_EntityCommandBufferSystem.AddJobHandleForProducer(unitJob);

            return unitJob;
        }

        return inputDeps;
    }
}
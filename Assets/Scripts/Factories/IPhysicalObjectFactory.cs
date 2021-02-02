using System;

public interface IPhysicalObjectFactory
{
    event Action<IPhysicalObject> onBlackPuckCreated;
    IPhysicalObject Create(PhysicalObjectFactory.ObjectType type);
}
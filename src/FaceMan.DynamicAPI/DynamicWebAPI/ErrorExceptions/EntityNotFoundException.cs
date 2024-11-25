using System;

namespace FaceMan.DynamicWebAPI.ErrorExceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; }
        public object EntityId { get; }

        public EntityNotFoundException(Type entityType, object entityId)
            : base($"Entity of type '{entityType.Name}' with ID '{entityId}' was not found.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}

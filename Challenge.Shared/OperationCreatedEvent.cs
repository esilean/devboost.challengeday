using Challenge.Shared.Enum;
using System;

namespace Challenge.Shared
{
    public class OperationCreatedEvent
    {
        public Guid Id { get; private set; }
        public double Value { get; private set; }
        public OperationType OperationType { get; private set; }
        public DateTime Created { get; set; }

        public OperationCreatedEvent(double value, OperationType operationType)
        {
            Id = Guid.NewGuid();
            Value = value;
            OperationType = operationType;
            Created = DateTime.Now;
        }
    }
}

using Challenge.Shared.Enum;
using System;

namespace Challenge.Domain
{
    public class Operation
    {
        public Guid Id { get; private set; }
        public double Value { get; private set; }
        public OperationType OperationType { get; private set; }
        public DateTime Created { get; set; }

        public Operation(Guid id, double value, OperationType operationType, DateTime created)
        {
            Id = id;
            Value = value;
            OperationType = operationType;
            Created = created;
        }
    }
}

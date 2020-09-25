using Challenge.Shared.Enum;
using System;

namespace Challenge.Application.Dto
{
    public class OperationDto
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public OperationType OperationType { get; set; }
        public DateTime Created { get; set; }
    }
}

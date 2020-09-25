using Challenge.Shared.Enum;

namespace Challenge.Producer.Api.Dto
{
    public class OperationDto
    {
        public double Value { get; set; }
        public OperationType OperationType { get; set;  }
    }
}

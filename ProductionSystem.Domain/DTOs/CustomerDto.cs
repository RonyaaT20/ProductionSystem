namespace ProductionSystem.Domain.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class CreateCustomerDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class EditCustomerDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
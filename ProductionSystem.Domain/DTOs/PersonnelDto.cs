namespace ProductionSystem.Domain.DTOs
{
    public class PersonnelDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
    }

    public class CreatePersonnelDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
    }

    public class EditPersonnelDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
    }
}
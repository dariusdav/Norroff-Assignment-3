namespace Assignment_3.Models.DTO.Franchise
{
    /// <summary>
    /// Data Transfer Object used for Franchise Get, Update And Delete Calls
    /// </summary>
    public class FranchiseBaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }
    }
}

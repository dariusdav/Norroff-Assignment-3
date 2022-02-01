namespace Assignment_3.models.DTO.Character
{
    /// <summary>
    /// DataTransferObject used for creating, editing and reading the Character object.
    /// </summary>
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Alias { get; set; }

        public string Gender { get; set; }

        public string Picture { get; set; }
    }
}

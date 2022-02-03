namespace Assignment_3.Models.DTO.Character
{
    /// <summary>
    /// DTO class without ID for creating a character since the ID is assigned automatically.
    /// </summary>
    public class CharacterCreateDTO
    {
        public string Name { get; set; }

        public string Alias { get; set; }

        public string Gender { get; set; }

        public string Picture { get; set; }
    }
}

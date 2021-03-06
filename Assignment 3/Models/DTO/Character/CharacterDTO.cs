namespace Assignment_3.models.DTO.Character
{
    /// <summary>
    /// DataTransferObject used for updating  and interacting with the Character object.
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

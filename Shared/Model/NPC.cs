namespace Kobold.Shared.Model;

public class NPC
{
    public NPC(string firstName, string lastName, string race)
    {
        FirstName = firstName;
        LastName = lastName;
        Race = race;
    }

    public uint Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Race { get; set; }
    public string? Profession { get; set; }
    public string? Alignment { get; set; }
    public uint ArmorClass { get; set; }
    public uint HitPoints { get; set; }
    public uint Strength { get; set; }
    public uint Dexterity { get; set; }
    public uint Constitution { get; set; }
    public uint Intelligence { get; set; }
    public uint Wisdom { get; set; }
    public uint Charisma { get; set; }
    public uint Speed { get; set; }
    public string? CharacterTraits { get; set; }
    public string? Appearance { get; set; }
    public string? Backstory { get; set; }
    public string? Notes { get; set; }
    
}
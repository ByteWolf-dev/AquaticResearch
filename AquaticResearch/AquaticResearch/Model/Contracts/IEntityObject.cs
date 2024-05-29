namespace AquaticResearch.Model.Contracts;

/// <typeparam name="T"></typeparam>
public interface IEntityObject
{
    /// <summary>
    /// Eindeutige Identitaet des Objektes.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Die Version dieses Datenbank-Objektes. Diese Version wird beim Erzeugen (Insert) 
    /// automatisch und mit jeder Änderung neu gesetzt. 
    /// </summary>
    byte[]? RowVersion { get; set; }
}
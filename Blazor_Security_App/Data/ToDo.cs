using EntityFrameworkCore.EncryptColumn.Attribute;

namespace Blazor_Security_App.Data
{
    public class ToDo
    {
        public int Id { get; set; }
        [EncryptColumn]public string Name { get; set; }
        [EncryptColumn] public string Description { get; set; }
        [EncryptColumn] public string UserId { get; set; }
    }
}

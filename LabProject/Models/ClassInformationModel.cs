/* 
create a class named ClassInformationModel.cs.
• This class will store the following properties: Id (auto-incremented) ClassName StudentCount Description
The Id property will be automatically incremented each time a new item is added to the list. 
The list will act like a simple in-memory database. 
ClassInformationModel.cs dosyasında non nulable hatası veriyor
*/
namespace LabProject.Models
{
    public class ClassInformationModel
    {
        public int Id { get; set; } // Auto-incremented ID
        public string ClassName { get; set; } = string.Empty; // Default empty string
        public int StudentCount { get; set; } = 0; // Default 0
        public string Description { get; set; } = string.Empty; // Default empty string
    }
}

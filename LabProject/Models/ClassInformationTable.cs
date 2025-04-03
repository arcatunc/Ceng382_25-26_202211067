namespace LabProject.Models
{
    public class ClassInformationTable
    {
        public string ClassName { get; set; } // Name of the class
        public int StudentCount { get; set; } // Number of students in the class
        public string Description { get; set; } // Description of the class

        // Hidden property for actions like edit, delete, or details
        public int Id { get; set; }
    }
}

/* created a new model class called ClassInformationTable. This model will store
the filtered version of your main model and will be used to display data in the table. In this
model, the ID should not be shown in the table, but the ID will still be used in the
background for actions like edit, delete, or details.
*/
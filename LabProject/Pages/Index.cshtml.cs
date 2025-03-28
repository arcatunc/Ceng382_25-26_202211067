using LabProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace LabProject.Pages
{
    public class IndexModel : PageModel
    {
         public static List<ClassInformationModel> Classes { get; set; } = new List<ClassInformationModel>();

    [BindProperty]
    public ClassInformationModel NewClass { get; set; }

    public IndexModel()
    {
        // Initialize NewClass to avoid the CS8618 error
        NewClass = new ClassInformationModel();
    }

        // OnGet method to initialize the page
        public void OnGet()
        {
            // Optional: Pre-fill with some initial data if desired
            if (!Classes.Any())
            {
                Classes.Add(new ClassInformationModel { Id = 1, ClassName = "Math", StudentCount = 30, Description = "Basic Math" });
                Classes.Add(new ClassInformationModel { Id = 2, ClassName = "Science", StudentCount = 25, Description = "Intro to Science" });
            }
        }

        // OnPostAdd to handle form submission for adding a new class
        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Return to the same page if form is invalid
            }

            // Automatically assign an Id based on the count of existing classes
            NewClass.Id = Classes.Count + 1;
            Classes.Add(NewClass);

            NewClass = new ClassInformationModel(); // Reset the form

            return RedirectToPage(); // Refresh the page to display updated table
        }

        // OnPostDelete to handle deletion of a class by Id
        public IActionResult OnPostDelete(int id)
        {
            var classToDelete = Classes.FirstOrDefault(c => c.Id == id);
            if (classToDelete != null)
            {
                Classes.Remove(classToDelete);
            }

            return RedirectToPage(); // Refresh the page to display updated table
        }

        // OnPostEdit to handle editing of a class by Id
        public IActionResult OnPostEdit(int id)
        {
            var classToEdit = Classes.FirstOrDefault(c => c.Id == id);
            if (classToEdit != null)
            {
                NewClass = classToEdit;
            }

            return Page(); // Stay on the same page with the form pre-filled for editing
        }

        // OnPostSave to save the edited class data
        public IActionResult OnPostSave()
        {
            var classToUpdate = Classes.FirstOrDefault(c => c.Id == NewClass.Id);
            if (classToUpdate != null)
            {
                classToUpdate.ClassName = NewClass.ClassName;
                classToUpdate.StudentCount = NewClass.StudentCount;
                classToUpdate.Description = NewClass.Description;
            }

            return RedirectToPage(); // Refresh the page after saving
        }
    }
}
/* On the left side of the page, there will be a form that collects: Class Name Student Count Description On the right side, there will be a table that displays all the submitted class data. 
The table will have the following columns: Id Class Name Student Count Description Actions (Edit and Delete)
The data should be validated and added to a static list each time the form is submitted. 
The data will then be displayed in the table.

Step 4 – Requirements and Constraints
• Use Bootstrap to create a responsive layout with two columns (form on the left, table on the
right).
• Use Razor Pages only; no JavaScript is allowed.
• All operations (Add, Edit, Delete) must be handled using C# methods in the PageModel.
• Form validation should be done using C# attributes like [Required], [Range], etc.
• When editing, pre-fill the form with the selected item's data.
• After deletion or editing, refresh the page and update the table accordingly.
Index.cshtml.cs dosyasında 
Non-nullable property 'NewClass' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the property as nullable.CS8618 bu hatayın veriyor

 */
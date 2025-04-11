using LabProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LabProject.Pages
{
    public class IndexModel : PageModel
    {
        // Static list to act as an in-memory database
        public static List<ClassInformationModel> Classes { get; set; } = new List<ClassInformationModel>();

        [BindProperty]
        public ClassInformationModel NewClass { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; } // Filter string for ClassName

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1; // Current page number

        public int PageSize { get; set; } = 10; // Number of items per page
        public int TotalPages { get; set; } // Total number of pages

        public List<ClassInformationModel> FilteredClasses { get; set; } // Filtered and paginated list

        public IndexModel()
        {
            // Initialize NewClass to avoid the CS8618 error
            NewClass = new ClassInformationModel();
            Filter = string.Empty; // Initialize Filter to an empty string
            FilteredClasses = new List<ClassInformationModel>(); // Initialize FilteredClasses to an empty list
        }

        // OnGet method to initialize the page
        public void OnGet(string filter = "", int pageNumber = 1)
        {
            Filter = filter;
            PageNumber = pageNumber;

            // Generate synthetic data if the list is empty
            if (!Classes.Any())
            {
                GenerateSyntheticData();
            }

            // Filter the list based on the Filter string
            var query = Classes.AsQueryable();
            if (!string.IsNullOrEmpty(Filter))
            {
                query = query.Where(c => c.ClassName.Contains(Filter, System.StringComparison.OrdinalIgnoreCase));
            }

            // Calculate total pages
            int totalItems = query.Count();
            TotalPages = (int)System.Math.Ceiling(totalItems / (double)PageSize);

            // Apply pagination
            FilteredClasses = query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
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

        // OnPostExport to export selected classes to a JSON file
        public IActionResult OnPostExport(List<int> SelectedClasses)
        {
            // If no classes are selected, export all classes on the current page
            var classesToExport = SelectedClasses?.Any() == true
                ? FilteredClasses.Where(c => SelectedClasses.Contains(c.Id)).ToList()
                : FilteredClasses;

            // Use Utils to serialize the data to JSON
            var json = LabProject.Helpers.Utils.Instance.ExportToJson(classesToExport);

            var fileName = "exported_classes.json";
            return File(System.Text.Encoding.UTF8.GetBytes(json), "application/json", fileName);
        }

        public IActionResult OnPostExport(string ExportMode, List<string> SelectedColumns)
        {
            // Determine the data to export based on the mode
            var dataToExport = ExportMode == "Filtered" ? FilteredClasses : Classes;

            // If no columns are selected, export all columns
            if (SelectedColumns == null || !SelectedColumns.Any())
            {
                var json = Utils.Instance.ExportToJson(dataToExport);
                return File(System.Text.Encoding.UTF8.GetBytes(json), "application/json", "exported_classes.json");
            }

            // Filter the data to include only the selected columns
            var filteredData = dataToExport.Select(item =>
            {
                var result = new Dictionary<string, object>();
                if (SelectedColumns.Contains("ClassName")) result["ClassName"] = item.ClassName;
                if (SelectedColumns.Contains("StudentCount")) result["StudentCount"] = item.StudentCount;
                if (SelectedColumns.Contains("Description")) result["Description"] = item.Description;
                return result;
            }).ToList();

            // Export the filtered data
            var filteredJson = Utils.Instance.ExportToJson(filteredData);
            return File(System.Text.Encoding.UTF8.GetBytes(filteredJson), "application/json", "exported_filtered_classes.json");
        }

        // Method to generate synthetic data
        private void GenerateSyntheticData()
        {
            for (int i = 1; i <= 100; i++)
            {
                Classes.Add(new ClassInformationModel
                {
                    Id = i,
                    ClassName = $"Class {i}",
                    StudentCount = i % 30 + 1, // Randomize student count between 1 and 30
                    Description = $"Description for Class {i}"
                });
            }
        }
    }
}

/* you will improve the table you created last week in your Razor Pages project. You
will add filtering and pagination features. However, filtering will be done on the data list in
the backend, not on the frontend.
The filtering logic must be written inside the OnGet methods 

In addition to filtering, you are required to implement pagination. To properly test the
pagination feature, you need to generate synthetic data. Make sure to create a list with at
least 100 sample records so you can see how the pagination works across multiple pages.
Tip :
When a filter value changes, the form should submit automatically or the user should click a
"Filter" button. This will trigger the OnGet method with the selected filter values passed as
query parameters.*/
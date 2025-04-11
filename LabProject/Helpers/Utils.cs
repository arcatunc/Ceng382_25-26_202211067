using System.Text.Json;

namespace LabProject.Helpers
{
    /// <summary>
    /// Utility class for exporting data to JSON.
    /// </summary>
    public class Utils
    {
        // Singleton instance
        private static readonly Utils _instance = new Utils();

        // Private constructor to prevent instantiation
        private Utils() { }

        /// <summary>
        /// Gets the singleton instance of the Utils class.
        /// </summary>
        public static Utils Instance => _instance;

        /// <summary>
        /// Exports a list of objects to a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="data">The list of objects to export.</param>
        /// <returns>A JSON string representing the data.</returns>
        public string ExportToJson<T>(List<T> data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true // Pretty-print the JSON for readability
            });
        }
    }
}
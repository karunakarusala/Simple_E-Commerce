using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace E_Commerce.categories
{
    // Represents a subcategory (brand, size, color, design pattern)
    class Subcategory
    {
        [JsonPropertyName("brand")]
        public List<Brand> Brands { get; set; }

        [JsonPropertyName("sizes")]
        public List<Size> Sizes { get; set; }

        [JsonPropertyName("color")]
        public List<Color> Colors { get; set; }

        [JsonPropertyName("Design_pattern")]
        public List<DesignPattern> DesignPatterns { get; set; }
    }

    class Brand
    {
        [JsonPropertyName("brandName")]
        public string BrandName { get; set; }
    }

    class Size
    {
        [JsonPropertyName("clothSize")]
        public string ClothSize { get; set; }
    }

    class Color
    {
        [JsonPropertyName("color")]
        public string ClothColor { get; set; }
    }

    class DesignPattern
    {
        [JsonPropertyName("design-pattern")]
        public string Pattern { get; set; }
    }

    // Represents a category
    class Category
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("subcategories")]
        public Subcategory Subcategories { get; set; }
    }

    public class Clothing
    {
        private static string filePath = @"JsonFiles\categories.json";

        public static void DisplayCategories()
        {
            // Check if file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: categories.json file not found.");
                return;
            }

            string json = File.ReadAllText(filePath);

            List<Category> categories = JsonSerializer.Deserialize<List<Category>>(json);

            if (categories == null || categories.Count == 0)
            {
                Console.WriteLine("Error: No categories found or JSON format is incorrect.");
                return;
            }

            Console.WriteLine("\nSelect category:");

            int index = 1;
            foreach (var category in categories)
            {
                Console.WriteLine($" {index}. {category.Name}");
                index++;
            }

            Console.Write("\nEnter category number: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= categories.Count)
            {
                Category selectedCategory = categories[choice - 1];
                Console.WriteLine($"\nYou selected: {selectedCategory.Name}");
                DisplaySubcategories(selectedCategory);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a valid category number.");
            }
        }

        private static void DisplaySubcategories(Category category)
        {
            if (category.Subcategories == null)
            {
                Console.WriteLine("No subcategories available for this category.");
                return;
            }

            Console.WriteLine("\nAvailable Brands:");
            foreach (var brand in category.Subcategories.Brands)
            {
                Console.WriteLine($" - {brand.BrandName}");
            }

            Console.WriteLine("\nAvailable Sizes:");
            foreach (var size in category.Subcategories.Sizes)
            {
                Console.WriteLine($" - {size.ClothSize}");
            }

            Console.WriteLine("\nAvailable Colors:");
            foreach (var color in category.Subcategories.Colors)
            {
                Console.WriteLine($" - {color.ClothColor}");
            }

            Console.WriteLine("\nAvailable Design Patterns:");
            foreach (var pattern in category.Subcategories.DesignPatterns)
            {
                Console.WriteLine($" - {pattern.Pattern}");
            }
        }
    }
}

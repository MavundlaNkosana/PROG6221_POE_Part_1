using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CyberKnight
{
    public class CyberTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? ReminderDate { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "✅ Completed" : "🕒 Pending";
            string reminder = ReminderDate.HasValue ? $"🔔 Reminder: {ReminderDate.Value.ToShortDateString()}" : "No Reminder";
            return $"{Title}\n  Description: {Description}\n  Status: {status}\n  {reminder}";
        }
    }

    public static class TaskManager
    {
        private static List<CyberTask> tasks = new List<CyberTask>();
        private static CyberTask lastAddedTask = null;

        public static void AddTask(string title, string description)
        {
            var task = new CyberTask
            {
                Title = title,
                Description = description,
                IsCompleted = false,
                ReminderDate = null
            };

            tasks.Add(task);
            lastAddedTask = task;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Task added: {title}");
            Console.ResetColor();
            CyberKnight.LogActivity($"Task added: '{title}'");

        }

        public static void StartTaskCreation()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();

            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            Console.Write("Would you like a reminder? (yes/no): ");
            string remindInput = Console.ReadLine().ToLower();

            string reminderInfo = "";

            if (remindInput == "yes")
            {
                Console.Write("When should I remind you? (e.g., in 3 days, on 2025-07-01): ");
                reminderInfo = Console.ReadLine();
            }

            AddTask(title, description);

            if (!string.IsNullOrWhiteSpace(reminderInfo))
            {
                SetReminder($"remind me {reminderInfo} to {title}");
            }

            Console.WriteLine("Task creation complete!\n");
        }


        public static void SetReminder(string reminderInput)
        {
            if (lastAddedTask == null)
            {
                Console.WriteLine("No task to set reminder for.");
                return;
            }

            if (TryParseReminder(reminderInput, out DateTime remindAt))
            {
                lastAddedTask.ReminderDate = remindAt;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Got it! I’ll remind you on {remindAt.ToShortDateString()}.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Could not understand reminder time.");
            }
            CyberKnight.LogActivity($"Reminder set for task: '{lastAddedTask?.Title}' on {remindAt.ToShortDateString()}");

        }

        public static void ViewTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("You have no tasks.");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"\nTask {i + 1}:");
                Console.WriteLine(tasks[i].ToString());
            }
        }

        public static void DeleteTask(int index)
        {
            if (index < 1 || index > tasks.Count)
            {
                Console.WriteLine("Invalid task number.");
                return;
            }

            tasks.RemoveAt(index - 1);
            Console.WriteLine($"Task {index} removed.");
        }

        public static void CompleteTask(int index)
        {
            if (index < 1 || index > tasks.Count)
            {
                Console.WriteLine("Invalid task number.");
                return;
            }

            tasks[index - 1].IsCompleted = true;
            Console.WriteLine($"Task {index} marked as completed.");
        }

        public static bool TryParseReminder(string input, out DateTime remindAt)
        {
            remindAt = DateTime.Now;
            input = input.ToLower().Trim();

            if (input.StartsWith("in "))
            {
                string[] words = input.Split(' ');
                if (words.Length >= 3 && int.TryParse(words[1], out int number))
                {
                    if (words[2].StartsWith("day"))
                    {
                        remindAt = DateTime.Now.AddDays(number);
                        return true;
                    }
                    if (words[2].StartsWith("week"))
                    {
                        remindAt = DateTime.Now.AddDays(number * 7);
                        return true;
                    }
                }
            }
            else if (input.StartsWith("on "))
            {
                string datePart = input.Substring(3).Trim();
                if (DateTime.TryParse(datePart, out DateTime parsedDate))
                {
                    remindAt = parsedDate;
                    return true;
                }
            }
            else if (input == "tomorrow")
            {
                remindAt = DateTime.Now.AddDays(1);
                return true;
            }
            else if (input == "next week")
            {
                remindAt = DateTime.Now.AddDays(7);
                return true;
            }
            else if (DateTime.TryParse(input, out DateTime genericDate))
            {
                remindAt = genericDate;
                return true;
            }

            return false;
        }

    }
}

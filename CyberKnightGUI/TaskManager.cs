using System;
using System.Collections.Generic;

namespace CyberKnightGUI
{
    public class CyberTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? ReminderDate { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "Completed" : "Pending";
            string reminder = ReminderDate.HasValue ? $"{ReminderDate.Value.ToShortDateString()}" : "No Reminder";
            return $"{Title} - {Description} | {status} | {reminder}";
        }
    }

    public static class TaskManager
    {
        private static List<CyberTask> tasks = new List<CyberTask>();
        private static CyberTask lastAddedTask = null;

        public static Action<string> LogActivityAction;

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
            Log($"Task added: '{title}'");
        }

        public static List<CyberTask> GetAllTasks()
        {
            return new List<CyberTask>(tasks);
        }

        public static bool CompleteTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].IsCompleted = true;
                Log($"Task '{tasks[index].Title}' marked as complete.");
                return true;
            }
            return false;
        }

        public static bool DeleteTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                string title = tasks[index].Title;
                tasks.RemoveAt(index);
                Log($"Task '{title}' deleted.");
                return true;
            }
            return false;
        }

        public static void SetReminder(string reminderInput)
        {
            if (lastAddedTask == null)
            {
                Log("No task to set a reminder for.");
                return;
            }

            if (TryParseReminder(reminderInput, out DateTime remindAt))
            {
                lastAddedTask.ReminderDate = remindAt;
                Log($"Reminder set for '{lastAddedTask.Title}' on {remindAt.ToShortDateString()}");
            }
            else
            {
                Log("Could not understand the reminder date.");
            }
        }

        public static List<CyberTask> GetTasks()
        {
            return new List<CyberTask>(tasks);
        }


        public static List<CyberTask> GetDueReminders()
        {
            DateTime today = DateTime.Today;
            return tasks.FindAll(task =>
                task.ReminderDate.HasValue &&
                task.ReminderDate.Value.Date <= today &&
                !task.IsCompleted);
        }


        private static void Log(string message)
        {
            LogActivityAction?.Invoke(message);
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
                    if (words[2].StartsWith("day")) remindAt = DateTime.Now.AddDays(number);
                    else if (words[2].StartsWith("week")) remindAt = DateTime.Now.AddDays(number * 7);
                    else return false;

                    return true;
                }
            }
            else if (input.StartsWith("on "))
            {
                if (DateTime.TryParse(input.Substring(3).Trim(), out DateTime parsedDate))
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

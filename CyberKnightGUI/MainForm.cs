using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace CyberKnightGUI
{
    public partial class MainForm : Form
    {
        private bool isNameSet = false;
        private int selectedTaskIndex = -1;

        public MainForm()
        {
            InitializeComponent();
            ShowDueReminders();

            // Initial greeting in lstOutput
            lstOutput.Items.Clear();
            lstOutput.Items.Add("Welcome to CyberKnight - Your trusted cybersecurity awareness assistant!");
            lstOutput.Items.Add("Before we begin, what should I call you?");
        }

        private void RefreshTaskList()
        {
            lstTasks.Items.Clear();
            var tasks = TaskManager.GetTasks();
            for (int i = 0; i < tasks.Count; i++)
            {
                var t = tasks[i];
                string reminderDisplay = t.ReminderDate.HasValue ? t.ReminderDate.Value.ToString("dd-MM-yyyy HH:mm") : "No Reminder";
                string status = t.IsCompleted ? "Completed" : "Pending";
                lstTasks.Items.Add($"{t.Title} - {t.Description} | {status} | Reminder: {reminderDisplay}");
            }
        }

        private bool TryGetFeatureHelp(string userInput, out List<string> helpMessages)
        {
            helpMessages = new List<string>();

            userInput = userInput.ToLower();

            if (userInput.Contains("how to play quiz") || userInput.Contains("how do i play quiz") || userInput.Contains("quiz instructions"))
            {
                helpMessages.Add("To play the quiz, simply press the 'Start Quiz' button on the main screen.");
                helpMessages.Add("You'll be given multiple-choice questions about cybersecurity.");
                helpMessages.Add("Select the answer you think is correct and get immediate feedback.");
                return true;
            }
            else if (userInput.Contains("how to add task") || userInput.Contains("how do i add task") || userInput.Contains("adding tasks"))
            {
                helpMessages.Add("To add a task:");
                helpMessages.Add("1. Enter the Task Title in the 'Task Title' box.");
                helpMessages.Add("2. Enter the Task Description in the 'Task Description' box.");
                helpMessages.Add("3. Click the 'Add Task' button.");
                helpMessages.Add("You can set reminders for tasks after adding them.");
                return true;
            }
            else if (userInput.Contains("how to complete task") || userInput.Contains("complete tasks") || userInput.Contains("mark task complete"))
            {
                helpMessages.Add("To complete a task:");
                helpMessages.Add("1. Select a task from the task list.");
                helpMessages.Add("2. Click the 'Complete Task' button.");
                helpMessages.Add("The task will be marked as completed.");
                return true;
            }
            else if (userInput.Contains("how to delete task") || userInput.Contains("delete tasks") || userInput.Contains("remove task"))
            {
                helpMessages.Add("To delete a task:");
                helpMessages.Add("1. Select a task from the task list.");
                helpMessages.Add("2. Click the 'Delete Task' button.");
                helpMessages.Add("3. Confirm the deletion in the popup dialog.");
                return true;
            }
            else if (userInput.Contains("how to create reminder") || userInput.Contains("how to set reminder") || userInput.Contains("create reminders") || userInput.Contains("set reminders"))
            {
                helpMessages.Add("To create reminders for tasks:");
                helpMessages.Add("1. First, add the task by filling in the Task Title and Task Description.");
                helpMessages.Add("2. After adding, open the task details or select the task from your list.");
                helpMessages.Add("3. Look for the option to set a reminder date and time.");
                helpMessages.Add("4. Save the reminder so you get notified when the task is due.");
                return true;
            }

            return false;
        }

        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTaskIndex = lstTasks.SelectedIndex;
        }


        private void btnAsk_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();
            lstOutput.Items.Clear();

            if (selectedTaskIndex >= 0)
            {
                // Try to parse reminder for selected task
                if (TryParseReminder(userInput, out DateTime remindAt))
                {
                    var tasks = TaskManager.GetTasks();
                    var task = tasks[selectedTaskIndex];
                    task.ReminderDate = remindAt;
                    TaskManager.LogActivityAction?.Invoke($"Reminder set for '{task.Title}' on {remindAt:dd-MM-yyyy HH:mm}");
                    MessageBox.Show($"Reminder set for task '{task.Title}' on {remindAt:dd-MM-yyyy HH:mm}");
                    RefreshTaskList();
                    txtUserInput.Clear();
                    return;
                }
                else
                {
                    lstOutput.Items.Add("Reminder format invalid. Please use dd-MM-yyyy HH:mm.");
                    txtUserInput.Clear();
                    return;
                }
            }

            if (!isNameSet)
            {
                if (userInput.ToLower().StartsWith("my name is "))
                {
                    string name = userInput.Substring("my name is ".Length).Trim();
                    CyberKnightLogic.Remember("name", name);
                    lstOutput.Items.Add($"Nice to meet you, {name}! What would you like to know about? If you're not sure, press the 'help' button.");
                    CyberKnightLogic.LogActivity($"User introduced themselves as {name}");
                    isNameSet = true;
                    txtUserInput.Clear();
                    return;
                }

                CyberKnightLogic.Remember("name", userInput);
                lstOutput.Items.Add($"Nice to meet you, {userInput}! What would you like to know about? If you're not sure, press the 'help' button.");
                CyberKnightLogic.LogActivity($"User introduced themselves as {userInput}");
                isNameSet = true;
                txtUserInput.Clear();
                return;
            }

            if (string.IsNullOrWhiteSpace(userInput))
            {
                lstOutput.Items.Add("Please type a question or keyword.");
                return;
            }

            // Check for feature help requests first
            if (TryGetFeatureHelp(userInput, out List<string> helpMessages))
            {
                foreach (var msg in helpMessages)
                {
                    lstOutput.Items.Add(msg);
                }
                txtUserInput.Clear();
                return;
            }

            // Check if the user is reintroducing their name
            if (userInput.ToLower().StartsWith("my name is "))
            {
                string name = userInput.Substring("my name is ".Length).Trim();
                CyberKnightLogic.Remember("name", name);
                lstOutput.Items.Add($"Nice to meet you, {name}!");
                CyberKnightLogic.LogActivity($"User reintroduced themselves as {name}");
                txtUserInput.Clear();
                return;
            }

            // Recall name request
            if (userInput.Contains("what's my name") || userInput.Contains("do you remember my name"))
            {
                var name = CyberKnightLogic.Recall("name");
                if (name != null)
                    lstOutput.Items.Add($"Of course! You're {name}.");
                else
                    lstOutput.Items.Add("Hmm... I don't think you've told me your name yet.");
                txtUserInput.Clear();
                return;
            }

            // Topic matching and response
            string topic = CyberKnightLogic.MatchKeywordToTopic(userInput);

            if (!string.IsNullOrEmpty(topic))
            {
                string response = CyberKnightLogic.GetRandomResponse(topic);
                lstOutput.Items.Add($"Topic: {topic}");
                lstOutput.Items.Add("CyberKnight: Got it!");
                lstOutput.Items.Add($"Response: {response}");

                string followUp = CyberKnightLogic.GetFollowUpSuggestion();
                if (!string.IsNullOrWhiteSpace(followUp))
                    lstOutput.Items.Add($"Suggestion: {followUp}");

                CyberKnightLogic.LogActivity($"User asked about {topic}");
            }
            else
            {
                string sentiment = CyberKnightLogic.DetectSentiment(userInput);
                string sentimentResponse = CyberKnightLogic.GetSentimentResponse(sentiment);
                lstOutput.Items.Add($"Detected Sentiment: {sentiment}");
                lstOutput.Items.Add($"Response: {sentimentResponse}");
                CyberKnightLogic.LogActivity($"Sentiment response: {sentiment}");
            }

            RefreshTaskList();
            txtUserInput.Clear();
        }

        private void ShowDueReminders()
        {
            var dueTasks = TaskManager.GetDueReminders();
            lstOutput.Items.Clear();

            if (dueTasks.Count == 0)
            {
                lstOutput.Items.Add("No reminders due today!");
            }
            else
            {
                lstOutput.Items.Add("Tasks Due Today:");
                foreach (var task in dueTasks)
                {
                    lstOutput.Items.Add(task.ToString());
                }
            }
        }

        private void btnCheckReminders_Click(object sender, EventArgs e)
        {
            ShowDueReminders();
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            string title = txtTaskTitle.Text.Trim();
            string description = txtTaskDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            TaskManager.AddTask(title, description);
            MessageBox.Show("Task added successfully!");

            txtTaskTitle.Clear();
            txtTaskDescription.Clear();
            lstTasks.Items.Add($"{title} - {description}");
        }

        private void btnViewTasks_Click(object sender, EventArgs e)
        {
            lstOutput.Items.Clear();
            foreach (var task in TaskManager.GetTasks())
            {
                lstOutput.Items.Add(task.ToString());
            }
        }

        private void btnCompleteTask_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstTasks.SelectedIndex;
            if (selectedIndex >= 0)
            {
                bool success = TaskManager.CompleteTask(selectedIndex);
                if (success)
                {
                    MessageBox.Show("Task marked as completed!");
                    RefreshTaskList();
                    CyberKnightLogic.LogActivity("User completed a task");
                }
            }
            else
            {
                MessageBox.Show("Please select a task to complete.");
            }
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstTasks.SelectedIndex;
            if (selectedIndex >= 0)
            {
                var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    bool success = TaskManager.DeleteTask(selectedIndex);
                    if (success)
                    {
                        MessageBox.Show("Task deleted.");
                        RefreshTaskList();
                        CyberKnightLogic.LogActivity("User deleted a task");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }

       

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            lstOutput.Items.Clear();
            var log = CyberKnightLogic.GetActivityLog();
            if (log.Count == 0)
                lstOutput.Items.Add("No recent actions recorded.");
            else
                foreach (var entry in log)
                    lstOutput.Items.Add(entry);
        }

        private void btnShowHelp_Click(object sender, EventArgs e)
        {
            lstOutput.Items.Clear();
            foreach (string line in CyberKnightLogic.GetHelp())
            {
                lstOutput.Items.Add(line);
            }
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            var quizForm = new QuizForm();
            quizForm.ShowDialog(); // modal
        }

        private bool TryParseReminder(string input, out DateTime remindAt)
        {
            remindAt = DateTime.MinValue;
            return DateTime.TryParseExact(input, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out remindAt);
        }

        private void txtTaskDescription_TextChanged(object sender, EventArgs e)
        {
            // Optional: your logic here if needed
        }
    }
}

using System;
using System.Media;
using System.Threading;
using System.Collections.Generic;
using CyberKnight;


namespace CyberKnight
{
    class CyberKnight
    {
        // Memory storage for user preferences
        private static Dictionary<string, string> userMemory = new Dictionary<string, string>();

       
        
        // Response banks for varied answers
        private static Dictionary<string, List<string>> responseBank = new Dictionary<string, List<string>>()
        {
            {"password", new List<string>{
                "Make sure to use strong, unique passwords for each account. Avoid using personal details!",
                "A good password should be at least 12 characters long with a mix of letters, numbers and symbols.",
                "Consider using a passphrase like 'PurpleTiger$JumpsHigh' instead of simple passwords."
            }},
            {"phishing", new List<string>{
                "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.",
                "Check the sender's email address carefully - phishing emails often use similar-looking addresses.",
                "Never click on suspicious links. Hover over them first to see the actual URL destination."
            }},
            {"privacy", new List<string>{
                "Review your social media privacy settings regularly - they often change without notice.",
                "Be careful what personal info you share online - even innocent details can be used against you.",
                "Consider using a VPN when on public Wi-Fi to protect your browsing activity."
            }},
            {"malware", new List<string>{
                "Always keep your antivirus software updated to protect against the latest malware threats.",
                "Be extremely careful with email attachments, even from people you know.",
                "Regularly back up your important files to protect against ransomware attacks."
            }},
            {"browsing", new List<string>{
                "Look for HTTPS and the padlock icon to ensure secure connections.",
                "Use private browsing mode when accessing sensitive information on shared computers.",
                "Clear your browser cache regularly to remove tracking cookies."
            }},
            {"engineering", new List<string>{
                "- Be skeptical of urgent requests for information",
                "- Verify identities before sharing sensitive info",
                "- Don't trust caller ID because it can be spoofed",
                "- Be cautious of 'too good to be true' offers",
                "- Educate yourself about common tactics",
            }}
        };

        //Activity to track key actions
        public static List<string> activityLog = new List<string>();
        public const int maxLogEntries = 10;

        public static void LogActivity(string message) {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            activityLog.Add($"[{timestamp}]{message}");

            if (activityLog.Count > maxLogEntries) {
                activityLog.RemoveAt(0);
            }
        }

        static void Main(string[] args)
        {
            // Attempt to play a welcome sound
            try
            {
                SoundPlayer player = new SoundPlayer("media\\welcome.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to play welcome message: " + ex.Message);
            }

            // Display a stylized ASCII art banner
            DisplayAsciiArt();

            // Start user interaction by greeting and requesting their name
            GreetUser();
        }

        // Displays ASCII logo/banner in blue color
        static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"


   ______      __              __ __       _       __    __ 
  / ____/_  __/ /_  ___  _____/ //_/____  (_)___ _/ /_  / /_
 / /   / / / / __ \/ _ \/ ___/ ,<  / __ \/ / __ `/ __ \/ __/
/ /___/ /_/ / /_/ /  __/ /  / /| |/ / / / / /_/ / / / / /_  
\____/\__, /_.___/\___/_/  /_/ |_/_/ /_/_/\__, /_/ /_/\__/  
     /____/                              /____/             

Cybersecurity Awareness Assistant

");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Greets the user, prompts for their name, and starts conversation
        static void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("__________________________________________");
            Console.WriteLine("|  Welcome to CyberKnight - Your trusted |");
            Console.WriteLine("|  cybersecurity awareness assistant!    |");
            Console.WriteLine("|________________________________________|");
            Console.ResetColor();
            Console.WriteLine();

            // Ask for user name
            Console.Write("Before we begin, what should I call you? ");
            Console.ForegroundColor = ConsoleColor.Green;
            string userName = Console.ReadLine();
            Console.ResetColor();

            // Prompt again if name is empty or whitespace
            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I didn't catch your name. Could you please tell me again?");
                Console.ResetColor();
                Console.Write("What should I call you? ");
                Console.ForegroundColor = ConsoleColor.Green;
                userName = Console.ReadLine();
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Hello, {userName}! I'm CyberKnight, here to help you stay safe online.");
            Console.ResetColor();
            Console.WriteLine();

            // Store user name in memory
            userMemory["name"] = userName;

            // Begin interactive Q&A session
            StartConversation(userName);
        }

        // Starts an interactive session with the user
        static void StartConversation(string userName)
        {
            bool continueChat = true;
            string currentTopic = "";
            
            DisplayHelp();

            while (continueChat)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                string prompt = string.IsNullOrEmpty(currentTopic) 
                    ? $"{userName}, what would you like to know about?" 
                    : $"{userName}, would you like more info about {currentTopic} or a new topic?";
                
                Console.Write($"{prompt} (type 'help' for the topics or 'exit' to leave): ");
                Console.ResetColor();
                
                string userInput = Console.ReadLine()?.ToLower() ?? "";
                Console.WriteLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    HandleEmptyInput();
                    continue;
                }

                if (userInput == "exit")
                {
                    EndConversation(userName);
                    continueChat = false;
                    continue;
                }

                if (userInput.Contains("help") || userInput.Contains("topic"))
                {
                    DisplayHelp();
                    currentTopic = "";
                    continue;
                }

                if (userInput.Contains("activity log") || userInput.Contains("what have you done"))
                {
                    DisplayActivityLog();
                    continue;
                }


                // Process input and update current topic
                currentTopic = ProcessUserInputWithFlow(userInput, userName, currentTopic);
            }
        }

        static void DisplayActivityLog()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("📋 Recent Activity Log:");
            Console.ResetColor();

            if (activityLog.Count == 0)
            {
                Console.WriteLine("No recent actions recorded.");
                return;
            }

            int index = 1;
            foreach (var entry in activityLog)
            {
                Console.WriteLine($"{index}. {entry}");
                index++;
            }

            Console.WriteLine();
        }


        static string ProcessUserInputWithFlow(string input, string userName, string currentTopic)
        {
            // Check if user wants to continue current topic
            if ((input.Contains("more") || input.Contains("again")) && !string.IsNullOrEmpty(currentTopic))
            {
                ProcessUserInput(currentTopic, userName);
                return currentTopic;
            }
            
            // Otherwise process new input and return new topic
            ProcessUserInput(input, userName);
            
            // Detect and return new topic
            if (input.Contains("password")) return "password security";
            if (input.Contains("phish") || input.Contains("scam")) return "phishing";
            if (input.Contains("privacy")) return "online privacy";
            if (input.Contains("malware")) return "malware protection";
            if (input.Contains("brows") || input.Contains("internet")) return "safe browsing";
            
            return ""; // No specific topic detected
        }

        // Responds based on user input topic
        static void ProcessUserInput(string input, string userName)
        {
            bool responseFound = true;
            Console.ForegroundColor = ConsoleColor.White;

            // Sentiment detection
            bool isWorried = input.Contains("worried") || input.Contains("concerned") || input.Contains("scared");
            bool isCurious = input.Contains("curious") || input.Contains("wonder") || input.Contains("tell me");
            bool isFrustrated = input.Contains("angry") || input.Contains("frustrated") || input.Contains("annoyed");

            // Memory recall
            if (input.Contains("remember") && userMemory.ContainsKey("interest"))
            {
                TypeWriterEffect($"I remember you're interested in {userMemory["interest"]}. " +
                                $"Here's something new about {userMemory["interest"]}...");
                ProcessUserInput(userMemory["interest"], userName);
                return;
            }

 


            // Predefined keyword responses
            if (input.Contains("how are you"))
            {
                string[] greetings = {
            "I'm functioning at optimal security levels! How about you?",
            "My firewalls are up and I'm ready to help!",
            "I'm doing great! Always happy to discuss cybersecurity."
        };
                Random rand = new Random();
                TypeWriterEffect(greetings[rand.Next(greetings.Length)]);
            }
            else if (input.Contains("purpose") || input.Contains("why do you exist"))
            {
                TypeWriterEffect("My purpose is to educate South African citizens about cybersecurity threats " +
                                 "and best practices to stay safe online.");
            }
            else if (input.Contains("what can i ask") || input.Contains("topics"))
            {
                DisplayHelp();
            }
            else if (input.Contains("password"))
            {
                if (isWorried) TypeWriterEffect("I understand password security can feel overwhelming. ");
                TypeWriterEffect(GetRandomResponse("password"));
                if (!userMemory.ContainsKey("interest")) userMemory.Add("interest", "password security");
            }
            else if (input.Contains("phish") || input.Contains("scam"))
            {
                if (isWorried) TypeWriterEffect("Phishing scams are common but preventable. ");
                TypeWriterEffect(GetRandomResponse("phishing"));
                if (!userMemory.ContainsKey("interest")) userMemory.Add("interest", "phishing prevention");
            }
            else if (input.Contains("privacy"))
            {
                if (isCurious) TypeWriterEffect("Great question! Privacy is crucial online. ");
                TypeWriterEffect(GetRandomResponse("privacy"));
                if (!userMemory.ContainsKey("interest")) userMemory.Add("interest", "online privacy");
            }
            else if (input.Contains("malware"))
            {
                if (isFrustrated) TypeWriterEffect("Malware can be frustrating, but prevention is possible. ");
                TypeWriterEffect(GetRandomResponse("malware"));
                if (!userMemory.ContainsKey("interest")) userMemory.Add("interest", "malware protection");
            }
            else if (input.Contains("brows") || input.Contains("internet"))
            {
                if (isCurious) TypeWriterEffect("Safe browsing is essential in today's digital world. ");
                TypeWriterEffect(GetRandomResponse("browsing"));
                if (!userMemory.ContainsKey("interest")) userMemory.Add("interest", "safe browsing");
            }
            else if (input.Contains("social") || input.Contains("engineer"))
            {
                if (isCurious) TypeWriterEffect("Social engineering tricks people into breaking security procedures:");
                TypeWriterEffect(GetRandomResponse("engineering"));
                if (!userMemory.ContainsKey("interest")) userMemory.Add("interest", "engineering");
            }
            else
            {
                // NLP Intent detection
                // NLP Intent detection
                string intent = DetectIntent(input);

                // Log recognized intent
                if (intent == "task")
                {
                    LogActivity("Detected intent: Add Task");
                    TypeWriterEffect("Sure! Let's add a task. What would you like the title to be?");
                    TaskManager.StartTaskCreation();
                    return;
                }
                else if (intent == "quiz")
                {
                    LogActivity("Detected intent: Start Quiz");
                    TypeWriterEffect("Awesome! Let's test your cybersecurity knowledge!");
                    CyberQuizGame.StartGame();
                    return;
                }


                if (intent == "task")
                {
                    TypeWriterEffect("Sure! Let's add a task. What would you like the title to be?");
                    TaskManager.StartTaskCreation();
                    return;
                }
                else if (intent == "quiz")
                {
                    TypeWriterEffect("Awesome! Let's test your cybersecurity knowledge!");
                    CyberQuizGame.StartGame();
                    return;
                }

                // Additional task-related logic (fallback commands)
                if (input.StartsWith("add task"))
                {
                    string taskTitle = input.Replace("add task", "").Trim();
                    if (string.IsNullOrWhiteSpace(taskTitle))
                    {
                        Console.WriteLine("Please provide a task title.");
                    }
                    else
                    {
                        string desc = $"Cybersecurity Task: {taskTitle}";
                        TaskManager.AddTask(taskTitle, desc);
                        Console.Write("Would you like a reminder? ");
                    }
                }
                else if (input.Contains("remind me"))
                {
                    TaskManager.SetReminder(input);
                }
                else if (input.Contains("view task"))
                {
                    TaskManager.ViewTasks();
                }
                else if (input.StartsWith("complete task"))
                {
                    var parts = input.Split(' ');
                    if (parts.Length >= 3 && int.TryParse(parts[2], out int index))
                    {
                        TaskManager.CompleteTask(index);
                        LogActivity($"Task {index} marked as completed.");
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Please specify the task number to complete. Example: complete task 2");
                    }
                }
                else if (input.StartsWith("delete task"))
                {
                    var parts = input.Split(' ');
                    if (parts.Length >= 3 && int.TryParse(parts[2], out int index))
                    {
                        TaskManager.DeleteTask(index);
                        LogActivity($"Task {index} deleted.");
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Please specify the task number to delete. Example: delete task 1");
                    }
                }
                else
                {
                    responseFound = false;
                }
            }

            // Sentiment responses
            if (isWorried)
            {
                TypeWriterEffect("\nI understand this can be concerning. Remember, being aware is the first step to protection!");
            }
            else if (isFrustrated)
            {
                TypeWriterEffect("\nCybersecurity can be frustrating, but you're doing great by seeking information!");
            }
            else if (isCurious)
            {
                TypeWriterEffect("\nThat's an excellent question! Knowledge is your best defense.");
            }

            // Fallback response
            if (!responseFound)
            {
                HandleUnknownInput();
            }

            Console.ResetColor();
            Console.WriteLine();
        }


        static string GetRandomResponse(string topic)
        {
            Random rand = new Random();
            List<string> responses = responseBank[topic];
            return responses[rand.Next(responses.Count)];
        }

        static void HandleUnknownInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            TypeWriterEffect("I'm not sure I understand. Could you try:");
            Console.WriteLine("- Rephrasing your question");
            Console.WriteLine("- Asking about passwords, phishing, or privacy");
            Console.WriteLine("- Typing 'help' for more options");
            Console.ResetColor();
        }

        static void HandleEmptyInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I didn't quite catch that. Could you please ask again?");
            Console.ResetColor();
        }

        // Displays list of topics user can ask about
        static void DisplayHelp()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("_____________________________________________________");
            Console.WriteLine("|                  Available Features               |");
            Console.WriteLine("|___________________________________________________|");
            Console.WriteLine("| • Passwords            • Phishing                   |");
            Console.WriteLine("| • Malware              • Privacy                    |");
            Console.WriteLine("| • Safe Browsing        • Social Engineering         |");
            Console.WriteLine("| • How are you?         • What's your purpose?       |");
            Console.WriteLine("|___________________________________________________|");
            Console.WriteLine("|                    Extra Commands                 |");
            Console.WriteLine("|___________________________________________________|");
            Console.WriteLine("| • Add Task / View Tasks / Complete Task            |");
            Console.WriteLine("| • Set Reminders (e.g. 'Remind me in 3 days')       |");
            Console.WriteLine("| • Start Quiz (e.g. 'Let's play a quiz')            |");
            Console.WriteLine("| • Show Activity Log / What have you done?          |");
            Console.WriteLine("| • Remember / Recall Interests (Conversation Memory)|");
            Console.WriteLine("|___________________________________________________|");
            Console.ResetColor();
            Console.WriteLine();
        }


        static void EndConversation(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            
            // Personalize goodbye if we know user's interest
            if (userMemory.ContainsKey("interest"))
            {
                TypeWriterEffect($"Goodbye, {userName}! Remember to practice good {userMemory["interest"]} habits!");
            }
            else
            {
                TypeWriterEffect($"Goodbye, {userName}! Stay safe online!");
            }
            
            Console.ResetColor();
        }

        static string DetectIntent(string input)
        {
            input = input.ToLower();

            if ((input.Contains("add") || input.Contains("set") || input.Contains("remind")) &&
                (input.Contains("task") || input.Contains("reminder") || input.Contains("to")))
            {
                return "task";
            }

            if (input.Contains("quiz") || input.Contains("game") || input.Contains("test"))
            {
                return "quiz";
            }

            if (input.Contains("password") || input.Contains("phishing") || input.Contains("malware") ||
                input.Contains("privacy") || input.Contains("browsing") || input.Contains("engineering"))
            {
                return "cyber_topic";
            }

            return "unknown";
        }


        // Simulates typing effect for text display
        static void TypeWriterEffect(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay); // Delay between characters
            }
            Console.WriteLine();
        }
    }
}
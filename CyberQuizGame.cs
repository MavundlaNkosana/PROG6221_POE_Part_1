using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberKnight
{
    public static class CyberQuizGame
    {
        private class QuizQuestion
        {
            public string QuestionText { get; set; }
            public List<string> Options { get; set; } // for MCQs
            public string CorrectAnswer { get; set; } // e.g. "A", "True"
            public string Explanation { get; set; }
            public bool IsTrueFalse { get; set; }
        }

        private static List<QuizQuestion> questions = new List<QuizQuestion>
        {
            new QuizQuestion {
                QuestionText = "True or False: You should reuse the same password for multiple accounts.",
                CorrectAnswer = "False",
                Explanation = "Reusing passwords increases your risk if one account is compromised.",
                IsTrueFalse = true
            },
            new QuizQuestion {
                QuestionText = "Which of the following is the most secure password?",
                Options = new List<string> {
                    "A. 123456",
                    "B. password123",
                    "C. T!ger$R0cks91",
                    "D. iloveyou"
                },
                CorrectAnswer = "C",
                Explanation = "Strong passwords mix letters, numbers, and symbols.",
                IsTrueFalse = false
            },
            new QuizQuestion {
                QuestionText = "True or False: HTTPS ensures your connection is secure.",
                CorrectAnswer = "True",
                Explanation = "HTTPS encrypts data between your browser and the website.",
                IsTrueFalse = true
            },
            new QuizQuestion {
                QuestionText = "Which is a sign of a phishing attempt?",
                Options = new List<string> {
                    "A. Poor grammar and urgent tone",
                    "B. Email from your own address",
                    "C. Suspicious attachments",
                    "D. All of the above"
                },
                CorrectAnswer = "D",
                Explanation = "Phishing emails often combine several red flags.",
                IsTrueFalse = false
            },
            new QuizQuestion {
                QuestionText = "True or False: Antivirus software is unnecessary if you're careful online.",
                CorrectAnswer = "False",
                Explanation = "Antivirus is an extra layer of protection against unseen threats.",
                IsTrueFalse = true
            },
            new QuizQuestion {
                QuestionText = "Which of the following is an example of social engineering?",
                Options = new List<string> {
                    "A. Brute force attack",
                    "B. Asking for a password over the phone",
                    "C. Keylogger software",
                    "D. Firewall breach"
                },
                CorrectAnswer = "B",
                Explanation = "Social engineering manipulates people, not systems.",
                IsTrueFalse = false
            },
            new QuizQuestion {
                QuestionText = "True or False: You should click unknown links in emails to check what they are.",
                CorrectAnswer = "False",
                Explanation = "Never click unknown links — hover to preview, verify the sender.",
                IsTrueFalse = true
            },
            new QuizQuestion {
                QuestionText = "Which tool helps protect your privacy on public Wi-Fi?",
                Options = new List<string> {
                    "A. VPN",
                    "B. Firewall",
                    "C. Pop-up blocker",
                    "D. Cookies"
                },
                CorrectAnswer = "A",
                Explanation = "VPNs encrypt your internet traffic, especially on unsecured networks.",
                IsTrueFalse = false
            },
            new QuizQuestion {
                QuestionText = "True or False: You should update your apps regularly to avoid bugs and vulnerabilities.",
                CorrectAnswer = "True",
                Explanation = "Updates often patch security holes hackers can exploit.",
                IsTrueFalse = true
            },
            new QuizQuestion {
                QuestionText = "Which is the best way to verify a website’s authenticity?",
                Options = new List<string> {
                    "A. Clicking the link in an email",
                    "B. Checking for a green padlock and correct domain",
                    "C. Asking a friend",
                    "D. Looking at the logo"
                },
                CorrectAnswer = "B",
                Explanation = "Always check the padlock and domain name in your browser.",
                IsTrueFalse = false
            }
        };

        public static void StartGame()
        {
            int score = 0;
            int qNum = 1;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Welcome to the Cybersecurity Quiz Mini-Game! ---\n");
            Console.ResetColor();

            CyberKnight.LogActivity("Cybersecurity quiz started.");

            foreach (var q in questions)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Question {qNum++}: {q.QuestionText}");
                Console.ResetColor();

                if (!q.IsTrueFalse && q.Options != null)
                {
                    foreach (var opt in q.Options)
                        Console.WriteLine(opt);
                }

                Console.Write("Your answer: ");
                string answer = Console.ReadLine()?.Trim().ToUpper();
                Console.WriteLine();

                if (answer == q.CorrectAnswer.ToUpper())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct! ✅");
                    score++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Incorrect. ❌ The correct answer is: {q.CorrectAnswer}");
                }

                Console.ResetColor();
                Console.WriteLine("Why: " + q.Explanation);
                Console.WriteLine("--------------------------------------------------\n");
                Thread.Sleep(500);
            }

            // Show final result
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Quiz complete! You scored {score}/{questions.Count}\n");

            if (score >= 9)
                Console.WriteLine("Excellent! You're a cybersecurity pro! 🔐");
            else if (score >= 6)
                Console.WriteLine("Good job! Just a few more tips to master. 🛡️");
            else
                Console.WriteLine("Keep learning to stay safe online. 💡");

            Console.ResetColor();

            // ✅ Log AFTER the score is known
            CyberKnight.LogActivity($"Quiz completed. Score: {score}/{questions.Count}");
        }

    }
}

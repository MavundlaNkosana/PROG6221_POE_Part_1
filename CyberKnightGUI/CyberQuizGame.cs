using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CyberKnightGUI
{
    public static class CyberQuizGame
    {
        public class QuizQuestion
        {
            public string QuestionText { get; set; }
            public List<string> Options { get; set; } // for MCQs
            public string CorrectAnswer { get; set; } // e.g. "A", "True"
            public string Explanation { get; set; }
            public bool IsTrueFalse { get; set; }
        }

        public static void StartGameGUI(ListBox lstOutput)
        {
            int score = 0;
            int qNum = 1;

            foreach (var q in questions)
            {
                string userAnswer = Microsoft.VisualBasic.Interaction.InputBox(
                    q.IsTrueFalse ? $"Q{qNum++}: {q.QuestionText} (True/False)" :
                                    $"Q{qNum++}: {q.QuestionText}\n\nOptions:\n{string.Join("\n", q.Options)}",
                    "CyberQuiz",
                    "");

                if (userAnswer?.Trim().ToUpper() == q.CorrectAnswer.ToUpper())
                {
                    lstOutput.Items.Add($"✅ Correct! Q{qNum - 1}");
                    score++;
                }
                else
                {
                    lstOutput.Items.Add($"❌ Incorrect. Q{qNum - 1} - Correct: {q.CorrectAnswer}");
                }

                lstOutput.Items.Add($"Why: {q.Explanation}");
                lstOutput.Items.Add("--------------------------------------------------");
            }

            // Final score summary
            lstOutput.Items.Add($"🎯 Score: {score}/{questions.Count}");
            CyberKnightLogic.LogActivity($"Quiz completed. Score: {score}/{questions.Count}");
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

        private static int currentQuestionIndex = 0;
        private static int score = 0;

        public static Action<string> LogActivityAction;

        public static void ResetQuiz()
        {
            currentQuestionIndex = 0;
            score = 0;
        }

        public static QuizQuestion GetNextQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                return questions[currentQuestionIndex];
            }

            return null;
        }

        public static string SubmitAnswer(string userAnswer)
        {
            var q = questions[currentQuestionIndex];
            currentQuestionIndex++;

            bool correct = userAnswer.Trim().ToUpper() == q.CorrectAnswer.ToUpper();
            if (correct) score++;

            return $"You answered: {userAnswer}\n" +
                   (correct ? "✅ Correct!" : $"❌ Incorrect. The correct answer is: {q.CorrectAnswer}") +
                   $"\nWhy: {q.Explanation}";
        }

        public static int GetScore()
        {
            return score;
        }

        public static int GetTotalQuestions()
        {
            return questions.Count;
        }

        public static string GetFinalMessage()
        {
            string msg = $"You scored {score}/{questions.Count}.\n";

            if (score >= 9) msg += "Excellent! You're a cybersecurity pro! 🔐";
            else if (score >= 6) msg += "Good job! Just a few more tips to master. 🛡️";
            else msg += "Keep learning to stay safe online. 💡";

            LogActivityAction?.Invoke($"Quiz completed. Score: {score}/{questions.Count}");
            return msg;
        }
    }
}

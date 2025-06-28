using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CyberKnightGUI
{
    public partial class QuizForm : Form
    {
        private class QuizQuestion
        {
            public string QuestionText { get; set; }
            public List<string> Options { get; set; }
            public char CorrectAnswer { get; set; } // 'a', 'b', 'c', or 'd'
            public string Explanation { get; set; }
        }

        private List<QuizQuestion> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        public QuizForm()
        {
            InitializeComponent();
            InitializeQuestions();
            LoadQuestion(currentQuestionIndex);
        }

        private void InitializeQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion {
                    QuestionText = "What does 'phishing' typically involve?",
                    Options = new List<string> {
                        "a) Sending fraudulent emails to steal info",
                        "b) Installing malware from untrusted sites",
                        "c) Using strong passwords",
                        "d) Updating software regularly"
                    },
                    CorrectAnswer = 'a',
                    Explanation = "Phishing involves sending deceptive emails to trick users into giving personal info."
                },
                new QuizQuestion {
                    QuestionText = "Which of these is a strong password practice?",
                    Options = new List<string> {
                        "a) Using 'password123'",
                        "b) Using your birthdate",
                        "c) Using a mix of letters, numbers, and symbols",
                        "d) Using 'admin' as your password"
                    },
                    CorrectAnswer = 'c',
                    Explanation = "Strong passwords combine letters, numbers, and symbols to be harder to guess."
                },
                new QuizQuestion {
                    QuestionText = "What is a VPN used for?",
                    Options = new List<string> {
                        "a) To browse anonymously and securely",
                        "b) To speed up your internet connection",
                        "c) To remove viruses from your computer",
                        "d) To backup your files automatically"
                    },
                    CorrectAnswer = 'a',
                    Explanation = "A VPN encrypts your internet traffic to protect your privacy and security online."
                },
                new QuizQuestion {
                    QuestionText = "True or False: You should never share your password with anyone.",
                    Options = new List<string> {
                        "a) True",
                        "b) False"
                    },
                    CorrectAnswer = 'a',
                    Explanation = "Passwords should always be kept private to protect your accounts."
                },
                new QuizQuestion {
                    QuestionText = "Which of these is a sign of a scam email?",
                    Options = new List<string> {
                        "a) Poor grammar and spelling mistakes",
                        "b) Email from a trusted colleague",
                        "c) Official company logos",
                        "d) Personalized greetings"
                    },
                    CorrectAnswer = 'a',
                    Explanation = "Scam emails often have spelling errors and suspicious grammar."
                },
                new QuizQuestion {
                    QuestionText = "What is malware?",
                    Options = new List<string> {
                        "a) Software that protects your computer",
                        "b) Software designed to harm or exploit devices",
                        "c) An internet browser",
                        "d) A type of antivirus"
                    },
                    CorrectAnswer = 'b',
                    Explanation = "Malware is malicious software created to damage or disrupt computers."
                },
                new QuizQuestion {
                    QuestionText = "How often should you update your software?",
                    Options = new List<string> {
                        "a) Only when it stops working",
                        "b) Regularly, to patch security vulnerabilities",
                        "c) Never",
                        "d) Only if a friend tells you to"
                    },
                    CorrectAnswer = 'b',
                    Explanation = "Regular updates fix bugs and security issues, keeping your system safe."
                },
                new QuizQuestion {
                    QuestionText = "True or False: Using the same password for multiple accounts is safe.",
                    Options = new List<string> {
                        "a) True",
                        "b) False"
                    },
                    CorrectAnswer = 'b',
                    Explanation = "Using unique passwords per account helps prevent a breach from spreading."
                },
                new QuizQuestion {
                    QuestionText = "What is 'social engineering'?",
                    Options = new List<string> {
                        "a) Hacking using software tools",
                        "b) Manipulating people to give up info",
                        "c) Building computer networks",
                        "d) Writing software code"
                    },
                    CorrectAnswer = 'b',
                    Explanation = "Social engineering tricks people into revealing confidential info."
                },
                new QuizQuestion {
                    QuestionText = "What should you do if you receive an unexpected email attachment?",
                    Options = new List<string> {
                        "a) Open it immediately",
                        "b) Delete the email without opening",
                        "c) Verify the sender before opening",
                        "d) Forward it to friends"
                    },
                    CorrectAnswer = 'c',
                    Explanation = "Always verify unknown attachments before opening to avoid malware."
                }
            };
        }

        private void LoadQuestion(int index)
        {
            if (index >= questions.Count)
            {
                // Quiz finished
                lblQuestion.Text = "Quiz Complete!";
                lstOptions.Items.Clear();
                txtUserAnswer.Enabled = false;
                btnSubmitAnswer.Enabled = false;
                lblFeedback.Text = $"Your final score is {score} out of {questions.Count}.";
                lblScore.Text = "";
                return;
            }

            var q = questions[index];
            lblQuestion.Text = q.QuestionText;
            lstOptions.Items.Clear();
            foreach (var option in q.Options)
            {
                lstOptions.Items.Add(option);
            }

            txtUserAnswer.Text = "";
            lblFeedback.Text = "";
            lblScore.Text = $"Score: {score} / {questions.Count}";
        }

        private void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex >= questions.Count) return;

            string answer = txtUserAnswer.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(answer))
            {
                MessageBox.Show("Please enter your answer (e.g., a, b, c, d, true, false).");
                return;
            }

            var currentQ = questions[currentQuestionIndex];
            char expected = currentQ.CorrectAnswer;

            bool isCorrect = false;

            // Handle true/false questions with a/b or true/false
            if (currentQ.Options.Count == 2 &&
                (answer == "true" || answer == "false" || answer == "a" || answer == "b"))
            {
                if (answer == "true" && (expected == 'a')) isCorrect = true;
                else if (answer == "false" && (expected == 'b')) isCorrect = true;
                else if (answer == expected.ToString()) isCorrect = true;
            }
            else
            {
                // Otherwise just compare the single char answer
                if (answer.Length == 1 && answer[0] == expected) isCorrect = true;
            }

            if (isCorrect)
            {
                score++;
                lblFeedback.Text = $"Correct! {currentQ.Explanation}";
            }
            else
            {
                lblFeedback.Text = $"Incorrect. {currentQ.Explanation}";
            }

            lblScore.Text = $"Score: {score} / {questions.Count}";

            currentQuestionIndex++;

            // Small delay before loading next question (optional)
            Timer timer = new Timer();
            timer.Interval = 1500; // 1.5 seconds delay
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                LoadQuestion(currentQuestionIndex);
            };
            timer.Start();
        }
    }
}

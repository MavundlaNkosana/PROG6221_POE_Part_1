using System;
using System.Collections.Generic;

namespace CyberKnightGUI
{
    public static class CyberKnightLogic
    {
        public static Dictionary<string, string> userMemory = new Dictionary<string, string>();
        public static List<string> activityLog = new List<string>();
        public const int maxLogEntries = 10;
        public static string LastTopic { get; set; } = null;

        public static void Remember(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                userMemory[key.ToLower()] = value;
        }

        public static string Recall(string key)
        {
            if (userMemory.TryGetValue(key.ToLower(), out var value))
                return value;
            return null;
        }

        public static Dictionary<string, string> GetKeywordTopicMap()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", "password" },
                { "passcode", "password" },
                { "credentials", "password" },
                { "phish", "phishing" },
                { "scam", "phishing" },
                { "fraud", "phishing" },
                { "spoof", "phishing" },
                { "privacy", "privacy" },
                { "private", "privacy" },
                { "vpn", "privacy" },
                { "encryption", "privacy" },
                { "malware", "malware" },
                { "virus", "malware" },
                { "trojan", "malware" },
                { "worm", "malware" },
                { "spyware", "malware" },
                { "ransomware", "malware" },
                { "browsing", "browsing" },
                { "internet", "browsing" },
                { "surf", "browsing" },
                { "https", "browsing" },
                { "social", "engineering" },
                { "engineer", "engineering" },
                { "trick", "engineering" },
                { "manipulate", "engineering" },
                { "deceive", "engineering" }
            };
        }

        private static readonly Dictionary<string, List<string>> sentimentKeywords = new Dictionary<string, List<string>>
        {
            { "worried", new List<string> { "worried", "scared", "anxious", "nervous", "afraid", "concerned" } },
            { "curious", new List<string> { "curious", "wondering", "interested", "what", "how", "why" } },
            { "frustrated", new List<string> { "angry", "annoyed", "frustrated", "irritated", "upset", "tired" } }
        };

        public static void LogActivity(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            activityLog.Add($"[{timestamp}] {message}");

            if (activityLog.Count > maxLogEntries)
                activityLog.RemoveAt(0);
        }

        public static List<string> GetHelp()
        {
            return new List<string>
            {
                "Available Topics:",
                "- Password Security",
                "- Phishing & Scams",
                "- Privacy",
                "- Malware",
                "- Safe Browsing",
                "- Social Engineering",
                "",
                "Features:",
                "- Add/View/Complete Tasks",
                "- Set Reminders",
                "- Take a Cybersecurity Quiz",
                "- See Activity Log",
            };
        }

        public static string GetRandomResponse(string topic)
        {
            if (!responseBank.ContainsKey(topic)) return "I'm not sure how to answer that yet.";

            LastTopic = topic;

            var rand = new Random();
            var responses = responseBank[topic];
            var baseResponse = responses[rand.Next(responses.Count)];

            var name = Recall("name");
            if (!string.IsNullOrWhiteSpace(name))
                return $"Alright {name}, here's a tip:\n{baseResponse}";

            return baseResponse;
        }

        public static string GetFollowUpSuggestion()
        {
            if (string.IsNullOrEmpty(LastTopic)) return "";

            var suggestions = new Dictionary<string, string>
            {
                { "password", "Would you like tips on using password managers?" },
                { "phishing", "Want to learn how to report phishing attempts?" },
                { "privacy", "Should we talk about social media privacy settings?" },
                { "malware", "Do you want advice on avoiding fake software?" },
                { "browsing", "Want to know how to browse safely on public Wi-Fi?" },
                { "engineering", "Should I explain how attackers manipulate trust?" }
            };

            return suggestions.ContainsKey(LastTopic) ? suggestions[LastTopic] : "";
        }

        private static Dictionary<string, List<string>> responseBank = new Dictionary<string, List<string>>
        {
            { "password", new List<string>
                {
                    "Strong passwords keep hackers out!",
                    "Use at least 12 characters, mix them up!",
                    "Avoid reusing the same password across platforms.",
                    "Use a password manager to store your credentials securely."
                }
            },
            { "phishing", new List<string>
                {
                    "Don’t click suspicious links!",
                    "Always check the sender’s email.",
                    "Beware of urgent messages demanding immediate action.",
                    "Phishers often impersonate legitimate brands — stay alert!"
                }
            },
            { "privacy", new List<string>
                {
                    "Keep your social media profiles private.",
                    "Use VPNs on public Wi-Fi to stay anonymous.",
                    "Avoid oversharing personal info online.",
                    "Enable two-factor authentication for sensitive accounts."
                }
            },
            { "malware", new List<string>
                {
                    "Install antivirus software and keep it updated.",
                    "Don’t download files from untrusted sources.",
                    "Run regular system scans.",
                    "Be cautious with popups or shady downloads."
                }
            },
            { "browsing", new List<string>
                {
                    "Always look for HTTPS in URLs.",
                    "Clear your cache and cookies regularly.",
                    "Use private browsing modes when on public devices.",
                    "Avoid downloading browser extensions from unknown sources."
                }
            },
            { "engineering", new List<string>
                {
                    "Social engineering tricks people, not systems.",
                    "If a message feels off — verify before acting.",
                    "Never give passwords over phone/email.",
                    "Watch for emotional manipulation like fear or urgency."
                }
            }
        };

        private static readonly Dictionary<string, List<string>> sentimentResponses = new Dictionary<string, List<string>>
        {
            { "worried", new List<string>
                {
                    "It’s okay to feel worried. Cyber threats can seem scary, but I’ve got your back.",
                    "You’re not alone. Let's break it down and protect you step by step.",
                    "Worry is natural — the good news is you're being proactive by asking!"
                }
            },
            { "curious", new List<string>
                {
                    "Curiosity is the first step toward cybersecurity mastery!",
                    "I love how inquisitive you are — let’s dive into it.",
                    "Great question! Let’s explore that together."
                }
            },
            { "frustrated", new List<string>
                {
                    "It’s frustrating, I get it. Cybersecurity can be complex, but you’re doing great.",
                    "Hang in there — every expert started as a beginner.",
                    "Frustration means you're trying — let’s make things clearer."
                }
            },
            { "neutral", new List<string>
                {
                    "Let me know if there's anything you'd like help with.",
                    "I'm here whenever you're ready to chat about cybersecurity!",
                    "Feel free to ask anything about online safety."
                }
            }
        };

        public static string MatchKeywordToTopic(string input)
        {
            var map = GetKeywordTopicMap();
            foreach (var pair in map)
            {
                if (input.ToLower().Contains(pair.Key.ToLower()))
                    return pair.Value;
            }
            return null;
        }

        public static string DetectSentiment(string input)
        {
            string lowered = input.ToLower();

            foreach (var pair in sentimentKeywords)
            {
                foreach (string keyword in pair.Value)
                {
                    if (lowered.Contains(keyword))
                        return pair.Key;
                }
            }

            return "neutral";
        }

        public static string GetSentimentResponse(string sentiment)
        {
            if (sentimentResponses.ContainsKey(sentiment))
            {
                var responses = sentimentResponses[sentiment];
                string response = responses[new Random().Next(responses.Count)];
                return $"CyberKnight: I hear you.\n{response}";
            }

            return "I'm here to help however you're feeling.";
        }

        public static List<string> GetActivityLog()
        {
            return activityLog;
        }
    }
}

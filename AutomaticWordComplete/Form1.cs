using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AutomaticWordComplete
{
    public partial class Form1 : Form
    {
        private Trie trie = new Trie();

        public Form1()
        {
            InitializeComponent();
            LoadPhrasesFromFile("phrases.txt");           
        }

        private void LoadPhrasesFromFile(string filePath)
        {
            try
            {
                string fullPath = Path.Combine(Application.StartupPath,$"..\\..\\{filePath}");

                if (File.Exists(fullPath))
                {
                    string[] lines = File.ReadAllLines(fullPath);
                    foreach (string line in lines)
                    {
                        string phrase = line.Trim();
                        if (!string.IsNullOrWhiteSpace(phrase))
                            trie.Insert(phrase.ToLower());
                    }
                }
                else
                {
                    MessageBox.Show($"'{fullPath}' not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading phrases: " + ex.Message);
            }
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            string prefix = txtInput.Text.Trim().ToLower();
            lstSuggestions.Items.Clear();

            if (prefix == "")
                return;

            List<string> suggestions = trie.AutoComplete(prefix);

            if (suggestions.Count == 0)
            {
                lstSuggestions.Items.Add("No matches found.");
            }
            else
            {
                foreach (var suggestion in suggestions)
                {
                    lstSuggestions.Items.Add(suggestion);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
                        string selectedPhrase = lstSuggestions.SelectedItem as string;

            if (!string.IsNullOrWhiteSpace(selectedPhrase))
            {
                MessageBox.Show($"You selected: '{selectedPhrase}'", "Search Result");
            }
            else
            {
                MessageBox.Show("Please select a suggestion from the list.", "No Selection");
            }
        
        }
    }
}

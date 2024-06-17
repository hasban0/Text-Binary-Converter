using System;
using System.Text;
using System.Windows.Forms;

namespace TextToBinary
{
    public partial class Main : Form
    {
        private bool isTextToBinary = true; // Current conversion mode (Text to Binary or Binary to Text)
        private string binaryOutput = string.Empty; // Stores binary output
        private string textOutput = string.Empty; // Stores text output

        public Main()
        {
            InitializeComponent();
        }

        // Function to convert Text to Binary
        private string ConvertTextToBinary(string text)
        {
            string binaryString = "";
            foreach (char c in text)
            {
                // Converts each character to an 8-bit binary format and separates with a space
                binaryString += Convert.ToString(c, 2).PadLeft(8, '0') + " ";
            }
            return binaryString.Trim(); // Removes trailing space and returns
        }

        // Function to convert Binary to Text
        private string ConvertBinaryToText(string binary)
        {
            string[] binaryArray = binary.Split(' ');
            StringBuilder text = new StringBuilder();

            foreach (string binaryChar in binaryArray)
            {
                if (!string.IsNullOrEmpty(binaryChar))
                {
                    // Converts each binary character to integer and then to ASCII character
                    text.Append((char)Convert.ToUInt64(binaryChar, 2));
                }
            }
            return text.ToString(); // Returns the resulting text
        }

        // Triggered when text input changes
        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            if (isTextToBinary)
            {
                // If in Text to Binary conversion mode
                string inputText = textBoxInput.Text;
                binaryOutput = ConvertTextToBinary(inputText); // Converts text to binary
                textBoxOutput.Text = binaryOutput; // Shows binary output

                // If 'Copy' option is checked and output is not empty, copy to clipboard
                if (binaryOutput != string.Empty && checkBox1.Checked)
                {
                    Clipboard.SetText(binaryOutput);
                }
            }
            else
            {
                // If in Binary to Text conversion mode
                if (IsValidBinaryInput(textBoxInput.Text))
                {
                    string binaryText = textBoxInput.Text;
                    textOutput = ConvertBinaryToText(binaryText); // Converts binary to text
                    textBoxOutput.Text = textOutput; // Shows text output

                    // If 'Copy' option is checked and output is not empty, copy to clipboard
                    if (textOutput != string.Empty && checkBox1.Checked)
                    {
                        Clipboard.SetText(textOutput);
                    }
                }
                else
                {
                    // Removes invalid binary characters
                    textBoxInput.Text = RemoveInvalidBinaryCharacters(textBoxInput.Text);
                }
            }
        }

        // Button function to toggle conversion mode
        private void buttonToggle_Click(object sender, EventArgs e)
        {
            // Clears input and output boxes
            textBoxInput.Text = string.Empty;
            textBoxOutput.Text = string.Empty;

            // Updates mode and labels
            if (isTextToBinary)
            {
                label1.Text = "Binary";
                label2.Text = "Text";
                buttonToggle.Text = "Convert to Text";
            }
            else
            {
                label1.Text = "Text";
                label2.Text = "Binary";
                buttonToggle.Text = "Convert to Binary";
            }
            isTextToBinary = !isTextToBinary; // Toggles the mode
        }

        // Function to check valid binary input
        private bool IsValidBinaryInput(string input)
        {
            foreach (char c in input)
            {
                if (c != '0' && c != '1' && c != ' ')
                {
                    return false; // Returns false if there are invalid characters
                }
            }
            return true; // Returns true if input is valid binary
        }

        // Function to remove invalid binary characters
        private string RemoveInvalidBinaryCharacters(string input)
        {
            StringBuilder validInput = new StringBuilder();
            foreach (char c in input)
            {
                if (c == '0' || c == '1' || c == ' ')
                {
                    validInput.Append(c); // Appends valid characters
                }
            }
            return validInput.ToString(); // Returns cleaned input
        }

        // Checkbox change event for 'Copy' functionality
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // If 'Copy' option is checked
                if (isTextToBinary)
                {
                    if (binaryOutput != string.Empty)
                        Clipboard.SetText(binaryOutput); // Copies binary output to clipboard
                }
                else
                {
                    if (textOutput != string.Empty)
                        Clipboard.SetText(textOutput); // Copies text output to clipboard
                }
            }
        }
    }
}

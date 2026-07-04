using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class MainForm : Form
{
    private TextBox pathTextBox;
    private TextBox wordTextBox;
    private Button searchButton;
    private ListBox resultListBox;

    public MainForm()
    {
        InitializeControls();
    }

    private void InitializeControls()
    {
        this.Text = "Пошук слова у файлах";
        this.Width = 600;
        this.Height = 400;

        pathTextBox = new TextBox { PlaceholderText = "Шлях до директорії", Top = 10, Left = 10, Width = 400 };
        wordTextBox = new TextBox { PlaceholderText = "Слово для пошуку", Top = 40, Left = 10, Width = 400 };
        searchButton = new Button { Text = "Пошук", Top = 70, Left = 10 };
        resultListBox = new ListBox { Top = 100, Left = 10, Width = 560, Height = 250 };

        searchButton.Click += async (s, e) => await StartSearch();

        Controls.Add(pathTextBox);
        Controls.Add(wordTextBox);
        Controls.Add(searchButton);
        Controls.Add(resultListBox);
    }

    private async Task StartSearch()
    {
        string path = pathTextBox.Text;
        string word = wordTextBox.Text;

        if (!Directory.Exists(path) || string.IsNullOrWhiteSpace(word))
        {
            MessageBox.Show("Введіть коректні дані");
            return;
        }

        resultListBox.Items.Clear();
        searchButton.Enabled = false;

        await Task.Run(() => SearchInFiles(path, word));

        searchButton.Enabled = true;
    }

    private void SearchInFiles(string path, string word)
    {
        var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            try
            {
                string content = File.ReadAllText(file);
                int count = 0;
                int index = content.IndexOf(word, StringComparison.OrdinalIgnoreCase);

                while (index != -1)
                {
                    count++;
                    index = content.IndexOf(word, index + word.Length, StringComparison.OrdinalIgnoreCase);
                }

                if (count > 0)
                {
                    string report = $"Назва файлу: {Path.GetFileName(file)} | Шлях: {file} | Входжень: {count}";
                    this.Invoke(new Action(() => resultListBox.Items.Add(report)));
                }
            }
            catch { }
        }
    }
}

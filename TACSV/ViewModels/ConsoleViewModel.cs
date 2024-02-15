namespace TACSV.ViewModels;

public class ConsoleViewModel : ViewModelBase
{
    private string _entries { get; set; }
    public string Entries { get { return _entries; } set { _entries = value; OnPropertyChanged(nameof(Entries)); } }

    public ConsoleViewModel()
    {
        Entries = TACSVConsole.GetEntriesString();
        TACSVConsole.Entries.CollectionChanged += (_, _) => Entries = TACSVConsole.GetEntriesString();
    }
}
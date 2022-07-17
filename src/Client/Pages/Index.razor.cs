using System.Data;

namespace ReflectInput.Client.Pages;

public partial class Index
{
    private string _identifier { get; set; }
    private string _text { get; set; }
    private List<DataRow>? _items { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    private void ResetClicked()
    {
        _identifier = string.Empty;
        _text = string.Empty;
        _items = null;
    }

    private void AnalyzeClicked()
    {
        _items = new List<DataRow>();
    }
}

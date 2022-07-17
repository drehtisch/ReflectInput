using System.Data;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ReflectInput.Analysis;

namespace ReflectInput.Client.Pages;

public partial class Index
{
    private string? _identifier { get; set; }
    private string? _text { get; set; }
    private  bool analyzed { get; set; }

    private TextAnalyzer _textAnalyzer = new();

    [Inject] private IJSRuntime _jsRuntime { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    private void ClearClicked()
    {
        _identifier = string.Empty;
        _text = string.Empty;
        analyzed = false;
        _textAnalyzer.Clear();
    }

    private async Task ExportCsvClicked()
    {
        var aggregateTable = _textAnalyzer.GetAggregateTable();
        string csvContent = GetCsvStringFromDatatable(aggregateTable);

        await using var csvStream = GenerateStreamFromString(csvContent);
        using var streamRef = new DotNetStreamReference(stream: csvStream);

        string date = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        await _jsRuntime.InvokeVoidAsync("downloadFileFromStream", $"ReflectInput-Export-{date}.csv", streamRef);
    }

    private void NextClicked()
    {
        _identifier = string.Empty;
        _text = string.Empty;
        analyzed = false;
    }

    private void AnalyzeClicked()
    {
        if (string.IsNullOrWhiteSpace(_identifier) || string.IsNullOrWhiteSpace(_text))
        {
            return;
        }

        _ = _textAnalyzer.CountWords(_text, _identifier);
        analyzed = true;
    }

    private string GetCsvStringFromDatatable(DataTable datatable)
    {
        var sb = new StringBuilder();
        int numberOfColumns = datatable.Columns.Count;
        char seperator = ';';

        for (int i = 0; i < numberOfColumns; i++)
        {
            sb.Append(datatable.Columns[i]);
            if (i < numberOfColumns - 1)
                sb.Append(seperator);
        }
        sb.Append(Environment.NewLine);

        foreach (DataRow dr in datatable.Rows)
        {
            for (int i = 0; i < numberOfColumns; i++)
            {
                sb.Append(dr[i].ToString());

                if (i < numberOfColumns - 1)
                    sb.Append(seperator);
            }
            sb.Append(Environment.NewLine);
        }
        return sb.ToString();
    }

    private Stream GenerateStreamFromString(string input)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(input);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}

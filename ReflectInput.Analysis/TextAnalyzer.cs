using System.Data;

namespace ReflectInput.Analysis;

public class TextAnalyzer
{
    private readonly List<InputResultModel> _inputResults = new();
    public InputResultModel CountWords(string content, string identifier)
    {
        char[]? punctuation = content.Where(char.IsPunctuation).Distinct().ToArray();

        var words = content
            .Split()
            .Select(x => x.ToLower().Trim(punctuation))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        var groups = words
            .GroupBy(x => x)
            .OrderBy(x => x.Key);

        var dict = groups.ToDictionary(x => x.Key, x => x.Count());
        var model = new InputResultModel() { CountedWords = dict, Identifier = identifier };
        _inputResults.Add(model);
        return model;
    }

    public Dictionary<string, int> GetAggregate()
    {
        var result = new Dictionary<string, int>();
        foreach (var inputResult in _inputResults)
        {
            foreach (var kvp in inputResult.CountedWords)
            {
                if (result.ContainsKey(kvp.Key))
                {
                    result[kvp.Key] += kvp.Value;
                }
                else
                {
                    result[kvp.Key] = kvp.Value;
                }
            }
        }

        return result.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
    }

    public DataTable GetAggregateTable()
    {
        DataTable table = new DataTable();
        var dictAggregate = GetAggregate();
        _ = table.Columns.Add("Identifier");
        foreach (var kvp in dictAggregate)
        {
            _ = table.Columns.Add(kvp.Key);
        }

        foreach (var result in _inputResults)
        {
            var row = table.NewRow();
            row[0] = result.Identifier;
            for (int i = 1; i < table.Columns.Count; i++)
            {
                string? name = table.Columns[i].ColumnName;
                if (result.CountedWords.TryGetValue(name, out int count))
                {
                    row[i] = count;
                }
                else
                {
                    row[i] = 0;
                }
            }

            table.Rows.Add(row);
        }

        var totalRow = table.NewRow();
        totalRow[0] = "Total";
        for (int i = 1; i < table.Columns.Count; i++)
        {
            string? name = table.Columns[i].ColumnName;
            totalRow[i] = dictAggregate[name];
        }

        table.Rows.Add(totalRow);

        return table;
    }
}

public class InputResultModel
{
    public string Identifier { get; init; }
    public Dictionary<string, int> CountedWords { get; init; }
}
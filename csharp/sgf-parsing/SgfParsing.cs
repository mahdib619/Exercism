using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sprache;

public class SgfTree
{
    public SgfTree(IDictionary<string, string[]> data, params SgfTree[] children)
    {
        Data = data;
        Children = children;
    }

    public IDictionary<string, string[]> Data { get; }
    public SgfTree[] Children { get; }
}

public static class SgfParser
{
    public static SgfTree ParseTree(string input)
    {
        var parser = GetParser();
        var result = parser.TryParse(input);

        return result.WasSuccessful ? result.Value : throw new ArgumentException("Invalid input!", nameof(input));
    }

    private static Parser<SgfTree> GetParser()
    {
        var valueTextParser = (from esc in Parse.Char('\\').Optional()
                               from ch in esc.IsDefined ? Parse.AnyChar : Parse.AnyChar.Except(Parse.Chars("[]"))
                               select esc.IsDefined ? EscapeChar(ch) : ch).AtLeastOnce().Text();

        var propertyValueParser = valueTextParser.Contained(Parse.Char('['), Parse.Char(']'));

        var propParser = from name in Parse.Upper.Many().Text()
                         from values in propertyValueParser.AtLeastOnce()
                         select new KeyValuePair<string, string[]>(name, values.ToArray());

        var nodeDataParser = from props in propParser.Many()
                             select new Dictionary<string, string[]>(props);

        var singleChildTreeParser = from open in Parse.Regex("\\(;")
                                    from treesData in nodeDataParser.DelimitedBy(Parse.Char(';'))
                                    from close in Parse.Char(')').End()
                                    select GenerateTree(treesData.Select(t => t.OneAsColl()));

        var childrenGroupDataParser = from o in Parse.Regex("\\(;")
                                      from nodes in nodeDataParser.DelimitedBy(Parse.Regex(Regex.Escape(")(;")))
                                      from c in Parse.Char(')')
                                      select nodes;

        var multiChildrenTreeParser = from open in Parse.Regex("\\(;")
                                      from fi in nodeDataParser
                                      from treesData in childrenGroupDataParser.DelimitedBy(Parse.Char(';'))
                                      from close in Parse.Char(')').End()
                                      select GenerateTree(fi.OneAsColl().OneAsColl().Concat(treesData));

        return singleChildTreeParser.Or(multiChildrenTreeParser);
    }

    private static SgfTree GenerateTree(IEnumerable<IEnumerable<Dictionary<string, string[]>>> data) =>
        data.Reverse()
            .Aggregate(default(SgfTree[]), (current, cData) =>
            {
                if (current is null)
                    return cData.Select(d => new SgfTree(d)).ToArray();

                var items = cData.ToList();
                return items.Count == 1 ? items.Select(d => new SgfTree(d, current)).ToArray() : items.Select(d => new SgfTree(d, current)).Concat(current).ToArray();
            }).First();

    private static IEnumerable<T> OneAsColl<T>(this T obj)
    {
        yield return obj;
    }

    private static char EscapeChar(char ch) => ch switch
    {
        'n' => '\n',
        't' => ' ',
        _ => ch
    };
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlocks
{
    public class Span
    {
        public int? Start { get; set; }
        public int? End { get; set; }
        public string? Content { get; set; }
    }

    public class ExpansionBuilder
    {
        private List<Span> _spans = new();
        public IList<Span> Spans => _spans;

        private Span? _spanOnDeck;
        public Span? OnDeck => _spanOnDeck;

        public Expansion? Expansion { get; set; }

        public Span BeginNewSpan()
        {
            _spanOnDeck = new();
            _spans.Add(_spanOnDeck);
            return _spanOnDeck;
        }
    }

    public class Expansion
    {
        public string MarkupSource { get; }
        public string MarkupRender { get; }

        public Expansion(string markupSource, string markupTarget)
        {
            MarkupSource = markupSource;
            MarkupRender = markupTarget;
        }
    }

    public class MarkupExpander
    {
        private IList<ExpansionBuilder> expansions = new List<ExpansionBuilder>();

        public ExpansionBuilder BeginNewExpansion()
        {
            ExpansionBuilder onDeck = new();
            expansions.Add(onDeck);
            return onDeck;
        }

        public void Expand(StringBuilder processor)
        {
            foreach(Expansion? expansion in expansions.Select(eb => eb.Expansion))
            {
                if (expansion != null)
                {
                    processor.Replace(expansion.MarkupSource, expansion.MarkupRender);
                }
            }
        }
    }
}
